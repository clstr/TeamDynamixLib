using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;

using TeamDynamix.Api.Auth;

namespace TeamDynamixLib.AuthenticationLib {
    public class AuthenticationLib {
        public async Task<string> GetAuthHeaderAsync( AdminTokenParameters adminTokenParameters, TDXEnvironment tDXEnvironment ) {
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            RestClient restClient = new RestClient(tDXEnvironment.ClientUrl + (tDXEnvironment.IsSandboxEnvironment ? "SBTDWebApi" : "TDWebApi"));
            RestRequest restRequest = new RestRequest($"api/auth/loginadmin", Method.POST);

            restRequest.AddHeader("accept", "application/json");
            string jsonObject = JsonConvert.SerializeObject(adminTokenParameters, Formatting.Indented, jsonSerializerSettings);
            restRequest.AddParameter("application/json", jsonObject, ParameterType.RequestBody);

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

            return response.Content;
        }
    }
}
