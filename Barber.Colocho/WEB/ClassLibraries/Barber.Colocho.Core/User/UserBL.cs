using Barber.Colocho.Core.Error;
using Barber.Colocho.Core.Helpers;
using Barber.Colocho.Core.RulesValidations;
using Barber.Colocho.Entities.Tables;
using Barber.Colocho.Models.Code;
using Barber.Colocho.Models.Company;
using Barber.Colocho.Models.ForgotPassword;
using Barber.Colocho.Models.Generic;
using Barber.Colocho.Models.User;
using Barber.Colocho.Repository.Repository;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Barber.Colocho.Core.User
{
    public class UserBL
    {
        #region Constructor
        private readonly IGenericRepository<Entities.Tables.User> user;
        private readonly IGenericRepository<Entities.Tables.Roles> roles;
        private readonly IGenericRepository<Entities.Tables.Code> code;
        private readonly IGenericRepository<Entities.Tables.Company> company;
        private readonly IGenericRepository<Entities.Tables.Favorite> favorite;
        private readonly IGenericRepository<Entities.Tables.Plan> plan;
        private readonly IGenericRepository<Entities.Tables.Suscription> suscription;
        private readonly ErrorBL errorBL;
        private readonly UploadImage uploadImage;

        public UserBL(IGenericRepository<Entities.Tables.User> user,IGenericRepository<Entities.Tables.Roles> roles,IGenericRepository<Entities.Tables.Code> code,IGenericRepository<Entities.Tables.Company> company,IGenericRepository<Entities.Tables.Favorite> favorite,IGenericRepository<Entities.Tables.Plan> plan,IGenericRepository<Entities.Tables.Suscription> suscription, ErrorBL errorBL, UploadImage uploadImage)
        {
            this.user = user;
            this.roles = roles;
            this.code = code;
            this.company = company;
            this.favorite = favorite;
            this.plan = plan;
            this.suscription = suscription;
            this.errorBL = errorBL;
            this.uploadImage = uploadImage;
        }
        #endregion
        public async Task<Response<bool>> AddUserAsync(AddUserModel model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Name))
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "Debe agregar el nombre"
                    };
                if (string.IsNullOrWhiteSpace(model.LastName))
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "Debe agregar los apellidos"
                    };
                if (!FluentValidation.IsPassword(model.Password))
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "Debe agregar una contraseña"
                    };
                if (!FluentValidation.IsEmail(model.Email))
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "Correo invalido"
                    };
                if (!FluentValidation.IsPhone(model.Phone))
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "Teléfono invalido"
                    };
                var searchEmail = await user.FindAsync(c => c.Email == model.Email);//se busca el correo
                var searchPhone = await user.FindAsync(c => c.Phone == model.Phone);//se busca el telefono
                string codeString = RandomString(5);
                var search = await user.FindAsync(c => c.Phone == model.Phone && c.Email == model.Email);
                if (search != null)
                {
                    if (search.IsActive)
                    {
                        if (search.IsConfirmed)
                        {
                            return new Response<bool>
                            {
                                Result = false,
                                Message = "Ya existe una cuenta"
                            };
                        }
                        else
                        {
                            await ValidateCode(search.Id);
                            await code.AddAsync(new Code
                            {
                                CodeName = codeString,
                                Created = DateTime.UtcNow,
                                Delete = null,
                                Expired = DateTime.UtcNow.AddMinutes(10),
                                IsActive = true,
                                Updated = null,
                                UserId = search.Id,
                            });
                            await SMS.SendTwilio(model.Phone, codeString);
                            return new Response<bool>
                            {
                                Result = true,
                                Message = "Se ha enviado el codigo de confirmación",
                                Count = search.Id
                            };
                        }
                    }
                    else
                        return new Response<bool>
                        {
                            Result = false,
                            Message = "La cuenta ya existe pero está desactivada, contacte con soporte"
                        };
                }
                else
                {
                    if (searchEmail != null)
                        return new Response<bool>
                        {
                            Result = false,
                            Message = "El correo existe en otra cuenta"
                        };

                    if (searchPhone != null)
                        return new Response<bool>
                        {
                            Result = false,
                            Message = "El teléfono existe en otra cuenta"
                        };
                    var rol = await roles.FindAsync(c => c.RolName == Enums.RolesEnum.User && c.IsActive);
                    var us = await user.AddAsync(new Entities.Tables.User
                    {
                        Name = model.Name,
                        Created = DateTime.UtcNow,
                        Delete = null,
                        Email = model.Email,
                        IsActive = true,
                        IsConfirmed = false,
                        LastName = model.LastName,
                        Password = PasswordHash.CreateHash(model.Password),
                        Phone = model.Phone,
                        Updated = null,
                        RolId = rol.Id,
                        Image = string.Empty
                    });
                    if (us != null && us.Id > 0)
                    {
                        await code.AddAsync(new Code
                        {
                            CodeName = codeString,
                            Created = DateTime.UtcNow,
                            Delete = null,
                            Expired = DateTime.UtcNow.AddMinutes(10),
                            IsActive = true,
                            Updated = null,
                            UserId = us.Id,
                        });
                        await SMS.SendTwilio(model.Phone, codeString);
                        return new Response<bool>
                        {
                            Result = true,
                            Message = "Se ha creado el usuario",
                            Count = us.Id
                        };
                    }

                    else
                        return new Response<bool>
                        {
                            Result = false,
                            Message = "No se pudo crear el usuario"
                        };
                }
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

        private async Task ValidateCode(int idUser)
        {
            try
            {
                var search = await code.GetAllAsync(c => c.UserId == idUser);
                foreach (var item in search) 
                {
                    item.IsActive = false;
                    item.Delete = DateTime.UtcNow;
                    await code.UpdateAsync(item);
                }
            }
            catch(Exception ex)
            {
                await errorBL.LogError(ex);
            }
        }

        public string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "01234567890987654321";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public async Task<Response<bool>> SendCodeUser(CodeModel co, int userId)
        {
            try
            {
                var cod = await code.FindAsync(c => c.IsActive && c.UserId == userId && c.CodeName == co.Code);
                if (cod == null)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "No se ha encontrado el codigo"
                    };
                var date = DateTime.UtcNow;
                if (date > cod.Expired)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "El código ha expirado"
                    };
                var us = await user.FindAsync(c => c.IsActive && c.Id == userId);
                if (us == null)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "No se ha encontrado el usuario"
                    };
                us.IsConfirmed = true;
                await user.UpdateAsync(us);
                await ValidateCode(us.Id);
                return new Response<bool>
                {
                    Result = true,
                    Message = "Se ha guardado el usuario",
                    Count = 1
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

        public async Task<Response<bool>> ResendCodeUser(CodeModel co,int userId)
        {
            try
            {
                var us = await user.FindAsync(c => c.IsActive && c.Id == userId);
                if (us == null)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "No se ha encontrado el usuario"
                    };
                await ValidateCode(userId);
                string codeString = RandomString(5);
                var codAdd = await code.AddAsync(new Code
                {
                    Created = DateTime.UtcNow,
                    CodeName = codeString,
                    Delete = null,
                    Expired = DateTime.UtcNow.AddMinutes(10),
                    IsActive = true,
                    Updated = null,
                    UserId = userId
                });
                if (codAdd != null && codAdd.Id > 0)
                {
                    await SMS.SendTwilio(us.Phone, codeString);
                    return new Response<bool>
                    {
                        Result = true,
                        Message = "Se ha reenviado el nuevo código",
                        Count = 1
                    };
                }                  
                else
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "No se pudo generar el código"
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

        public async Task<Response<bool>> ChangeRolUser(int userId)
        {
            try
            {
                var us = await user.FindAsync(c => c.IsActive && c.IsConfirmed && c.Id == userId);
                if (us == null)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "No se encontró el usuario"
                    };
                var ro = await roles.FindAsync(c => c.IsActive && c.RolName == Enums.RolesEnum.UserAdmin);
                if (ro == null)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "No se encontró el usuario"
                    };

                us.RolId = ro.Id;
                us.Updated = DateTime.UtcNow;
                await user.UpdateAsync(us);
                return new Response<bool>
                {
                    Result = true,
                    Count = 1,
                    Message = "Se ha cambiado el tipo de usuario"
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

        public async Task<Response<UserModel>> GetUser(int userId)
        {
            try
            {
                var us = await user.FindAsync(c => c.IsConfirmed && c.IsActive && c.Id == userId, include: query => query.Include(r => r.Roles));
                if (us == null)
                    return new Response<UserModel>
                    {
                        Result = null,
                        Message = "No se encontró el usuario"
                    };
                return new Response<UserModel>
                {
                    Result = new UserModel
                    {
                        Name = us.Name,
                        Email = us.Email,
                        Phone = us.Phone,
                        Id = us.Id,
                        LastName = us.LastName,
                        RolName = us.Roles.RolName,
                        Image = (string.IsNullOrWhiteSpace(us.Image)) ? string.Empty : uploadImage.GetFile(us.Image, "User"),
                    },
                    Count = 1
                };
            }
            catch(Exception ex)
            {
                await errorBL.LogError(ex);
                return new Response<UserModel>
                {
                    Result = null,
                    Message = "Hubo un error, intente más tarde"
                };
            }
        }

        public async Task<Response<bool>> UpdateUserImage(int userId, AddImageModel model)
        {
            try
            {
                var searchUser = await user.FindAsync(c => c.IsConfirmed && c.IsActive && c.Id == userId);
                if (searchUser == null)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "No se encontró el usuario"
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
                string fileNameOld = searchUser.Image;
                var stm = model.File.OpenReadStream();
                using (MemoryStream ms = new MemoryStream())
                {
                    stm.CopyTo(ms);
                    var img = ms.ToArray();
                    string name =
                    await uploadImage.Upload(img, "User", fileName);
                    await uploadImage.Remove("User", fileNameOld);
                }
                searchUser.Updated = DateTime.UtcNow;
                searchUser.Image = fileName;
                await user.UpdateAsync(searchUser);
                return new Response<bool>
                {
                    Result = true,
                    Message = "Se ha guardado la imagen"
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

        public async Task<Response<bool>> UpdateUser(int userId, UpdateUserModel model)
        {
            try
            {
                var searchUser = await user.FindAsync(c => c.IsConfirmed && c.IsActive && c.Id == userId);
                var searchEmail = await user.FindAsync(c => c.Email == model.Email && c.Id != userId);
                if (searchEmail != null)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "El correo ya existe en otra cuenta"
                    };
                if (searchUser == null)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "No se encontró el usuario"
                    };
                if (string.IsNullOrWhiteSpace(model.Name))
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "Debe agregar el nombre"
                    };
                if (string.IsNullOrWhiteSpace(model.LastName))
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "Debe agregar los apellidos"
                    };
                if (!FluentValidation.IsEmail(model.Email))
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "El correo no es válido"
                    };
                //if (!FluentValidation.IsPassword(model.Password))
                //    return new Response<bool>
                //    {
                //        Result = false,
                //        Message = "Debe agregar la contraseña"
                //    };
                //searchUser.Password = PasswordHash.CreateHash(model.Password);
                searchUser.Name = model.Name;
                searchUser.LastName = model.LastName;
                searchUser.Email = model.Email;
                await user.UpdateAsync(searchUser);
                return new Response<bool>
                {
                    Result = true,
                    Message = "Se ha actualizado los datos del usuario"
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

        public async Task<Response<bool>> DeleteAccount(int userId)
        {
            try
            {
                var searchUser = await user.FindAsync(c => c.IsConfirmed && c.IsActive && c.Id == userId);
                if (searchUser == null)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "No se encontró la cuenta"
                    };
                searchUser.Delete = DateTime.UtcNow;
                searchUser.IsActive = false;
                await user.UpdateAsync(searchUser);
                return new Response<bool>
                {
                    Result = true,
                    Message = "Se ha eliminado la cuenta"
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

        public async Task<Response<bool>> AddFavorite(int userId, int companyId)
        {
            try
            {
                var searchCompany = await company.FindAsync(c => c.IsActive && c.Id == companyId);
                if (searchCompany == null)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "No se encontró la empresa"
                    };
                var add = await favorite.AddAsync(new Favorite
                {
                    IsActive = true,
                    CompanyId = companyId,
                    Delete = null,
                    Updated = null,
                    Created = DateTime.UtcNow,
                    UserId = userId
                });
                if (add != null && add.Id > 0)
                    return new Response<bool>
                    {
                        Result = true,
                        Message = "Se ha agregado a favoritos"
                    };
                else
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "No se pudo agregar a favoritos"
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

        public async Task<Response<bool>> DeleteFavorite(int userId, int companyId,int favoriteId)
        {
            try
            {
                var searchfavorite = await favorite.FindAsync(c => c.IsActive && c.UserId == userId && c.CompanyId == companyId && c.Id == favoriteId);
                if (searchfavorite == null)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "No se encontro el favorito"
                    };
                searchfavorite.IsActive = false;
                searchfavorite.Delete = DateTime.UtcNow;
                await favorite.UpdateAsync(searchfavorite);
                return new Response<bool>
                {
                    Result = true,
                    Message = "Se ha eliminado de favoritos"
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

        public async Task<Response<List<GetCompanyModel>>> Favorite(int userId)
        {
            try
            {
                var list = new List<GetCompanyModel>();
                var searchFavorite = await user.FindAsync(c => c.IsActive && c.Id == userId, include: query => query.Include(c => c.Favorite).ThenInclude(c => c.Company).ThenInclude(i => i.Image).Include(c => c.Company).ThenInclude(c => c.Service).ThenInclude(c => c.RatingService));
                if (searchFavorite != null && searchFavorite.Favorite != null && searchFavorite.Favorite.Count > 0)
                {
                    foreach (var item in searchFavorite.Favorite)
                    {
                        var listImg = new List<GenericModel>();
                        foreach (var itemI in item.Company.Image)
                        {
                            listImg.Add(new GenericModel
                            {
                                Id = itemI.Id,
                                Name = uploadImage.GetFile(itemI.Url, "Company")
                            });
                        }
                        var listService = item.Company.Service.Where(c => c.IsActive);
                        // Usando LINQ para calcular el promedio de los ratings activos en listas activas
                        var activeRatings = listService
                            .Where(l => l.IsActive)  // Filtra las listas activas
                            .SelectMany(l => l.RatingService)  // Aplana la lista de objetos lista2
                            .Where(n => n.IsActive)  // Filtra los items activos de lista2
                            .Select(n => n.Rating);  // Selecciona solo los ratings

                        double rating = activeRatings.Any() ? activeRatings.Average() : 0;

                        list.Add(new GetCompanyModel
                        {
                            Name = item.Company.Name,
                            RFC = item.Company.RFC,
                            Description = item.Company.Description,
                            Id = item.CompanyId,
                            ListImage = listImg,
                            Created = item.Created,
                            Rating = rating
                        });
                    }
                    return new Response<List<GetCompanyModel>>
                    {
                        Result = list,
                        Count = list.Count
                    };
                }
                else
                    return new Response<List<GetCompanyModel>>
                    {
                        Result = null,
                        Message = "No hay favoritos"
                    };
            }
            catch(Exception ex)
            {
                await errorBL.LogError(ex);
                return new Response<List<GetCompanyModel>>
                {
                    Result = null,
                    Message = "Hubo un error, intente más tarde"
                };
            }
        }

        public async Task<Response<bool>> Suscription(int userId, int companyId, int planId)
        {
            try
            {
                var validateSus = await suscription.FindAsync(c => c.IsActive && c.CompanyId == companyId);
                if (validateSus != null)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "Ya cuenta con una suscrpción activa"
                    };
                var searchUser = await user.FindAsync(c => c.IsConfirmed && c.IsActive && c.Id == userId);
                if (searchUser == null)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "Usuario no encontrado"
                    };
                var searchCompany = await company.FindAsync(c => c.IsActive && c.Id == companyId);
                if (searchCompany == null)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "Empresa no encontrada"
                    };
                var searchPlan = await plan.FindAsync(c => c.IsActive && c.Id == planId);
                if (searchCompany == null)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "No se encontró el plan"
                    };
                var dateInit = DateTime.UtcNow;
                var dateFinsh = DateTime.UtcNow.AddMonths(1);
                var add = await suscription.AddAsync(new Entities.Tables.Suscription
                {
                    CompanyId = companyId,
                    Created = DateTime.UtcNow,
                    PlanId = planId,
                    Delete = null,
                    IsActive = true,
                    Price = searchPlan.Price,
                    Updated = null,
                    InitSuscription = dateInit,
                    FinishSuscription = dateFinsh,
                });
                if (add != null && add.Id > 0)
                    return new Response<bool>
                    {
                        Result = true,
                        Message = "Se ha creado la suscripcion"
                    };
                else
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "No se ha podido crear la suscripción"
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

        public async Task<Response<bool>> SendCodeForgotPassword(SendCodeForgotPasswordModel vm)
        {
            try
            {
                if (!FluentValidation.IsPhone(vm.Phone))
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "El teléfono no es válido"
                    };
                if (!FluentValidation.IsEmail(vm.Email))
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "El correo no es válido"
                    };

                var resultUser = await user.FindAsync(c => c.Phone == vm.Phone && c.Email == vm.Email);
                if (resultUser == null)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "No se encontró el usuario"
                    };
                if (!resultUser.IsActive)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "El usuario no está activo"
                    };
                if (!resultUser.IsConfirmed)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "El usuario no ha sido confirmado"
                    };
                await ValidateCode(resultUser.Id);
                string codeString = RandomString(5);
                var codAdd = await code.AddAsync(new Code
                {
                    Created = DateTime.UtcNow,
                    CodeName = codeString,
                    Delete = null,
                    Expired = DateTime.UtcNow.AddMinutes(10),
                    IsActive = true,
                    Updated = null,
                    UserId = resultUser.Id,
                });
                if (codAdd != null && codAdd.Id > 0)
                {
                    await SMS.SendTwilio(resultUser.Phone, codeString);
                    return new Response<bool>
                    {
                        Result = true,
                        Message = "Se ha enviado el código, para restablecer la contraseña",
                        Count = resultUser.Id
                    };
                }
                else
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "No se pudo generar el código"
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

        public async Task<Response<bool>> ForgotPassword(ForgotPasswordModel vm)
        {
            try
            {
                if (!FluentValidation.IsPassword(vm.Password))
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "La contraseña es invalida"
                    };
                if (string.IsNullOrWhiteSpace(vm.Code))
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "El código no es valido"
                    };
                if(vm.Code.Length > 5 )
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "El código no es valido"
                    };
                if(vm.Code.Length < 5)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "El código no es valido"
                    };

                var resultUser = await user.FindAsync(c => c.IsActive && c.IsConfirmed && c.Id == vm.UserId);
                if (resultUser == null)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "No se encontró el usuario"
                    };
                var cod = await code.FindAsync(c => c.IsActive && c.UserId == vm.UserId && c.CodeName == vm.Code);
                if (cod == null)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "No se ha encontrado el codigo"
                    };
                var date = DateTime.UtcNow;
                if (date > cod.Expired)
                    return new Response<bool>
                    {
                        Result = false,
                        Message = "El código ha expirado"
                    };
                await ValidateCode(resultUser.Id);

                resultUser.Updated = DateTime.UtcNow;
                resultUser.Password = PasswordHash.CreateHash(vm.Password);
                await user.UpdateAsync(resultUser);
                return new Response<bool>
                {
                    Result = true,
                    Message = "Se ha actualizado la contraseña"
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
