using Barber.Colocho.Core.Error;
using Barber.Colocho.Core.Helpers;
using Barber.Colocho.Entities.Tables;
using Barber.Colocho.Models.Authenticate;
using Barber.Colocho.Repository.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Barber.Colocho.Core.Auth
{
    public class AuthenticateBL
    {

        #region Constructor
        private readonly AuthService authService;
        private readonly ILogger<RefreshTokenModel> logger;
        private readonly IGenericRepository<Entities.Tables.RefreshToken> refreshToken;
        private readonly IGenericRepository<Entities.Tables.User> userManager;
        private readonly ErrorBL errorBL;

        public AuthenticateBL(AuthService authService, ILogger<RefreshTokenModel> logger, IGenericRepository<Entities.Tables.RefreshToken> refreshToken, IGenericRepository<Entities.Tables.User> userManager, ErrorBL errorBL)
        {
            this.authService = authService;
            this.logger = logger;
            this.refreshToken = refreshToken;
            this.userManager = userManager;
            this.errorBL = errorBL;
        }
        #endregion

        #region RefreshToken
        public async Task<Response<RefreshTokenResponseModel>> RefreshToken(RefreshTokenModel request)
        {
            try
            {
                var refresh = await refreshToken.FindAsync(c => c.RefreshTokenValue == request.RefreshToken);

                // Refresh token no existe, expiró o fue revocado manualmente
                // (Pensando que el usuario puede dar click en "Cerrar Sesión en todos lados" o similar)
                if (refresh is null ||
                    refresh.IsActive == false ||
                    refresh.Expiration <= DateTime.UtcNow)
                {
                    return new Response<RefreshTokenResponseModel>
                    {
                        Result = null,
                        Message = "No se ha encontrado el dato"
                    };
                }

                // Se está intentando usar un Refresh Token que ya fue usado anteriormente,
                // puede significar que este refresh token fue robado.
                if (refresh.Used)
                {
                    logger.LogWarning("El refresh token del {UserId} ya fue usado. RT={RefreshToken}", refresh.UserId, refresh.RefreshTokenValue);

                    var refreshTokens = await refreshToken.GetAllAsync(c => c.IsActive && c.Used == false && c.UserId == refresh.UserId);

                    foreach (var rt in refreshTokens)
                    {
                        rt.Used = true;
                        rt.IsActive = false;
                        await refreshToken.UpdateAsync(rt);
                    }

                    return new Response<RefreshTokenResponseModel>
                    {
                        Result = null,
                        Message = "No se ha encontrado el dato"
                    };
                }

                // TODO: Podríamos validar que el Access Token sí corresponde al mismo usuario

                refresh.Used = true;

                var user = await userManager.FindAsync(c => c.Id == refresh.UserId);

                if (user is null)
                {
                    return new Response<RefreshTokenResponseModel>
                    {
                        Result = null,
                        Message = "No se ha encontrado el dato"
                    };
                }

                var jwt = await authService.GenerateAccessToken(user);

                var newRefreshToken = new RefreshToken
                {
                    IsActive = true,
                    Expiration = DateTime.UtcNow.AddDays(7),
                    RefreshTokenValue = Guid.NewGuid().ToString("N"),
                    Used = false,
                    UserId = user.Id,
                    Created = DateTime.UtcNow,
                };

                await refreshToken.AddAsync(newRefreshToken);

                return new Response<RefreshTokenResponseModel>
                {
                    Result = new RefreshTokenResponseModel
                    {
                        AccessToken = jwt,
                        RefreshToken = newRefreshToken.RefreshTokenValue
                    },
                    Count = 1
                };
            }
            catch(Exception ex)
            {
                await errorBL.LogError(ex);
                return new Response<RefreshTokenResponseModel>
                {
                    Result = null,
                    Message = "Hubo un error, intente, más tarde"
                };
            }
        }
        #endregion

        #region Token
        public async Task<Response<TokenResponseModel>> Token(TokenModel request)
        {
            try
            {
                var user = await userManager.FindAsync(c => c.IsActive && c.Phone == request.Phone, include: query => query.Include(u => u.Roles));
                if (user is null || !PasswordHash.ValidatePassword(request.Password, user.Password))
                {
                    return new Response<TokenResponseModel>
                    {
                        Result = null,
                        Message = "No se ha encontrado el dato"
                    };
                }
                if (!user.IsConfirmed)
                    return new Response<TokenResponseModel>
                    {
                        Result = null,
                        Message = "No se pudo encontrar el usuario"
                    };


                var jwt = await authService.GenerateAccessToken(user);
                var newAccessToken = new RefreshToken
                {
                    IsActive = true,
                    Expiration = DateTime.UtcNow.AddDays(7),
                    RefreshTokenValue = Guid.NewGuid().ToString("N"),
                    Used = false,
                    UserId = user.Id,
                    Created = DateTime.UtcNow,
                };
                await refreshToken.AddAsync(newAccessToken);
                return new Response<TokenResponseModel>
                {
                    Result = new TokenResponseModel
                    {
                        UserId = user.Id,
                        AccessToken = jwt,
                        RefreshToken = newAccessToken.RefreshTokenValue
                    },
                    Count = 1
                };
            }
            catch(Exception ex)
            {
                await errorBL.LogError(ex);
                return new Response<TokenResponseModel>
                {
                    Result = null,
                    Message = "Hubo un error, intente más tarde"
                };
            }
        }
        #endregion
    }
}
