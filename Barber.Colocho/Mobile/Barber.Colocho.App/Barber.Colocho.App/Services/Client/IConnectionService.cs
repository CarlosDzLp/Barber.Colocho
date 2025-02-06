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
    [Headers("Content-Type: application/json;", "Api-Version: 1.0", "accept: */*")]
    public interface IConnectionService
    {
        #region Address
        [Post("/api/v1/Address/{UserId}/AddAddressUser")]
        Task<ApiResponse<Response<bool>>> AddAddressUser(int UserId, [Body] AddAdressModel query, [Header("Authorization")] string authorization);

        [Get("/api/v1/Address/{UserId}/DeleteAddress/{IdAddress}")]
        Task<ApiResponse<Response<bool>>> DeleteAddress(int UserId, int IdAddress, [Header("Authorization")] string authorization);

        [Get("/api/v1/Address/{UserId}/GetListAddressByUserId")]
        Task<ApiResponse<Response<List<AddressModel>>>> GetListAddressByUserId(int UserId, [Header("Authorization")] string authorization);

        [Get("/api/v1/Address/{UserId}/GetAddressById/{IdAddress}")]
        Task<ApiResponse<Response<AddressModel>>> GetAddressById(int UserId, int IdAddress, [Header("Authorization")] string authorization);

        [Get("/api/v1/Address/{UserId}/UpdateDefaultAddress/{IdAddress}")]
        Task<ApiResponse<Response<bool>>> UpdateDefaultAddress(int UserId, int IdAddress, [Header("Authorization")] string authorization);

        [Post("/api/v1/Address/{UserId}/UpdateAddress/{IdAddress}")]
        Task<ApiResponse<Response<bool>>> UpdateAddress(int UserId, int IdAddress, [Body] AddAdressModel query, [Header("Authorization")] string authorization);
        #endregion

        #region Company
        [Post("/api/v1/Company/{UserId}/AddCompany")]
        Task<ApiResponse<Response<bool>>> AddCompany(int UserId, [Body] AddCompanyModel query, [Header("Authorization")] string authorization);

        [Get("/api/v1/Company/{UserId}/Company/{CompanyId}/SuscripcionActiveCompany")]
        Task<ApiResponse<Response<bool>>> SuscripcionActiveCompany(int UserId, int CompanyId, [Header("Authorization")] string authorization);

        [Get("/api/v1/Company/{UserId}/DeleteCompany/{CompanyId}")]
        Task<ApiResponse<Response<bool>>> DeleteCompany(int UserId, int CompanyId, [Header("Authorization")] string authorization);

        [Post("/api/v1/Company/{UserId}/GetListCompany")]
        Task<ApiResponse<Response<List<CompanyModel>>>> GetListCompany(int UserId, [Header("Authorization")] string authorization);

        [Post("/api/v1/Company/{UserId}/GetCompany/{CompanyId}")]
        Task<ApiResponse<Response<CompanyModel>>> GetCompany(int UserId, int CompanyId, [Header("Authorization")] string authorization);

        [Post("/api/v1/Company/{UserId}/UpdateCompany/{CompanyId}/")]
        Task<ApiResponse<Response<bool>>> UpdateCompany(int UserId, int CompanyId, [Body] UpdateCompanyModel query, [Header("Authorization")] string authorization);

        [Multipart]
        [Post("/api/v1/Company/{UserId}/AddImageCompany/{CompanyId}")]
        Task<ApiResponse<Response<bool>>> UpdateUserImage(int UserId, int CompanyId, [AliasAs("File")] StreamContent File, [Header("Authorization")] string authorization);

        [Get("/api/v1/Company/{UserId}/Company/{CompanyId}/DeleteImageCompany/{ImageId}")]
        Task<ApiResponse<Response<bool>>> DeleteImageCompany(int UserId, int CompanyId, int ImageId, [Header("Authorization")] string authorization);
        #endregion

        #region OAuth
        [Post("/api/v1/OAuth/Authenticate")]
        Task<ApiResponse<Response<TokenResponseModel>>> Authenticate([Body] AuthenticateModel query);
        #endregion

        #region Plan
        [Get("/api/v1/Plan/GetPlan")]
        Task<ApiResponse<Response<List<PlanModel>>>> ListPlan([Body] AuthenticateModel query);
        #endregion

        #region Service
        [Post("/api/v1/Service/{UserId}/Company/{CompanyId}/AddService")]
        Task<ApiResponse<Response<bool>>> AddService(int UserId, int CompanyId, [Body] AddServiceModel query, [Header("Authorization")] string authorization);

        [Get("/api/v1/Service/{UserId}/Company/{CompanyId}/Service/{ServiceId}/DeleteService")]
        Task<ApiResponse<Response<bool>>> DeleteService(int UserId, int CompanyId, int ServiceId, [Header("Authorization")] string authorization);

        [Get("/api/v1/Service/{UserId}/Service/{CompanyId}/ImageService/{ImageServiceId}/DeleteImageService")]
        Task<ApiResponse<Response<bool>>> DeleteImageService(int UserId, int CompanyId, int ImageServiceId, [Header("Authorization")] string authorization);

        [Post("/api/v1/Service/{UserId}/Company/{CompanyId}/Service/{ServceId}/AddImageService")]
        Task<ApiResponse<Response<bool>>> AddImageService(int UserId, int CompanyId, int ServceId, [AliasAs("File")] StreamContent File, [Header("Authorization")] string authorization);

        [Post("/api/v1/Service/{UserId}/Company/{CompanyId}/Service/{ServiceId}/UpdateService")]
        Task<ApiResponse<Response<bool>>> UpdateService(int UserId, int CompanyId, int ServiceId, [Body] UpdateServiceModel query, [Header("Authorization")] string authorization);

        [Get("/api/v1/Service/{UserId}/Company/{CompanyId}/GetListServiceByIdCompany")]
        Task<ApiResponse<Response<List<ServiceModel>>>> ListServiceByIdCompany(int UserId, int CompanyId, [Header("Authorization")] string authorization);

        [Get("/api/v1/Service/{UserId}/Company/{CompanyId}/Service/{ServiceId}/GetServiceByIdService")]
        Task<ApiResponse<Response<ServiceModel>>> ServiceByIdService(int UserId, int CompanyId, int ServiceId, [Header("Authorization")] string authorization);

        [Get("/api/v1/Service/GetListServiceAllUser/{Page}")]
        Task<ApiResponse<Response<List<ServiceModel>>>> ListServiceAllUser(int Page);

        [Get("/api/v1/Service/{CompanyId}/Service/{ServiceId}/GetServiceAllUser")]
        Task<ApiResponse<Response<ServiceModel>>> ServiceAllUser(int CompanyId, int ServiceId);
        #endregion

        #region User
        [Post("/api/v1/User/AddUser")]
        Task<ApiResponse<Response<bool>>> Register([Body] AddUserModel query);

        [Post("/api/v1/User/{UserId}/SendCodeUser")]
        Task<ApiResponse<Response<bool>>> SendCodeUser(int UserId, [Body] SendCodeUserModel query);

        [Post("/api/v1/User/{UserId}/ResendCodeUser")]
        Task<ApiResponse<Response<bool>>> ResendCodeUser(int UserId, [Body] SendCodeUserModel query);

        [Post("/api/v1/User/SendCodeForgotPassword")]
        Task<ApiResponse<Response<bool>>> SendCodeForgotPassword([Body] SendCodeForgotPasswordModel query);

        [Post("/api/v1/User/ForgotPassword")]
        Task<ApiResponse<Response<bool>>> ForgotPassword([Body] ForgotPasswordModel query);

        [Post("/api/v1/User/{UserId}/ChangeRolUser")]
        Task<ApiResponse<Response<bool>>> ChangeRolUser(int UserId, [Header("Authorization")] string authorization);

        [Multipart]
        [Post("/api/v1/User/{UserId}/UpdateUserImage")]
        Task<ApiResponse<Response<bool>>> UpdateUserImage(int UserId, [AliasAs("File")] StreamContent File, [Header("Authorization")] string authorization);

        [Post("/api/v1/User/{UserId}/UpdateUser")]
        Task<ApiResponse<Response<bool>>> UpdateUser(int UserId, [Body] UpdateUserModel query, [Header("Authorization")] string authorization);

        [Get("/api/v1/User/{UserId}/GetUser")]
        Task<ApiResponse<Response<UserModel>>> GetUser(int UserId, [Header("Authorization")] string authorization);

        [Get("/api/v1/User/{UserId}/DeleteAccount")]
        Task<ApiResponse<Response<bool>>> DeleteAccount(int UserId, [Header("Authorization")] string authorization);

        [Get("/api/v1/User/{UserId}/Company/{CompanyId}/AddFavorite")]
        Task<ApiResponse<Response<bool>>> AddFavorite(int UserId, int CompanyId, [Header("Authorization")] string authorization);

        [Get("/api/v1/User/{UserId}/Company/{CompanyId}/DeleteFavorite/{FavoriteId}")]
        Task<ApiResponse<Response<bool>>> DeleteFavorite(int UserId, int CompanyId, int FavoriteId, [Header("Authorization")] string authorization);

        [Get("/api/v1/User/{UserId}/Favorite")]
        Task<ApiResponse<Response<List<CompanyModel>>>> Favorite(int UserId, [Header("Authorization")] string authorization);

        [Get("/api/v1/User/{UserId}/Company/{CompanyId}/Plan/{PlanId}/SuscribePlan")]
        Task<ApiResponse<Response<bool>>> SuscribePlan(int UserId, int CompanyId, int PlanId, [Header("Authorization")] string authorization);
        #endregion


        #region INEGI
        [Get("/api/v1/Inegi/GetState")]
        Task<ApiResponse<Response<List<GenericModel>>>> GetState();

        [Get("/api/v1/Inegi/GetCity?IdState={Id}")]
        Task<ApiResponse<Response<List<GenericModel>>>> GetCity(int Id);

        [Get("/api/v1/Inegi/GetColony?IdCity={Id}")]
        Task<ApiResponse<Response<List<GenericModel>>>> GetColony(int Id);

        [Get("/api/v1/Inegi/GetCodePostal?CodePostal={CP}")]
        Task<ApiResponse<Response<CodePostalInegeModel>>> GetCodePostal(string CP);
        #endregion
    }
}
