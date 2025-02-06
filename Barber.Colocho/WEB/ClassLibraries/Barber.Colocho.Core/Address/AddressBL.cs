using Barber.Colocho.Core.Error;
using Barber.Colocho.Core.Helpers;
using Barber.Colocho.Models.Address;
using Barber.Colocho.Repository.Repository;
using Microsoft.EntityFrameworkCore;

namespace Barber.Colocho.Core.Address
{
    public class AddressBL
    {
        #region Constructor
        private readonly IGenericRepository<Entities.Tables.Address> address;
        private readonly IGenericRepository<Entities.Tables.User> user;
        private readonly ErrorBL errorBL;
        private readonly IGenericRepository<Entities.Tables.Company> company;

        public AddressBL(IGenericRepository<Entities.Tables.Address> address, IGenericRepository<Entities.Tables.User> user, ErrorBL errorBL,IGenericRepository<Entities.Tables.Company> company)
        {
            this.address = address;
            this.user = user;
            this.errorBL = errorBL;
            this.company = company;
        }
        #endregion

        public async Task<Response<bool>> AddAddressUser(int userId, AddAddressModel model)
        {
            try
            {
                if (userId == 0)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "Debe agregar un usuario"
                    };
                if (string.IsNullOrWhiteSpace(model.Name))
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "Agregue un nombre"
                    };
                if (string.IsNullOrWhiteSpace(model.Street))
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "Agregue la calle"
                    };
                if (string.IsNullOrWhiteSpace(model.CodePostal))
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "Agregue el código postal"
                    };
                if (model.IdColony == 0)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "Debe agregar la colonia"
                    };
                if (string.IsNullOrWhiteSpace(model.Observations))
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "Agregue observaciones"
                    };

                if (model.IsDefault)
                {
                    var searchDefault = await address.GetAllAsync(c => c.UserId == userId && c.IsActive && c.IsDefault);
                    if (searchDefault != null && searchDefault.Count() > 0) 
                    {
                        foreach (var item in searchDefault) 
                        {
                            item.IsDefault = false;
                            await address.UpdateAsync(item);
                        }
                    }
                }
                else
                {
                    var searchDefault = await address.GetAllAsync(c => c.UserId == userId && c.IsActive && c.IsDefault);
                    if (searchDefault != null && searchDefault.Count() > 0)
                        model.IsDefault = false;
                    else
                        model.IsDefault = true;
                }

                var ad = await address.AddAsync(new Entities.Tables.Address
                {
                    CodePostal = model.CodePostal,
                    Created = DateTime.UtcNow,
                    Delete = null,
                    IsDefault = model.IsDefault,
                    IdColony = model.IdColony,
                    IsActive = true,
                    Name = model.Name,
                    NumExt = model.NumExt,
                    Observations = model.Observations,
                    Street = model.Street,
                    Updated = null,
                    UserId = userId,
                    Latitude = model.Latitude,
                    Longitude = model.Longitude
                });
                if (ad != null && ad.Id > 0)
                    return new Response<bool>
                    {
                        Result = true,
                        Message = "Se ha registrado la dirección"
                    };
                else
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "No se pudo registrar la dirección"
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
        public async Task<Response<bool>> DeleteAddress(int userId, int idAddress)
        {
            try
            {
                var ad = await address.FindAsync(c => c.UserId == userId && c.Id == idAddress && c.User.IsActive && c.User.IsConfirmed, include: query => query.Include(u => u.User));
                if (ad == null)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "No se encontró la dirección"
                    };
                var searchAddresCompany = await company.FindAsync(c => c.IsActive && c.IdUser == userId && c.IdAddress == idAddress);
                if (searchAddresCompany != null)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "No se puede eliminar a dirección esta vinculada a una empresa"
                    };

                ad.IsActive = false;
                ad.Delete = DateTime.UtcNow;
                await address.UpdateAsync(ad);
                return new Response<bool>
                {
                    Result = true,
                    Message = "Se ha eliminado la dirección"
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
        public async Task<Response<List<AddressModel>>> GetListAddressByUserId(int userId)
        {
            try
            {
                var list = new List<AddressModel>();
                var result = await user.FindAsync(c => c.IsConfirmed && c.IsActive && c.Id == userId, include: query =>query.Include(a => a.Address).ThenInclude(c => c.Colony).ThenInclude(c => c.City).ThenInclude(s => s.State));
                foreach (var item in result.Address.Where(c => c.IsActive))
                {
                    list.Add(new AddressModel
                    {
                        Id = item.Id,
                        IsDefault = item.IsDefault,
                        Name = item.Name,
                        NumExt = item.NumExt,
                        CodePostal = item.CodePostal,
                        Street = item.Street,
                        ColonyName = item.Colony.Name,
                        Observations = item.Observations,
                        Created = item.Created,
                        CityName = item.Colony.City.Name,
                        StateName = item.Colony.City.State.Name,
                        Latitude = item.Latitude,
                        Longitude = item.Longitude,
                        IdColony = item.IdColony
                    });
                }
                return new Response<List<AddressModel>>
                {
                    Result = list,
                    Count = list.Count()
                };
            }
            catch (Exception ex)
            {
                await errorBL.LogError(ex);
                return new Response<List<AddressModel>>
                {
                    Result = null,
                    Message = "Hubo un error, intente más tarde"
                };
            }
        }
        public async Task<Response<AddressModel>> GetAddressById(int addressId,int userId)
        {
            try
            {
                var list = new List<AddressModel>();
                var result = await address.GetAllAsync(c => c.IsActive && c.Id == addressId && c.UserId == userId, include: query => query.Include(c => c.Colony).ThenInclude(c => c.City).ThenInclude(s => s.State));
                foreach (var item in result)
                {
                    list.Add(new AddressModel
                    {
                        Id = item.Id,
                        IsDefault = item.IsDefault,
                        Name = item.Name,
                        NumExt = item.NumExt,
                        CodePostal = item.CodePostal,
                        Street = item.Street,
                        ColonyName = item.Colony.Name,
                        Observations = item.Observations,
                        Created = item.Created,
                        CityName = item.Colony.City.Name,
                        StateName = item.Colony.City.State.Name,
                        Latitude = item.Latitude,
                        Longitude = item.Longitude,
                        IdColony = item.IdColony
                    });
                }
                return new Response<AddressModel>
                {
                    Result = list.FirstOrDefault(),
                    Count = (list.FirstOrDefault() == null) ? 0 : 1
                };
            }
            catch (Exception ex)
            {
                await errorBL.LogError(ex);
                return new Response<AddressModel>
                {
                    Result = null,
                    Message = "Hubo un error, intente más tarde"
                };
            }
        }
        public async Task<Response<bool>> UpdateDefaultAddress(int addressId,int userId)
        {
            try
            {
                var searchAddress = await address.GetAllAsync(c => c.IsActive && c.UserId == userId);
                var filter = searchAddress.Where(c => c.Id == addressId).FirstOrDefault();
                if (filter == null)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "No se encontró la dirección"
                    };

                foreach (var item in searchAddress) 
                {
                    item.Updated = DateTime.UtcNow;
                    if (item.Id == addressId)
                        item.IsDefault = true;
                    else
                        item.IsDefault = false;
                    await address.UpdateAsync(item);
                }
                return new Response<bool>
                {
                    Result = true,
                    Message = "Se ha cambiado la dirección predeterminada"
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
        public async Task<Response<bool>> UpdateAddressUser(int userId, int addressId, AddAddressModel model)
        {
            try
            {
                var ad = await address.FindAsync(c => c.IsActive && c.Id == addressId);
                if (ad == null)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "No se encontró la dirección"
                    };
                if (userId == 0)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "Debe agregar un usuario"
                    };
                if (string.IsNullOrWhiteSpace(model.Name))
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "Agregue un nombre"
                    };
                if (string.IsNullOrWhiteSpace(model.Street))
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "Agregue la calle"
                    };
                if (string.IsNullOrWhiteSpace(model.CodePostal))
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "Agregue el código postal"
                    };
                if (model.IdColony == 0)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "Debe agregar la colonia"
                    };
                if (string.IsNullOrWhiteSpace(model.Observations))
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "Agregue observaciones"
                    };

                if (model.IsDefault)
                {
                    var searchDefault = await address.GetAllAsync(c => c.UserId == userId && c.IsActive && c.IsDefault);
                    if (searchDefault != null && searchDefault.Count() > 0)
                    {
                        foreach (var item in searchDefault)
                        {
                            item.IsDefault = false;
                            await address.UpdateAsync(item);
                        }
                    }
                }
                else
                {
                    var searchDefault = await address.GetAllAsync(c => c.UserId == userId && c.IsActive && c.IsDefault && c.Id == addressId);
                    if (searchDefault != null && searchDefault.Count() > 0)
                        model.IsDefault = false;
                    else
                        model.IsDefault = true;
                }
                ad.IsDefault = model.IsDefault;
                ad.Observations = model.Observations;
                ad.CodePostal = model.CodePostal;
                ad.Updated = DateTime.UtcNow;
                ad.Street = model.Street;
                ad.IdColony = model.IdColony;
                ad.Name = model.Name;
                ad.NumExt = model.NumExt;
                ad.Longitude = model.Longitude;
                ad.Latitude = model.Latitude;
                await address.UpdateAsync(ad);
                return new Response<bool>
                {
                    Result = true,
                    Message = "Se ha actualizado la dirección"
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
    }
}
