﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Net;

using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

using TeamDynamix.Api.Tickets;

namespace TeamDynamixLib {
    public class SourcesLib {

        /// <summary>
        /// Gets all active ticket sources.
        /// Invocations of this method are rate-limited, with a restriction of 60 calls per IP address every 60 seconds. 
        /// </summary>
        /// <param name="appID"></param>
        /// <param name="authHeader"></param>
        /// <param name="tDXEnvironment"></param>
        /// <returns>A list of all active ticket sources. (IEnumerable(Of TeamDynamix.Api.Tickets.TicketSource)) </returns>
        public async Task<List<TicketSource>> GetTicketSourcesAsync(int appID, string authHeader, TDXEnvironment tDXEnvironment) {
            var bearer = $"Bearer {authHeader}";

            RestClient restClient = new RestClient(tDXEnvironment.ClientUrl + (tDXEnvironment.IsSandboxEnvironment ? "SBTDWebApi" : "TDWebApi"));
            RestRequest restRequest = new RestRequest($"api/{appID}/tickets/sources", Method.GET);

            restRequest.AddHeader("accept", "application/json");
            restRequest.AddHeader("Authorization", bearer);

            if(tDXEnvironment.ProxyURL != null) {
                restClient.Proxy = new WebProxy(tDXEnvironment.ProxyURL, tDXEnvironment.ProxyPort);
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

            return JsonConvert.DeserializeObject<List<TicketSource>>(response.Content);
        }
    }
}
