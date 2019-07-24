using System;
using System.Net;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;

using TeamDynamix.Api.Auth;
using TeamDynamix.Api.Tickets;
using TeamDynamix.Api.Users;


namespace TeamDynamixLib {
    public class TeamDynamixLib {
        public async Task<string> GetAuthHeaderAsync(Guid BEID, Guid WebServicesKey, TDXEnvironment tDXEnvironment) {
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            AdminTokenParameters parameters = new AdminTokenParameters() {
                BEID = BEID,
                WebServicesKey = WebServicesKey
            };

            RestClient restClient = new RestClient(tDXEnvironment.ClientUrl + (tDXEnvironment.IsSandboxEnvironment ? "SBTDWebApi" : "TDWebApi"));
            RestRequest restRequest = new RestRequest($"api/auth/loginadmin", Method.POST);

            restRequest.AddHeader("accept", "application/json");
            string jsonObject = JsonConvert.SerializeObject(parameters, Formatting.Indented, jsonSerializerSettings);
            restRequest.AddParameter("application/json", jsonObject, ParameterType.RequestBody);

            if(tDXEnvironment.ProxyURL != null) {
                restClient.Proxy = new WebProxy(tDXEnvironment.ProxyURL, tDXEnvironment.ProxPort);
            }
            

            TaskCompletionSource<IRestResponse> taskCompletion = new TaskCompletionSource<IRestResponse>();

            RestRequestAsyncHandle handle = restClient.ExecuteAsync(
                restRequest,
                r => taskCompletion.SetResult(r)
            );

            RestResponse response = (RestResponse)(await taskCompletion.Task);

            if(response.StatusCode == HttpStatusCode.NotFound) {
                return null;
            }

            return response.Content;
        }

        public async Task<Ticket> GetTicketAsync(int ticketID, int appID, string auth, TDXEnvironment tDXEnvironment) {
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var bearer = $"Bearer {auth}";

            RestClient restClient = new RestClient(tDXEnvironment.ClientUrl + (tDXEnvironment.IsSandboxEnvironment ? "SBTDWebApi" : "TDWebApi"));

            RestRequest restRequest = new RestRequest($"api/{appID}/tickets/{ticketID}", Method.GET);
            restRequest.AddHeader("accept", "application/json");
            restRequest.AddHeader("Authorization", bearer);

            if(tDXEnvironment.ProxyURL != null) {
                restClient.Proxy = new WebProxy(tDXEnvironment.ProxyURL, tDXEnvironment.ProxPort);
            }

            TaskCompletionSource<IRestResponse> taskCompletion = new TaskCompletionSource<IRestResponse>();

            RestRequestAsyncHandle handle = restClient.ExecuteAsync(restRequest,
                r => taskCompletion.SetResult(r)
            );
            RestResponse response = (RestResponse)(await taskCompletion.Task);

            if(response.StatusCode == HttpStatusCode.NotFound) {
                return null;
            }

            return JsonConvert.DeserializeObject<Ticket>(response.Content);
        }

        public async Task<User> GetPersonAsync(Guid? uid, string auth, TDXEnvironment tDXEnvironment) {
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var bearer = $"Bearer {auth}";

            RestClient restClient = new RestClient(tDXEnvironment.ClientUrl + (tDXEnvironment.IsSandboxEnvironment ? "SBTDWebApi" : "TDWebApi"));

            RestRequest restRequest = new RestRequest($"api/people/{uid}", Method.GET);
            restRequest.AddHeader("accept", "application/json");
            restRequest.AddHeader("Authorization", bearer);

            if(tDXEnvironment.ProxyURL != null) {
                restClient.Proxy = new WebProxy(tDXEnvironment.ProxyURL, tDXEnvironment.ProxPort);
            }

            TaskCompletionSource<IRestResponse> taskCompletion = new TaskCompletionSource<IRestResponse>();

            RestRequestAsyncHandle handle = restClient.ExecuteAsync(restRequest,
                r => taskCompletion.SetResult(r)
            );
            RestResponse response = (RestResponse)(await taskCompletion.Task);

            if(response.StatusCode == HttpStatusCode.NotFound) {
                return null;
            }

            return JsonConvert.DeserializeObject<User>(response.Content);
        }
    }
}
