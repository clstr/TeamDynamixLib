using System.Net;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;

using TeamDynamix.Api.Tickets;

namespace TeamDynamixLib {
    public class TicketLib {
        public async Task<Ticket> GetTicketAsync( int ticketID, int appID, string authHeader, TDXEnvironment tDXEnvironment ) {
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var bearer = $"Bearer {authHeader}";

            RestClient restClient = new RestClient(tDXEnvironment.ClientUrl + (tDXEnvironment.IsSandboxEnvironment ? "SBTDWebApi" : "TDWebApi"));
            RestRequest restRequest = new RestRequest($"api/{appID}/tickets/{ticketID}", Method.GET);

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

            return JsonConvert.DeserializeObject<Ticket>(response.Content);
        }

        // POST https://yourSchool.School.edu/TDWebApi/api/{appId}/tickets?EnableNotifyReviewer={EnableNotifyReviewer}&NotifyRequestor={NotifyRequestor}&NotifyResponsible={NotifyResponsible}&AllowRequestorCreation={AllowRequestorCreation}
        // Description: Creates a ticket.
        // Invocations of this method are rate-limited, with a restriction of 120 calls per IP address every 60 seconds.
        public async Task<Ticket> CreateTicketAsync( Ticket ticket, int appID, TicketCreateOptions ticketCreateOptions, string authHeader, TDXEnvironment tDXEnvironment ) {
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var bearer = $"Bearer {authHeader}";

            RestClient restClient = new RestClient(tDXEnvironment.ClientUrl + (tDXEnvironment.IsSandboxEnvironment ? "SBTDWebApi" : "TDWebApi"));
            RestRequest restRequest = new RestRequest($"api/{appID}/tickets?EnableNotifyReviewer={ticketCreateOptions.EnableNotifyReviewer}&NotifyRequestor={ticketCreateOptions.NotifyRequestor}&NotifyResponsible={ticketCreateOptions.NotifyResponsible}&AllowRequestorCreation={ticketCreateOptions.AllowRequestorCreation}", 
                Method.POST);

            restRequest.AddHeader("accept", "application/json");
            restRequest.AddHeader("Authorization", bearer);
            string jsonObject = JsonConvert.SerializeObject(ticket, Formatting.Indented, jsonSerializerSettings);
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

            return JsonConvert.DeserializeObject<Ticket>(response.Content);
        }
    }
}
