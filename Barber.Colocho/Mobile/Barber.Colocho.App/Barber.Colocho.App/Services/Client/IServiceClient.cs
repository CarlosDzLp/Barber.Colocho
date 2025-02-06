using Barber.Colocho.App.Models.Address;
using Barber.Colocho.App.Models.Authenticate;
using Barber.Colocho.App.Models.Base;
using Barber.Colocho.App.Models.Company;
using Barber.Colocho.App.Models.Inegi;
using Barber.Colocho.App.Models.Plan;
using Barber.Colocho.App.Models.Profile;
using Barber.Colocho.App.Models.Service;
using Barber.Colocho.App.Models.User;
using Refit;

namespace Barber.Colocho.App.Services.Client
{
    public interface IServiceClient
    {
        #region Address
        Task<Response<bool>> AddAddressUser(int UserId, AddAdressModel query, string authorization);

        Task<Response<bool>> DeleteAddress(int UserId, int IdAddress, string authorization);

        Task<Response<List<AddressModel>>> GetListAddressByUserId(int UserId, string authorization);

        Task<Response<AddressModel>> GetAddressById(int UserId, int IdAddress, string authorization);

        Task<Response<bool>> UpdateDefaultAddress(int UserId, int IdAddress, string authorization);

        Task<Response<bool>> UpdateAddress(int UserId, int IdAddress, AddAdressModel query, string authorization);
        #endregion

        #region Company
        Task<Response<bool>> AddCompany(int UserId, AddCompanyModel query, string authorization);

        Task<Response<bool>> SuscripcionActiveCompany(int UserId, int CompanyId, string authorization);

        Task<Response<bool>> DeleteCompany(int UserId, int CompanyId, string authorization);

        Task<Response<List<CompanyModel>>> GetListCompany(int UserId, string authorization);

        Task<Response<CompanyModel>> GetCompany(int UserId, int CompanyId, string authorization);

        Task<Response<bool>> UpdateCompany(int UserId, int CompanyId, UpdateCompanyModel query, string authorization);

        Task<Response<bool>> UpdateUserImage(int UserId, int CompanyId, StreamContent File, string authorization);

        Task<Response<bool>> DeleteImageCompany(int UserId, int CompanyId, int ImageId, string authorization);
        #endregion

        #region OAuth
        Task<Response<TokenResponseModel>> Authenticate(AuthenticateModel query);
        #endregion

        #region Plan
        Task<Response<List<PlanModel>>> ListPlan(AuthenticateModel query);
        #endregion

        #region Service
        Task<Response<bool>> AddService(int UserId, int CompanyId, AddServiceModel query, string authorization);

        Task<Response<bool>> DeleteService(int UserId, int CompanyId, int ServiceId, string authorization);

        Task<Response<bool>> DeleteImageService(int UserId, int CompanyId, int ImageServiceId, string authorization);

        Task<Response<bool>> AddImageService(int UserId, int CompanyId, int ServceId, StreamContent File, string authorization);

        Task<Response<bool>> UpdateService(int UserId, int CompanyId, int ServiceId, UpdateServiceModel query, string authorization);

        Task<Response<List<ServiceModel>>> ListServiceByIdCompany(int UserId, int CompanyId, string authorization);

        Task<Response<ServiceModel>> ServiceByIdService(int UserId, int CompanyId, int ServiceId, string authorization);

        Task<Response<List<ServiceModel>>> ListServiceAllUser(int Page);

        Task<Response<ServiceModel>> ServiceAllUser(int CompanyId, int ServiceId);
        #endregion

        #region User
        Task<Response<bool>> Register(AddUserModel query);

        Task<Response<bool>> SendCodeUser(int UserId, SendCodeUserModel query);

        Task<Response<bool>> ResendCodeUser(int UserId, SendCodeUserModel query);

        Task<Response<bool>> SendCodeForgotPassword(SendCodeForgotPasswordModel query);

        Task<Response<bool>> ForgotPassword(ForgotPasswordModel query);

        Task<Response<bool>> ChangeRolUser(int UserId, string authorization);

        Task<Response<bool>> UpdateUserImage(int UserId, StreamContent File, string authorization);

        Task<Response<bool>> UpdateUser(int UserId, UpdateUserModel query, string authorization);

        Task<Response<UserModel>> GetUser(int UserId, string authorization);

        Task<Response<bool>> DeleteAccount(int UserId, string authorization);

        Task<Response<bool>> AddFavorite(int UserId, int CompanyId, string authorization);

        Task<Response<bool>> DeleteFavorite(int UserId, int CompanyId, int FavoriteId, string authorization);

        Task<Response<List<CompanyModel>>> Favorite(int UserId, string authorization);

        Task<Response<bool>> SuscribePlan(int UserId, int CompanyId, int PlanId, string authorization);
        #endregion

        #region Inegi
        Task<Response<List<GenericModel>>> GetState();

        Task<Response<List<GenericModel>>> GetCity(int IdState);

        Task<Response<List<GenericModel>>> GetColony(int IdCity);

        Task<Response<CodePostalInegeModel>> GetCodePostal(string CodePostal);
        #endregion
    }
}
