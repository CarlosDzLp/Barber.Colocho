using Barber.Colocho.Core.Error;
using Barber.Colocho.Core.Helpers;
using Barber.Colocho.Entities.Tables;
using Barber.Colocho.Models.Address;
using Barber.Colocho.Models.Company;
using Barber.Colocho.Models.Generic;
using Barber.Colocho.Repository.Repository;
using Microsoft.EntityFrameworkCore;

namespace Barber.Colocho.Core.Company
{
    public class CompanyBL
    {

        #region Constructor
        private readonly IGenericRepository<Entities.Tables.Company> company;
        private readonly IGenericRepository<Entities.Tables.Address> address;
        private readonly IGenericRepository<Entities.Tables.Image> image;
        private readonly IGenericRepository<Entities.Tables.Suscription> suscription;
        private readonly ErrorBL errorBL;
        private readonly UploadImage uploadImage;

        public CompanyBL(IGenericRepository<Entities.Tables.Company> company,IGenericRepository<Entities.Tables.Address> address,IGenericRepository<Entities.Tables.Image> image,IGenericRepository<Entities.Tables.Suscription> suscription, ErrorBL errorBL, UploadImage uploadImage)
        {
            this.company = company;
            this.address = address;
            this.image = image;
            this.suscription = suscription;
            this.errorBL = errorBL;
            this.uploadImage = uploadImage;
        }
        #endregion

        public async Task<Response<bool>> AddCompany(AddCompanyModel vm, int UserId)
        {
            try
            {
                var searchAddress = await address.FindAsync(c => c.Id == vm.AddressId && c.IsActive && c.UserId == UserId && c.User.IsActive && c.User.IsConfirmed, include: query => query.Include(u => u.User));
                if (searchAddress == null)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "No existe la dirección proporcionada"
                    };
                if (!string.IsNullOrWhiteSpace(vm.RFC))
                {
                    if (!RulesValidations.FluentValidation.IsRFC(vm.RFC))
                    {
                        return new Response<bool>
                        {
                            Result = false,
                            Message = "El RFC no es válido"
                        };
                    }
                }
                //var searchUser = await company.FindAsync(c => c.IsActive && c.IdUser == UserId && c.User.IsActive && c.User.IsConfirmed, include: query => query.Include(u => u.User));
                //if (searchUser != null)
                //    return new Response<bool>
                //    {
                //        Result = true,
                //        Message = "Usted ya tiene una empresa creada"
                //    };
                if (string.IsNullOrWhiteSpace(vm.Name))
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "Debe agregar el nombre"
                    };
                if (string.IsNullOrWhiteSpace(vm.Description))
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "Debe agregar una descripción"
                    };
                var resultAdd = await company.AddAsync(new Entities.Tables.Company
                {
                    Created = DateTime.UtcNow,
                    Name = vm.Name,
                    Description = vm.Description,
                    IdUser = UserId,
                    Updated = null,
                    Delete = null,
                    IdAddress = vm.AddressId,
                    IsActive = true,
                    RFC = vm.RFC
                });
                if (resultAdd != null && resultAdd.Id > 0)
                    return new Response<bool>
                    {
                        Result = true,
                        Message = "Se ha registrado la barbería"
                    };
                else
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "No se ha podido registrar la barbería"
                    };
            }
            catch(Exception ex)
            {
                await errorBL.LogError(ex);
                return new Response<bool>
                {
                    Result = false,
                    Message = "Hubo un error, intente más tarde"
                };
            }
        }

        public async Task<Response<bool>> DeleteCompany(int IdCompany,int userId)
        {
            try
            {
                var search = await company.FindAsync(c => c.IsActive && c.Id == IdCompany && c.IdUser == userId, include: query => query.Include(l => l.Image));
                if (search == null)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "No se encontró la barbería"
                    };
                if(search.Image != null && search.Image.Count > 0)
                {
                    foreach (var item in search.Image)
                    {
                        item.IsActive = false;
                        item.Delete = DateTime.UtcNow;
                        await image.UpdateAsync(item);
                        await uploadImage.Remove("Company", item.Url);
                    }
                }
                
                search.IsActive = false;
                search.Delete = DateTime.UtcNow;
                await company.UpdateAsync(search);
                return new Response<bool>
                {
                    Result = true,
                    Message = "Se ha eliminado la barbería"
                };
            }
            catch (Exception ex)
            {
                await errorBL.LogError(ex);
                return new Response<bool>
                {
                    Result = false,
                    Message = "Hubo un error, intente más tarde"
                };
            }
        }

        public async Task<Response<List<GetCompanyModel>>> GetListCompany(int userId)
        {
            try
            {
                var list = new List<GetCompanyModel>();
                var searchCompany = await company.GetAllAsync(c => c.IsActive && c.IdUser == userId && c.User.IsActive && c.User.IsConfirmed, include: query => query.Include(u => u.User).Include(lI => lI.Image).Include(s => s.Service).ThenInclude(r => r.RatingService).Include(c => c.Suscription)
                .Include(c => c.Address).ThenInclude(c => c.Colony).ThenInclude(c => c.City).ThenInclude(c => c.State));
                foreach (var item in searchCompany)
                {
                    var filterSus = item.Suscription.Where(c => c.IsActive).FirstOrDefault();
                    var listImg = new List<GenericModel>();
                    foreach (var itemI in item.Image)
                    {
                        listImg.Add(new GenericModel
                        {
                            Id = itemI.Id,
                            Name = uploadImage.GetFile(itemI.Url, "Company")
                        });
                    }
                    double rating = item.Rating;
                    var ad = new AddressModel
                    {
                        Id = item.Address.Id,
                        IsDefault = item.Address.IsDefault,
                        Name = item.Address.Name,
                        NumExt = item.Address.NumExt,
                        CodePostal = item.Address.CodePostal,
                        Street = item.Address.Street,
                        ColonyName = item.Address.Colony.Name,
                        Observations = item.Address.Observations,
                        Created = item.Address.Created,
                        CityName = item.Address.Colony.City.Name,
                        StateName = item.Address.Colony.City.State.Name,
                        Latitude = item.Address.Latitude,
                        Longitude = item.Address.Longitude,
                    };
                    list.Add(new GetCompanyModel
                    {
                        Address = ad,
                        Name = item.Name,
                        RFC = item.RFC,
                        Description = item.Description,
                        Id = item.Id,
                        ListImage = listImg,
                        Created = item.Created,
                        Rating = rating,
                        IsSuscription = (filterSus != null) ? true : false
                    });
                }
                return new Response<List<GetCompanyModel>>
                {
                    Result = list,
                    Count = list.Count
                };
            }
            catch (Exception ex)
            {
                await errorBL.LogError(ex);
                return new Response<List<GetCompanyModel>>
                {
                    Result = null,
                    Message = "Hubo un error, intente más tarde"
                };
            }
        }

        public async Task<Response<GetCompanyModel>> GetCompany(int companyId,int userId)
        {
            try
            {
                var list = new List<GetCompanyModel>();
                var searchCompany = await company.GetAllAsync(c => c.IsActive && c.IdUser == userId && c.Id == companyId, include: query => query.Include(lI => lI.Image).Include(a => a.Address).ThenInclude(c => c.Colony).ThenInclude(s => s.City).ThenInclude(s => s.State).Include(s => s.Service).ThenInclude(r => r.RatingService).Include(c => c.Suscription).ThenInclude(c => c.Plan));
                foreach (var item in searchCompany)
                {
                    var filterSus = item.Suscription.Where(c => c.IsActive).FirstOrDefault();
                    var listImg = new List<GenericModel>();
                    foreach (var itemI in item.Image)
                    {
                        listImg.Add(new GenericModel
                        {
                            Id = itemI.Id,
                            Name = uploadImage.GetFile(itemI.Url, "Company")
                        });
                    }
                    double rating = item.Rating;
                    var ad = new AddressModel
                    {
                        Id = item.Address.Id,
                        IsDefault = item.Address.IsDefault,
                        Name = item.Address.Name,
                        NumExt = item.Address.NumExt,
                        CodePostal = item.Address.CodePostal,
                        Street = item.Address.Street,
                        ColonyName = item.Address.Colony.Name,
                        Observations = item.Address.Observations,
                        Created = item.Address.Created,
                        CityName = item.Address.Colony.City.Name,
                        StateName = item.Address.Colony.City.State.Name,
                        Latitude = item.Address.Latitude,
                        Longitude = item.Address.Longitude,
                    };
                    list.Add(new GetCompanyModel
                    {
                        Name = item.Name,
                        RFC = item.RFC,
                        Description = item.Description,
                        Id = item.Id,
                        ListImage = listImg,
                        Created = item.Created,
                        Address = ad,
                        Rating = rating,
                        Suscription = (filterSus != null) ? new SuscriptionModel
                        {
                            PlanName = filterSus.Plan.Name,
                            InitSuscription = filterSus.InitSuscription,
                            Id = filterSus.Id,
                            IsActive = filterSus.IsActive,
                            Created = filterSus.Created,
                            FinishSuscription = filterSus.FinishSuscription,
                            Price = filterSus.Price
                        } : null,
                        IsSuscription = (filterSus != null) ? true : false
                    });
                }
                return new Response<GetCompanyModel>
                {
                    Result = list.FirstOrDefault(),
                    Count = (list.FirstOrDefault() != null) ? 1 : 0
                };
            }
            catch (Exception ex)
            {
                await errorBL.LogError(ex);
                return new Response<GetCompanyModel>
                {
                    Result = null,
                    Message = "Hubo un error, intente más tarde"
                };
            }
        }

        public async Task<Response<bool>> UpdateCompany(int companyId, int userId, UpdateCompanyModel vm)
        {
            try
            {
                var searchAddress = await address.FindAsync(c => c.Id == vm.AddressId && c.IsActive && c.UserId == userId && c.User.IsActive && c.User.IsConfirmed, include: query => query.Include(u => u.User));
                if (searchAddress == null)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "No existe la dirección proporcionada"
                    };
                var searchCompany = await company.FindAsync(c => c.IsActive && c.IdUser == userId && c.User.IsActive && c.User.IsConfirmed && c.Id == companyId, include: query => query.Include(u => u.User));
                if (searchCompany == null)
                    return new Response<bool>
                    {
                        Result = true,
                        Message = "No se encontró la barbería"
                    };
                if (string.IsNullOrWhiteSpace(vm.Name))
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "Debe agregar el nombre"
                    };
                if (string.IsNullOrWhiteSpace(vm.Description))
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "Debe agregar una descripción"
                    };
                searchCompany.Updated = DateTime.UtcNow;
                searchCompany.Name = vm.Name;
                searchCompany.Description = vm.Description;
                searchCompany.IdAddress = vm.AddressId;
                searchCompany.RFC = (string.IsNullOrWhiteSpace(vm.RFC)) ? searchCompany.RFC : vm.RFC;
                await company.UpdateAsync(searchCompany);
                return new Response<bool>
                {
                    Result = true,
                    Message = "Se ha actualizado la barbería"
                };
            }
            catch (Exception ex)
            {
                await errorBL.LogError(ex);
                return new Response<bool>
                {
                    Result = false,
                    Message = "Hubo un error, intente más tarde"
                };
            }
        }

        public async Task<Response<bool>> AddImageCompany(int companyId,int userId, AddImageModel model)
        {
            try
            {
                var searchCompany = await company.FindAsync(c => c.IdUser == userId && c.IsActive && c.Id == companyId, include: query => query.Include(i => i.Image));
                if (searchCompany == null)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "No se encontró la barbería"
                    };

                int countImage = searchCompany.Image.Where(c => c.IsActive).ToList().Count;
                if (countImage >= 5)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "Ya alcanzó el maximo de imagenes"
                    };


                if (model.File == null)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "No se cargó ninguna imagen"
                    };

                if (model.File.Length == 0)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "No se cargó ninguna imagen"
                    };

                if (model.File.ContentType != "image/png")
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "No se cargó ninguna imagen"
                    };
                string fileName = $"{Guid.NewGuid()}.png";
                var stm = model.File.OpenReadStream();
                using (MemoryStream ms = new MemoryStream())
                {
                    stm.CopyTo(ms);
                    var img = ms.ToArray();
                    string name = await uploadImage.Upload(img, "Company", fileName);
                }
                var adImg = await image.AddAsync(new Image
                {
                    CompanyId = companyId,
                    Created = DateTime.UtcNow,
                    Delete = null,
                    Updated = null,
                    IsActive = true,
                    Url = fileName
                });
                if (adImg != null && adImg.Id > 0)
                    return new Response<bool>
                    {
                        Result = true,
                        Message = "Se ha guardado la magen"
                    };
                else
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "No se pudo guardar la imagen"
                    };
            }
            catch(Exception ex)
            {
                await errorBL.LogError(ex);
                return new Response<bool>
                {
                    Result = false,
                    Message = "Hubo un error, intente más tarde"
                };
            }
        }

        public async Task<Response<bool>> DeleteImageCompany(int imageId, int userId, int companyId)
        {
            try
            {

                var searchImage = await image.FindAsync(c => c.IsActive && c.Id == imageId && c.CompanyId == companyId && c.Company.IdUser == userId, include: query => query.Include(c => c.Company));
                if (searchImage == null)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "No se encontró la imagen"
                    };
                await uploadImage.Remove("Company", searchImage.Url);
                searchImage.Delete = DateTime.UtcNow;
                searchImage.IsActive = false;
                await image.UpdateAsync(searchImage);
                return new Response<bool>
                {
                    Result = true,
                    Message = "Se ha eliminado la imagen"
                };
            }
            catch (Exception ex)
            {
                await errorBL.LogError(ex);
                return new Response<bool>
                {
                    Result = false,
                    Message = "Hubo un error, intente más tarde"
                };
            }
        }

        public async Task<Response<bool>> SuscripcionActiveCompany(int userId, int companyId)
        {
            try
            {
                var searchSuscription = await suscription.FindAsync(c => c.IsActive && c.CompanyId == companyId && c.Company.IdUser == userId, include: query => query.Include(c => c.Company));
                if (searchSuscription == null)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "No tiene una suscripción activa"
                    };
                else
                    return new Response<bool>
                    {
                        Result = true,
                        Message = "Tiene suscripción activa"
                    };
            }
            catch(Exception ex)
            {
                await errorBL.LogError(ex);
                return new Response<bool>
                {
                    Result = false,
                    Message = "Hubo un error, intente más tarde"
                };
            }
        }
    }
}
