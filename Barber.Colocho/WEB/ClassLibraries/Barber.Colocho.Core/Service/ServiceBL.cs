using Barber.Colocho.Core.Error;
using Barber.Colocho.Core.Helpers;
using Barber.Colocho.Entities.Tables;
using Barber.Colocho.Models.Generic;
using Barber.Colocho.Models.Service;
using Barber.Colocho.Repository.Repository;
using Microsoft.EntityFrameworkCore;

namespace Barber.Colocho.Core.Service
{
    public class ServiceBL
    {
        #region Constructor
        private readonly IGenericRepository<Entities.Tables.Service> service;
        private readonly IGenericRepository<Entities.Tables.Company> company;
        private readonly IGenericRepository<Entities.Tables.ImageService> imageService;
        private readonly IGenericRepository<Entities.Tables.Suscription> suscription;
        private readonly ErrorBL errorBL;
        private readonly UploadImage uploadImage;
        private readonly IGenericRepository<Entities.Tables.RatingService> ratingService;

        public ServiceBL(IGenericRepository<Entities.Tables.Service> service,IGenericRepository<Entities.Tables.Company> company,IGenericRepository<Entities.Tables.ImageService> imageService,IGenericRepository<Entities.Tables.Suscription> suscription,ErrorBL errorBL, UploadImage uploadImage,IGenericRepository<Entities.Tables.RatingService> ratingService)
        {
            this.service = service;
            this.company = company;
            this.imageService = imageService;
            this.suscription = suscription;
            this.errorBL = errorBL;
            this.uploadImage = uploadImage;
            this.ratingService = ratingService;
        }
        #endregion

        public async Task<Response<bool>> AddService(int userId, int companyId, AddServiceModel vm)
        {
            try
            {
                var validatesuscription = await suscription.FindAsync(c => c.IsActive && c.CompanyId == companyId);
                if (validatesuscription == null)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "No tienes una suscripción activa"
                    };

                var searchCompany = await company.FindAsync(c => c.IsActive && c.Id == companyId && c.IdUser == userId && c.User.IsConfirmed && c.User.IsActive, include: query => query.Include(u => u.User));
                if (searchCompany == null)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "No se encontró la barbería"
                    };
                if (string.IsNullOrWhiteSpace(vm.Name))
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "Debe agregar un nombre al servicio"
                    };
                if (string.IsNullOrWhiteSpace(vm.Description))
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "Debe agregar una descripción"
                    };
                if (vm.Price == 0)
                    return new Response<bool>
                    {
                        Message = "Debe agregar el precio"
                    };
                var adS = await service.AddAsync(new Entities.Tables.Service
                {
                    CompanyId = companyId,
                    Created = DateTime.UtcNow,
                    Delete = null,
                    Description = vm.Description,
                    Updated = null,
                    Duration = vm.Duration,
                    IsActive = true,
                    IsHomeService = vm.IsHomeService,
                    Name = vm.Name,
                    Price = vm.Price,
                });
                if (adS != null && adS.Id > 0)
                    return new Response<bool>
                    {
                        Result = true,
                        Message = "Se ha guardo el servicio"
                    };
                else
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "No se ha podido guardar el servicio"
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

        public async Task<Response<bool>> DeleteService(int serviceId,int companyId,int userId)
        {
            try
            {
                var searchService = await service.FindAsync(c => c.IsActive && c.Company.IdUser == userId && c.Id == serviceId && c.CompanyId == companyId, include: query => query.Include(i => i.ImageService).Include(c => c.Company));
                if (searchService == null)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "No se encontró el servicio"
                    };
                if (searchService.ImageService != null && searchService.ImageService.Count > 0)
                {
                    foreach(var item in  searchService.ImageService)
                    {
                        item.IsActive = false;
                        item.Delete = DateTime.UtcNow;
                        await imageService.UpdateAsync(item);
                        await uploadImage.Remove("ImageService", item.Url);
                    }
                }
                searchService.IsActive = false;   
                searchService.Delete = DateTime.UtcNow;
                await service.UpdateAsync(searchService);
                return new Response<bool>
                {
                    Result = true,
                    Message = "Se ha eliminado el servicio"
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

        public async Task<Response<bool>> DeleteImageService(int ImageServiceId,int serviceId,int userId)
        {
            try
            {
                var searchImage = await imageService.FindAsync(c => c.IsActive && c.Id == ImageServiceId && c.ServiceId == serviceId && c.Service.Company.IdUser == userId, include: query => query.Include(s => s.Service).ThenInclude(c => c.Company));
                if (searchImage == null)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "No se encontró la imagen"
                    };
                await uploadImage.Remove("ImageService", searchImage.Url);
                searchImage.Delete = DateTime.UtcNow;
                searchImage.IsActive = false;
                await imageService.UpdateAsync(searchImage);
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

        public async Task<Response<bool>> AddImageService(int serviceId, int companyId,AddImageModel model, int userId)
        {
            try
            {
                var searchCompany = await service.FindAsync(c => c.IsActive && c.Id == serviceId && c.CompanyId == companyId && c.Company.IdUser == userId, include: query => query.Include(i => i.ImageService).Include(c => c.Company));
                if (searchCompany == null)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "No se encontró el servicio"
                    };

                int countImage = searchCompany.ImageService.Where(c => c.IsActive).ToList().Count;
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
                    string name = await uploadImage.Upload(img, "ImageService", fileName);
                }
                var adImg = await imageService.AddAsync(new Entities.Tables.ImageService
                {
                    Created = DateTime.UtcNow,
                    Delete = null,
                    Updated = null,
                    IsActive = true,
                    Url = fileName,
                    ServiceId = serviceId,
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

        public async Task<Response<bool>>UpdateService(int serviceId,int companyId, UpdateServiceModel vm,int userId)
        {
            try
            {
                var searchService = await service.FindAsync(c => c.IsActive && c.Id == serviceId && c.CompanyId == companyId && c.Company.IdUser == userId, include: query => query.Include(c => c.Company));
                if (searchService == null)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "No se encontró el servicio"
                    };
                if (string.IsNullOrWhiteSpace(vm.Name))
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "Debe agregar un nombre al servicio"
                    };
                if (string.IsNullOrWhiteSpace(vm.Description))
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "Debe agregar una descripción"
                    };
                if (vm.Price == 0)
                    return new Response<bool>
                    {
                        Message = "Debe agregar el precio"
                    };
                searchService.Price = vm.Price;
                searchService.Description = vm.Description;
                searchService.Name = vm.Name;
                searchService.Duration = (vm.Duration == 0) ? searchService.Duration : vm.Duration;
                searchService.IsHomeService = vm.IsHomeService;
                searchService.Updated = DateTime.UtcNow;
                await service.UpdateAsync(searchService);
                return new Response<bool>
                {
                    Result = true,
                    Message = "Se ha guardo el servicio"
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

        public async Task<Response<List<GetServiceModel>>> GetListServiceByIdCompany(int companyId, int userId)
        {
            try
            {
                var searchCompany = await company.FindAsync(c => c.IsActive && c.Id == companyId && c.IdUser == userId && c.User.IsConfirmed && c.User.IsActive, include: query => query.Include(u => u.User).Include(s => s.Service).ThenInclude(s => s.RatingService).Include(s => s.Service).ThenInclude(i => i.ImageService).Include(c=>c.Suscription));
                if (searchCompany == null)
                    return new Response<List<GetServiceModel>>
                    {
                        Result = null,
                        Message = "No hay servicios"
                    };
                var filterSus = searchCompany.Suscription.Where(c => c.IsActive).FirstOrDefault();
                if (filterSus == null)
                    return new Response<List<GetServiceModel>>
                    {
                        Result = null,
                        Message = "No tiene suscripción activa"
                    };

                var list = new List<GetServiceModel>();
                foreach (var item in searchCompany.Service.Where(c => c.IsActive))
                {
                    

                    var listImage = new List<GenericModel>();
                    foreach (var itemI in item.ImageService.Where(c=>c.IsActive))
                    {
                        listImage.Add(new GenericModel
                        {
                            Id = itemI.Id,
                            Name = uploadImage.GetFile(itemI.Url, "ImageService")
                        });                      
                    }
                    double rating = item.RatingService.Where(c => c.IsActive).Sum(r => r.Rating);
                    list.Add(new GetServiceModel
                    {
                        ListImage = listImage,
                        Duration = item.Duration,
                        Description = item.Description,
                        Name = item.Name,
                        Price = item.Price,
                        IsHomeService = item.IsHomeService,
                        Id = item.Id,
                        Created = item.Created,
                        CompanyName = searchCompany.Name,
                        CompanyId = searchCompany.Id,
                        Rating = rating
                    });
                }
                return new Response<List<GetServiceModel>>
                {
                    Result = list,
                    Count = list.Count
                };
            }
            catch(Exception ex)
            {
                await errorBL.LogError(ex);
                return new Response<List<GetServiceModel>>
                {
                    Result = null,
                    Message = "Hubo un error, intente más tarde"
                };
            }
        }
        public async Task<Response<GetServiceModel>> GetServiceByIdService(int companyId, int serviceId, int userId)
        {
            try
            {
                var searchCompany = await service.GetAllAsync(c => c.IsActive && c.Id == companyId && c.CompanyId == companyId && c.Id == serviceId && c.Company.IdUser == userId, include: query => query.Include(im => im.ImageService).Include(c => c.Company).Include(r => r.RatingService));
                if (searchCompany == null)
                    return new Response<GetServiceModel>
                    {
                        Result = null,
                        Message = "No se encontró la barbería"
                    };
                var list = new List<GetServiceModel>();
                foreach (var item in searchCompany)
                {
                    var listImage = new List<GenericModel>();
                    foreach (var itemI in item.ImageService.Where(c => c.IsActive))
                    {
                        listImage.Add(new GenericModel
                        {
                            Id = itemI.Id,
                            Name = uploadImage.GetFile(itemI.Url, "ImageService")
                        });
                    }
                    var rating = item.RatingService.Where(n => n.IsActive)
                       .Sum(n => n.Rating);
                    list.Add(new GetServiceModel
                    {
                        ListImage = listImage,
                        Duration = item.Duration,
                        Description = item.Description,
                        Name = item.Name,
                        Price = item.Price,
                        IsHomeService = item.IsHomeService,
                        Id = item.Id,
                        Created = item.Created,
                        CompanyName = item.Company.Name,
                        CompanyId = item.Company.Id,
                        Rating = rating,
                    });
                }
                return new Response<GetServiceModel>
                {
                    Result = list.FirstOrDefault(),
                    Count = (list.FirstOrDefault() != null) ? list.Count : 0,
                    Message = (list.FirstOrDefault() != null) ? string.Empty : "No hay servicios"
                };
            }
            catch (Exception ex)
            {
                await errorBL.LogError(ex);
                return new Response<GetServiceModel>
                {
                    Result = null,
                    Message = "Hubo un error, intente más tarde"
                };
            }
        }

        public async Task<Response<bool>> AddCalificationService(int companyId, int serviceId, int userId, AddRatingModel addRating)
        {
            try
            {
                var searchCompany = await company.FindAsync(c => c.IsActive && c.Id == companyId);
                if (searchCompany == null)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "No se encontró la barbería"
                    };
                var searchService = await service.FindAsync(c => c.IsActive && c.Id == serviceId);
                if (searchService == null)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "No se encontró el servicio"
                    };
                if (string.IsNullOrWhiteSpace(addRating.Comment))
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "Debe agregar un comentario"
                    };
                if (addRating.Rating <= 0)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "Debe agregar una calificación válida"
                    };
                var result = await ratingService.AddAsync(new RatingService
                {
                    Comment = addRating.Comment,
                    Created = DateTime.UtcNow,
                    IsActive = true,
                    Rating = addRating.Rating,
                    UserId = userId,
                    ServiceId = serviceId,
                    Updated = null,
                    Delete = null,
                });

                var countServiceRating = await ratingService.GetAllAsync(c => c.IsActive && c.Service.CompanyId == companyId, include: query => query.Include(c => c.Service));
                var listCount = countServiceRating.ToList();
                int count = listCount.Count;
                double sum = listCount.Sum(c => c.Rating);
                double rating = Convert.ToDouble((sum / count));
                searchCompany.Rating = rating;
                searchCompany.Updated = DateTime.UtcNow;
                await company.UpdateAsync(searchCompany);
                if (result != null && result.Id > 0)
                    return new Response<bool>
                    {
                        Result = true,
                        Message = "Se ha calificado correctamente el servicio"
                    };
                else
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "No se pudo calificar el servicio, intente más tarde"
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

        #region Service AllUser
        public async Task<Response<List<GetServiceModel>>> GetListServiceAllUser(int page)
        {
            try
            {
                var searchCompany = await service.GetAllAsync(c => c.IsActive && c.Company.User.IsConfirmed && c.Company.User.IsActive, include: query => query.Include(c => c.Company).ThenInclude(u => u.User).Include(i => i.ImageService).Include(r => r.RatingService).Include(c => c.Company).ThenInclude(c => c.Suscription));
                if (searchCompany == null)
                    return new Response<List<GetServiceModel>>
                    {
                        Result = null,
                        Message = "No hay servicios"
                    };
                var list = new List<GetServiceModel>();
                int count = searchCompany.ToList().Count;
                var filter = searchCompany.Pagination(page, 10);
                foreach (var item in filter)
                {
                    var filterSus = item.Company.Suscription.Where(c => c.IsActive).FirstOrDefault();
                    if(filterSus != null)
                    {
                        var listImage = new List<GenericModel>();
                        foreach (var itemI in item.ImageService.Where(c => c.IsActive))
                        {
                            listImage.Add(new GenericModel
                            {
                                Id = itemI.Id,
                                Name = uploadImage.GetFile(itemI.Url, "ImageService")
                            });
                        }
                        var rating = item.RatingService.Where(n => n.IsActive)
                           .Sum(n => n.Rating);

                        list.Add(new GetServiceModel
                        {
                            ListImage = listImage,
                            Duration = item.Duration,
                            Description = item.Description,
                            Name = item.Name,
                            Price = item.Price,
                            IsHomeService = item.IsHomeService,
                            Id = item.Id,
                            Created = item.Created,
                            CompanyName = item.Company.Name,
                            CompanyId = item.Company.Id,
                            Rating = rating,
                        });
                    }
                    
                }
                return new Response<List<GetServiceModel>>
                {
                    Result = list,
                    Count = count
                };
            }
            catch (Exception ex)
            {
                await errorBL.LogError(ex);
                return new Response<List<GetServiceModel>>
                {
                    Result = null,
                    Message = "Hubo un error, intente más tarde"
                };
            }
        }
        public async Task<Response<GetServiceModel>> GetServiceAllUser(int companyId, int serviceId)
        {
            try
            {
                var searchCompany = await service.GetAllAsync(c => c.IsActive && c.Id == companyId && c.CompanyId == companyId && c.Id == serviceId, include: query => query.Include(im => im.ImageService).Include(c => c.Company).Include(r => r.RatingService).Include(c => c.Company).ThenInclude(c => c.Suscription));
                if (searchCompany == null)
                    return new Response<GetServiceModel>
                    {
                        Result = null,
                        Message = "No se encontró la barbería"
                    };

                var list = new List<GetServiceModel>();
                foreach (var item in searchCompany)
                {
                    var filterSus = item.Company.Suscription.Where(c => c.IsActive).FirstOrDefault();
                    if(filterSus != null)
                    {
                        var listImage = new List<GenericModel>();
                        foreach (var itemI in item.ImageService.Where(c => c.IsActive))
                        {
                            listImage.Add(new GenericModel
                            {
                                Id = itemI.Id,
                                Name = uploadImage.GetFile(itemI.Url, "ImageService")
                            });
                        }
                        var rating = item.RatingService.Where(n => n.IsActive)
                           .Sum(n => n.Rating);
                        list.Add(new GetServiceModel
                        {
                            ListImage = listImage,
                            Duration = item.Duration,
                            Description = item.Description,
                            Name = item.Name,
                            Price = item.Price,
                            IsHomeService = item.IsHomeService,
                            Id = item.Id,
                            Created = item.Created,
                            CompanyName = item.Company.Name,
                            CompanyId = item.Company.Id,
                            Rating = rating,
                        });
                    }
                }
                return new Response<GetServiceModel>
                {
                    Result = list.FirstOrDefault(),
                    Count = (list.FirstOrDefault() != null) ? list.Count : 0,
                    Message = (list.FirstOrDefault() != null) ? string.Empty : "No hay servicios"
                };
            }
            catch (Exception ex)
            {
                await errorBL.LogError(ex);
                return new Response<GetServiceModel>
                {
                    Result = null,
                    Message = "Hubo un error, intente más tarde"
                };
            }
        }

        public async Task<Response<List<ListRatingModel>>> ListCalificationService(int serviceId)
        {
            try
            {
                var list = new List<ListRatingModel>();
                var searchCompany = await service.FindAsync(c => c.IsActive && c.Company.User.IsConfirmed && c.Company.User.IsActive, include: query => query.Include(c => c.Company).ThenInclude(u => u.User).Include(i => i.ImageService).Include(r => r.RatingService).Include(c => c.Company).ThenInclude(c => c.Suscription));
                if (searchCompany == null)
                    return new Response<List<ListRatingModel>>
                    {
                        Result = null,
                        Message = "No se encontró el servicio"
                    };
                var result = await ratingService.GetAllAsync(c => c.ServiceId == serviceId);
                foreach (var rating in result)
                {
                    list.Add(new ListRatingModel
                    {
                        Comment = rating.Comment,
                        Rating = rating.Rating,
                        ServiceId = rating.ServiceId
                    });
                }
                return new Response<List<ListRatingModel>>
                {
                    Result = list,
                    Count = list.Count,
                    Message = string.Empty
                };
            }
            catch(Exception ex)
            {
                return new Response<List<ListRatingModel>>
                {
                    Result = null,
                    Message = "Hubo un error, intente más tarde"
                };
            }
        }
        #endregion
    }
}
