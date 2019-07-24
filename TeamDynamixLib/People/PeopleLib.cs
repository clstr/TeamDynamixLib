using System;
using System.Net;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;

using TeamDynamix.Api.Users;

namespace TeamDynamixLib.PeopleLib {
    public class PeopleLib {
        public async Task<User> GetPersonByUIDAsync( Guid? uid, string authHeader, TDXEnvironment tDXEnvironment ) {
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var bearer = $"Bearer {authHeader}";

            RestClient restClient = new RestClient(tDXEnvironment.ClientUrl + (tDXEnvironment.IsSandboxEnvironment ? "SBTDWebApi" : "TDWebApi"));
            RestRequest restRequest = new RestRequest($"api/people/{uid}", Method.GET);

            restRequest.AddHeader("accept", "application/json");
            restRequest.AddHeader("Authorization", bearer);

            if ( tDXEnvironment.ProxyURL != null ) {
                restClient.Proxy = new WebProxy(tDXEnvironment.ProxyURL, tDXEnvironment.ProxyPort);
            }

            TaskCompletionSource<IRestResponse> taskCompletion = new TaskCompletionSource<IRestResponse>();

            RestRequestAsyncHandle handle = restClient.ExecuteAsync(
                restRequest,
                r => taskCompletion.SetResult(r)
            );

            RestResponse response = (RestResponse) (await taskCompletion.Task);

            if ( response.StatusCode == HttpStatusCode.NotFound ) {
                return null;
            }

            return JsonConvert.DeserializeObject<User>(response.Content);
        }
    }
}
