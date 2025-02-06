using Barber.Colocho.App.Db;
using Barber.Colocho.App.Models.Authenticate;
using Barber.Colocho.App.Models.Base;
using Barber.Colocho.App.Settings;
using Barber.Colocho.App.Views.Session;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Barber.Colocho.App.Services.Client
{
    public class HttpClientMessageHandler : DelegatingHandler
    {
        public HttpClientMessageHandler()
        {
            InnerHandler = new HttpClientHandler();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await base.SendAsync(request, cancellationToken);
                if (response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.Forbidden)
                {
                    var token = DbContext.Instance.GetToken();
                    if (token != null && !string.IsNullOrWhiteSpace(token.RefreshToken))
                    {
                        var data = new
                        {
                            AccessToken = token.AccessToken,
                            RefreshToken = token.RefreshToken,
                        };
                        var serializer = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                        var streamContent = new HttpRequestMessage(HttpMethod.Post, KeyDictionary.URL_BASE + "/api/v1/OAuth/RefreshToken");
                        streamContent.Content = new StringContent(serializer, Encoding.UTF8, "application/json");
                        streamContent.Headers.Add("Api-Version", "1.0");

                        using (var refreshResponse = await SendAsync(streamContent, cancellationToken))
                        {
                            var rawResponse = await refreshResponse.Content.ReadAsStringAsync();
                            var x = JsonConvert.DeserializeObject<Response<RefreshTokenResponseModel>>(rawResponse);
                            if (x != null && x.Result != null)
                            {
                                DbContext.Instance.DeleteToken();
                                DbContext.Instance.InsertToken(new TokenResponseModel
                                {
                                    AccessToken = x.Result.AccessToken,
                                    RefreshToken = x.Result.RefreshToken,
                                    UserId = token.UserId,
                                });
                                request.Headers.Remove("Authorization");
                                request.Headers.Add("Authorization", $"Bearer {x.Result.AccessToken}");
                                response = await SendAsync(request, cancellationToken);
                            }
                            else
                            {
                                DbContext.Instance.DeleteToken();
                                DbContext.Instance.DeleteToken();
                                App.Current.MainPage = new NavigationPage(new OnBoardingPage());
                            }
                        }
                    }
                    else
                    {
                        DbContext.Instance.DeleteToken();
                        DbContext.Instance.DeleteToken();
                        App.Current.MainPage = new NavigationPage(new OnBoardingPage());
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
