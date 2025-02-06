using Barber.Colocho.App.Models.Address;
using Barber.Colocho.App.Models.Authenticate;
using Barber.Colocho.App.Models.Base;
using Barber.Colocho.App.Models.Company;
using Barber.Colocho.App.Models.Inegi;
using Barber.Colocho.App.Models.Plan;
using Barber.Colocho.App.Models.Profile;
using Barber.Colocho.App.Models.Service;
using Barber.Colocho.App.Models.User;
using Barber.Colocho.App.Settings;
using Newtonsoft.Json;
using Refit;
using System.Net;

namespace Barber.Colocho.App.Services.Client
{
    public class ServiceClient : IServiceClient
    {
        #region Properties
        protected HttpClient _httpClient;
        protected RefitSettings _refitSettings;
        protected JsonSerializerSettings _jsonSerializerSettings;
        protected IConnectionService _connectionService;
        #endregion

        #region Constructor
        public ServiceClient()
        {
            _httpClient = new HttpClient(new HttpClientMessageHandler())
            {
                BaseAddress = new Uri(KeyDictionary.URL_BASE),
                Timeout = TimeSpan.FromSeconds(KeyDictionary.TIME_OUT),
            };
            _refitSettings = new RefitSettings();
            _jsonSerializerSettings = new JsonSerializerSettings
            {
                Error = (sender, args) => args.ErrorContext.Handled = true,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                TypeNameHandling = TypeNameHandling.None,
            };
            _refitSettings.ContentSerializer = new NewtonsoftJsonContentSerializer(_jsonSerializerSettings);
            _connectionService = Refit.RestService.For<IConnectionService>(_httpClient, _refitSettings);
        }
        #endregion

        #region Methods
        public async Task<Response<bool>> AddAddressUser(int UserId, AddAdressModel query, string authorization)
        {
            try
            {
                var result = await _connectionService.AddAddressUser(UserId, query, authorization);
                if (result.StatusCode == HttpStatusCode.OK)
                    return result.Content;
                else
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<Response<bool>>(result.Error.Content);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Response<bool>> AddCompany(int UserId, AddCompanyModel query, string authorization)
        {
            try
            {
                var result = await _connectionService.AddCompany(UserId,query,authorization);
                if (result.StatusCode == HttpStatusCode.OK)
                    return result.Content;
                else
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<Response<bool>>(result.Error.Content);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Response<bool>> AddFavorite(int UserId, int CompanyId, string authorization)
        {
            try
            {
                var result = await _connectionService.AddFavorite(UserId,CompanyId,authorization);
                if (result.StatusCode == HttpStatusCode.OK)
                    return result.Content;
                else
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<Response<bool>>(result.Error.Content);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Response<bool>> AddImageService(int UserId, int CompanyId, int ServceId, StreamContent File, string authorization)
        {
            try
            {
                var result = await _connectionService.AddImageService(UserId, CompanyId, ServceId, File, authorization);
                if (result.StatusCode == HttpStatusCode.OK)
                    return result.Content;
                else
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<Response<bool>>(result.Error.Content);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Response<bool>> AddService(int UserId, int CompanyId, AddServiceModel query, string authorization)
        {
            try
            {
                var result = await _connectionService.AddService(UserId, CompanyId, query, authorization);
                if (result.StatusCode == HttpStatusCode.OK)
                    return result.Content;
                else
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<Response<bool>>(result.Error.Content);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Response<TokenResponseModel>> Authenticate(AuthenticateModel query)
        {
            try
            {
                var result = await _connectionService.Authenticate(query);
                if (result.StatusCode == HttpStatusCode.OK)
                    return result.Content;
                else
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<Response<TokenResponseModel>>(result.Error.Content);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Response<bool>> ChangeRolUser(int UserId, string authorization)
        {
            try
            {
                var result = await _connectionService.ChangeRolUser(UserId, authorization);
                if (result.StatusCode == HttpStatusCode.OK)
                    return result.Content;
                else
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<Response<bool>>(result.Error.Content);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Response<bool>> DeleteAccount(int UserId, string authorization)
        {
            try
            {
                var result = await _connectionService.DeleteAccount(UserId, authorization);
                if (result.StatusCode == HttpStatusCode.OK)
                    return result.Content;
                else
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<Response<bool>>(result.Error.Content);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Response<bool>> DeleteAddress(int UserId, int IdAddress, string authorization)
        {
            try
            {
                var result = await _connectionService.DeleteAddress(UserId, IdAddress, authorization);
                if (result.StatusCode == HttpStatusCode.OK)
                    return result.Content;
                else
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<Response<bool>>(result.Error.Content);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Response<bool>> DeleteCompany(int UserId, int CompanyId, string authorization)
        {
            try
            {
                var result = await _connectionService.DeleteCompany(UserId, CompanyId, authorization);
                if (result.StatusCode == HttpStatusCode.OK)
                    return result.Content;
                else
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<Response<bool>>(result.Error.Content);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Response<bool>> DeleteFavorite(int UserId, int CompanyId, int FavoriteId, string authorization)
        {
            try
            {
                var result = await _connectionService.DeleteFavorite(UserId, CompanyId, FavoriteId, authorization);
                if (result.StatusCode == HttpStatusCode.OK)
                    return result.Content;
                else
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<Response<bool>>(result.Error.Content);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Response<bool>> DeleteImageCompany(int UserId, int CompanyId, int ImageId, string authorization)
        {
            try
            {
                var result = await _connectionService.DeleteImageCompany(UserId, CompanyId, ImageId, authorization);
                if (result.StatusCode == HttpStatusCode.OK)
                    return result.Content;
                else
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<Response<bool>>(result.Error.Content);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Response<bool>> DeleteImageService(int UserId, int CompanyId, int ImageServiceId, string authorization)
        {
            try
            {
                var result = await _connectionService.DeleteImageService(UserId, CompanyId, ImageServiceId, authorization);
                if (result.StatusCode == HttpStatusCode.OK)
                    return result.Content;
                else
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<Response<bool>>(result.Error.Content);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Response<bool>> DeleteService(int UserId, int CompanyId, int ServiceId, string authorization)
        {
            try
            {
                var result = await _connectionService.DeleteService(UserId,CompanyId,ServiceId,authorization); ;
                if (result.StatusCode == HttpStatusCode.OK)
                    return result.Content;
                else
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<Response<bool>>(result.Error.Content);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Response<List<CompanyModel>>> Favorite(int UserId, string authorization)
        {
            try
            {
                var result = await _connectionService.Favorite(UserId, authorization);
                if (result.StatusCode == HttpStatusCode.OK)
                    return result.Content;
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Response<bool>> ForgotPassword(ForgotPasswordModel query)
        {
            try
            {
                var result = await _connectionService.ForgotPassword(query);
                if (result.StatusCode == HttpStatusCode.OK)
                    return result.Content;
                else
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<Response<bool>>(result.Error.Content);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Response<AddressModel>> GetAddressById(int UserId, int IdAddress, string authorization)
        {
            try
            {
                var result = await _connectionService.GetAddressById(UserId, IdAddress, authorization);
                if (result.StatusCode == HttpStatusCode.OK)
                    return result.Content;
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Response<List<GenericModel>>> GetCity(int IdState)
        {
            try
            {
                var result = await _connectionService.GetCity(IdState);
                if (result.StatusCode == HttpStatusCode.OK)
                    return result.Content;
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Response<List<GenericModel>>> GetColony(int IdCity)
        {
            try
            {
                var result = await _connectionService.GetColony(IdCity);
                if (result.StatusCode == HttpStatusCode.OK)
                    return result.Content;
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Response<CompanyModel>> GetCompany(int UserId, int CompanyId, string authorization)
        {
            try
            {
                var result = await _connectionService.GetCompany(UserId, CompanyId, authorization);
                if (result.StatusCode == HttpStatusCode.OK)
                    return result.Content;
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Response<List<AddressModel>>> GetListAddressByUserId(int UserId, string authorization)
        {
            try
            {
                var result = await _connectionService.GetListAddressByUserId(UserId, authorization);
                if (result.StatusCode == HttpStatusCode.OK)
                    return result.Content;
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Response<List<CompanyModel>>> GetListCompany(int UserId, string authorization)
        {
            try
            {
                var result = await _connectionService.GetListCompany(UserId, authorization);
                if (result.StatusCode == HttpStatusCode.OK)
                    return result.Content;
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Response<List<GenericModel>>> GetState()
        {
            try
            {
                var result = await _connectionService.GetState();
                if (result.StatusCode == HttpStatusCode.OK)
                    return result.Content;
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Response<UserModel>> GetUser(int UserId, string authorization)
        {
            try
            {
                var result = await _connectionService.GetUser(UserId, authorization);
                if (result.StatusCode == HttpStatusCode.OK)
                    return result.Content;
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Response<List<PlanModel>>> ListPlan(AuthenticateModel query)
        {
            try
            {
                var result = await _connectionService.ListPlan(query);
                if (result.StatusCode == HttpStatusCode.OK)
                    return result.Content;
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Response<List<ServiceModel>>> ListServiceAllUser(int Page)
        {
            try
            {
                var result = await _connectionService.ListServiceAllUser(Page);
                if (result.StatusCode == HttpStatusCode.OK)
                    return result.Content;
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Response<List<ServiceModel>>> ListServiceByIdCompany(int UserId, int CompanyId, string authorization)
        {
            try
            {
                var result = await _connectionService.ListServiceByIdCompany(UserId, CompanyId, authorization);
                if (result.StatusCode == HttpStatusCode.OK)
                    return result.Content;
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Response<bool>> Register(AddUserModel query)
        {
            try
            {
                var result = await _connectionService.Register(query);
                if (result.StatusCode == HttpStatusCode.OK)
                    return result.Content;
                else
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<Response<bool>>(result.Error.Content);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Response<bool>> ResendCodeUser(int UserId, SendCodeUserModel query)
        {
            try
            {
                var result = await _connectionService.ResendCodeUser(UserId,query);
                if (result.StatusCode == HttpStatusCode.OK)
                    return result.Content;
                else
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<Response<bool>>(result.Error.Content);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Response<bool>> SendCodeForgotPassword(SendCodeForgotPasswordModel query)
        {
            try
            {
                var result = await _connectionService.SendCodeForgotPassword(query);
                if (result.StatusCode == HttpStatusCode.OK)
                    return result.Content;
                else
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<Response<bool>>(result.Error.Content);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Response<bool>> SendCodeUser(int UserId, SendCodeUserModel query)
        {
            try
            {
                var result = await _connectionService.SendCodeUser(UserId, query);
                if (result.StatusCode == HttpStatusCode.OK)
                    return result.Content;
                else
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<Response<bool>>(result.Error.Content);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Response<ServiceModel>> ServiceAllUser(int CompanyId, int ServiceId)
        {
            try
            {
                var result = await _connectionService.ServiceAllUser(CompanyId, ServiceId);
                if (result.StatusCode == HttpStatusCode.OK)
                    return result.Content;
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Response<ServiceModel>> ServiceByIdService(int UserId, int CompanyId, int ServiceId, string authorization)
        {
            try
            {
                var result = await _connectionService.ServiceByIdService(UserId, CompanyId, ServiceId, authorization);
                if (result.StatusCode == HttpStatusCode.OK)
                    return result.Content;
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Response<bool>> SuscribePlan(int UserId, int CompanyId, int PlanId, string authorization)
        {
            try
            {
                var result = await _connectionService.SuscribePlan(UserId, CompanyId, PlanId, authorization);
                if (result.StatusCode == HttpStatusCode.OK)
                    return result.Content;
                else
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<Response<bool>>(result.Error.Content);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Response<CodePostalInegeModel>> GetCodePostal(string CodePostal)
        {
            try
            {
                var result = await _connectionService.GetCodePostal(CodePostal);
                if (result.StatusCode == HttpStatusCode.OK)
                    return result.Content;
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Response<bool>> SuscripcionActiveCompany(int UserId, int CompanyId, string authorization)
        {
            try
            {
                var result = await _connectionService.SuscripcionActiveCompany(UserId, CompanyId, authorization);
                if (result.StatusCode == HttpStatusCode.OK)
                    return result.Content;
                else
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<Response<bool>>(result.Error.Content);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Response<bool>> UpdateCompany(int UserId, int CompanyId, UpdateCompanyModel query, string authorization)
        {
            try
            {
                var result = await _connectionService.UpdateCompany(UserId, CompanyId, query, authorization);
                if (result.StatusCode == HttpStatusCode.OK)
                    return result.Content;
                else
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<Response<bool>>(result.Error.Content);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Response<bool>> UpdateDefaultAddress(int UserId, int IdAddress, string authorization)
        {
            try
            {
                var result = await _connectionService.UpdateDefaultAddress(UserId, IdAddress, authorization);
                if (result.StatusCode == HttpStatusCode.OK)
                    return result.Content;
                else
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<Response<bool>>(result.Error.Content);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Response<bool>> UpdateService(int UserId, int CompanyId, int ServiceId, UpdateServiceModel query, string authorization)
        {
            try
            {
                var result = await _connectionService.UpdateService(UserId, CompanyId, ServiceId, query, authorization);
                if (result.StatusCode == HttpStatusCode.OK)
                    return result.Content;
                else
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<Response<bool>>(result.Error.Content);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Response<bool>> UpdateUser(int UserId, UpdateUserModel query, string authorization)
        {
            try
            {
                var result = await _connectionService.UpdateUser(UserId, query, authorization);
                if (result.StatusCode == HttpStatusCode.OK)
                    return result.Content;
                else
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<Response<bool>>(result.Error.Content);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Response<bool>> UpdateUserImage(int UserId, int CompanyId, StreamContent File, string authorization)
        {
            try
            {
                var result = await _connectionService.UpdateUserImage(UserId, CompanyId, File, authorization);
                if (result.StatusCode == HttpStatusCode.OK)
                    return result.Content;
                else
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<Response<bool>>(result.Error.Content);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Response<bool>> UpdateUserImage(int UserId, StreamContent File, string authorization)
        {
            try
            {
                var result = await _connectionService.UpdateUserImage(UserId,File,authorization);
                if (result.StatusCode == HttpStatusCode.OK)
                    return result.Content;
                else
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<Response<bool>>(result.Error.Content);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Response<bool>> UpdateAddress(int UserId, int IdAddress, AddAdressModel query, string authorization)
        {
            try
            {
                var result = await _connectionService.UpdateAddress(UserId, IdAddress, query, authorization);
                if (result.StatusCode == HttpStatusCode.OK)
                    return result.Content;
                else
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<Response<bool>>(result.Error.Content);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}
