using Barber.Colocho.Core.Error;
using Barber.Colocho.Core.Helpers;
using Barber.Colocho.Models;
using Barber.Colocho.Models.Generic;
using Barber.Colocho.Repository.Repository;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Barber.Colocho.Core.INEGI
{
    public class InegiBL
    {
        #region Constructor
        private readonly IGenericRepository<Entities.Tables.State> state;
        private readonly IGenericRepository<Entities.Tables.City> city;
        private readonly IGenericRepository<Entities.Tables.Colony> colony;
        private readonly ErrorBL errorBL;

        public InegiBL(IGenericRepository<Entities.Tables.State> state, IGenericRepository<Entities.Tables.City> city, IGenericRepository<Entities.Tables.Colony> colony,ErrorBL errorBL)
        {
            this.state = state;
            this.city = city;
            this.colony = colony;
            this.errorBL = errorBL;
        }
        #endregion

        public async Task<Response<List<GenericModel>>> GetState()
        {
            try
            {
                var list = new List<GenericModel>();
                var sta = await state.GetAllAsync(c => c.IsActive);
                foreach (var item in sta) 
                {
                    list.Add(new GenericModel
                    {
                        Id = item.Id,
                        Name = item.Name,
                    });
                }
                return new Response<List<GenericModel>>
                {
                    Result = list,Count = list.Count()
                };
            }
            catch (Exception ex)
            {
                await errorBL.LogError(ex);
                return new Response<List<GenericModel>>
                {
                    Result = null,
                    Message = "Hubo un error, intente más tarde"
                };
            }
        }

        public async Task<Response<List<GenericModel>>>GetCity(int IdState)
        {
            try
            {
                var list = new List<GenericModel>();
                var result = await city.GetAllAsync(c => c.IdState == IdState && c.IsActive);
                foreach (var item in result)
                {
                    list.Add(new GenericModel
                    {
                        Id = item.Id,
                        Name = item.Name,
                    });
                }
                return new Response<List<GenericModel>>
                {
                    Result = list,
                    Count = list.Count()
                };
            }
            catch(Exception ex)
            {
                await errorBL.LogError(ex);
                return new Response<List<GenericModel>>
                {
                    Result = null,
                    Message = "Hubo un error, intente más tarde"
                };
            }
        }

        public async Task<Response<List<GenericModel>>> GetColony(int IdCity)
        {
            try
            {
                var list = new List<GenericModel>();
                var result = await colony.GetAllAsync(c => c.IsActive && c.IdCity == IdCity);
                foreach (var item in result)
                {
                    list.Add(new GenericModel
                    {
                        Id = item.Id,
                        Name = item.Name,
                    });
                }
                return new Response<List<GenericModel>>
                {
                    Result = list,
                    Count = list.Count()
                };
            }
            catch (Exception ex)
            {
                await errorBL.LogError(ex);
                return new Response<List<GenericModel>>
                {
                    Result = null,
                    Message = "Hubo un error, intente más tarde"
                };
            }
        }

        public async Task<Response<CodePostalInegeModel>> GetCodePostal(string codePostal)
        {
            try
            {
                var list = new List<GenericModel>();
                var result = await colony.GetAllAsync(c => c.IsActive && c.CodePostal == codePostal, include: query => query.Include(c => c.City).ThenInclude(s => s.State));
                if (result == null || result.Count() == 0)
                    return new Response<CodePostalInegeModel>
                    {
                        Result = null,
                        Message = "No se encontraron datos"
                    };
                foreach (var item in result)
                {
                    list.Add(new GenericModel
                    {
                        Id = item.Id,
                        Name = item.Name,
                    });
                }
                return new Response<CodePostalInegeModel>
                {
                    Result = new CodePostalInegeModel
                    {
                        City = new GenericModel
                        {
                            Id = result.FirstOrDefault().City.Id,
                            Name = result.FirstOrDefault().City.Name
                        },
                        State = new GenericModel
                        {
                            Id = result.FirstOrDefault().City.State.Id,
                            Name = result.FirstOrDefault().City.State.Name,
                        },
                        ListColony = list
                    },
                    Count = list.Count()
                };
            }
            catch(Exception ex)
            {
                await errorBL.LogError(ex);
                return new Response<CodePostalInegeModel>
                {
                    Result = null,
                    Message = "Hubo un error, intente más tarde"
                };
            }
        }

        public async Task<Response<bool>> GetCodePostalInsert()
        {
            try
            {
                using (StreamReader r = new StreamReader("C:\\PERSONAL\\Barber.Colocho\\Barber.Colocho\\WEB\\Barber.Colocho.Web\\Helpers\\ZipCodesMX.json"))
                {
                    string json = r.ReadToEnd();
                    var list = JsonConvert.DeserializeObject<List<Demo>>(json);
                    var groupEstado = list.GroupBy(c => c.State);
                    foreach (var item in groupEstado)
                    {
                        var st = await state.AddAsync(new Entities.Tables.State
                        {
                            Created = DateTime.UtcNow,
                            IsActive = true,
                            Name = item.Key,
                            Updated = null,
                            Delete = null,
                        });
                        var groupCity = item.GroupBy(c => c.Municipality);
                        foreach(var itemM in groupCity)
                        {
                            var ct = await city.AddAsync(new Entities.Tables.City
                            {
                                Created = DateTime.UtcNow,
                                IsActive = true,
                                Name = itemM.Key,
                                Updated = null,
                                Delete = null,
                                IdState = st.Id,
                            });
                            var lColony = new List<Entities.Tables.Colony>();
                            foreach (var itemC in itemM)
                            {
                                lColony.Add(new Entities.Tables.Colony
                                {
                                    CodePostal = itemC.Zipcode,
                                    Created = DateTime.UtcNow,
                                    IsActive = true,
                                    Name = itemC.Colony,
                                    Updated = null,
                                    Delete = null,
                                    IdCity = ct.Id,
                                });
                            }
                            await colony.AddRangeAsync(lColony);
                        }
                    }
                }
                return new Response<bool>
                {
                    Result = true
                };
            }
            catch(Exception ex)
            {
                await errorBL.LogError(ex);
                return new Response<bool>
                {
                    Result = false,
                };
            }
        }
    }
}
