﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;

using TeamDynamix.Api.Users;

namespace TeamDynamixLib {
    public class PeopleLib {

        /// <summary>
        /// Gets a person from the system.
        /// Invocations of this method are rate-limited, with a restriction of 60 calls per IP address every 60 seconds.
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="authHeader"></param>
        /// <param name="tDXEnvironment"></param>
        /// <returns></returns>
        public async Task<User> GetPersonByUIDAsync( Guid uid, string authHeader, TDXEnvironment tDXEnvironment ) {
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

        /// <summary>
        /// Creates a user in the system and returns an object representing that person.
        /// Invocations of this method are rate-limited, with a restriction of 45 calls per IP address every 60 seconds.
        /// </summary>
        /// <param name="appID"></param>
        /// <param name="newUser"></param>
        /// <param name="authHeader"></param>
        /// <param name="tDXEnvironment"></param>
        /// <returns></returns>
        public async Task<User> CreatePersonAsync( int appID, NewUser newUser, string authHeader, TDXEnvironment tDXEnvironment ) {
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var bearer = $"Bearer {authHeader}";

            RestClient restClient = new RestClient(tDXEnvironment.ClientUrl + (tDXEnvironment.IsSandboxEnvironment ? "SBTDWebApi" : "TDWebApi"));
            RestRequest restRequest = new RestRequest($"api/people", Method.POST);

            restRequest.AddHeader("accept", "application/json");
            restRequest.AddHeader("Authorization", bearer);
            string jsonObject = JsonConvert.SerializeObject(newUser, Formatting.Indented, jsonSerializerSettings);
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

            return JsonConvert.DeserializeObject<User>(response.Content);
        }

        /// <summary>
        /// Updates a person entry for the user with the specified identifier with a set of new values.
        /// Invocations of this method are rate-limited, with a restriction of 45 calls per IP address every 60 seconds.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="uid"></param>
        /// <param name="appID"></param>
        /// <param name="authHeader"></param>
        /// <param name="tDXEnvironment"></param>
        /// <returns></returns>
        public async Task<User> UpdatePersonAsync( User user, Guid uid, int appID, string authHeader, TDXEnvironment tDXEnvironment ) {
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var bearer = $"Bearer {authHeader}";

            RestClient restClient = new RestClient(tDXEnvironment.ClientUrl + (tDXEnvironment.IsSandboxEnvironment ? "SBTDWebApi" : "TDWebApi"));
            RestRequest restRequest = new RestRequest($"api/people/{uid}", Method.POST);

            restRequest.AddHeader("accept", "application/json");
            restRequest.AddHeader("Authorization", bearer);
            string jsonObject = JsonConvert.SerializeObject(user, Formatting.Indented, jsonSerializerSettings);
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

            return JsonConvert.DeserializeObject<User>(response.Content);
        }

        /// <summary>
        /// Gets all functional roles for a particular user.
        /// Invocations of this method are rate-limited, with a restriction of 180 calls per IP address every 60 seconds.
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="authHeader"></param>
        /// <param name="tDXEnvironment"></param>
        /// <returns></returns>
        public async Task<User> GetPersonFunctionalRolesAsync( Guid? uid, string authHeader, TDXEnvironment tDXEnvironment ) {
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var bearer = $"Bearer {authHeader}";

            RestClient restClient = new RestClient(tDXEnvironment.ClientUrl + (tDXEnvironment.IsSandboxEnvironment ? "SBTDWebApi" : "TDWebApi"));
            RestRequest restRequest = new RestRequest($"api/people/{uid}/functionalroles", Method.GET);

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

        /// <summary>
        /// Performs a restricted lookup of TeamDynamix people. Will not return full user information for each matching user. 
        /// Invocations of this method are rate-limited, with a restriction of 75 calls per IP address every 10 seconds.
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="authHeader"></param>
        /// <param name="tDXEnvironment"></param>
        /// <param name="maxResults"></param>
        /// <returns></returns>
        public async Task<List<User>> GetPersonLookupAsync(string searchText, string authHeader, TDXEnvironment tDXEnvironment, int maxResults = 50) {
            var bearer = $"Bearer {authHeader}";

            RestClient restClient = new RestClient(tDXEnvironment.ClientUrl + (tDXEnvironment.IsSandboxEnvironment ? "SBTDWebApi" : "TDWebApi"));
            RestRequest restRequest = new RestRequest($"api/people/lookup", Method.GET);
            restRequest.AddParameter("searchText", searchText);
            restRequest.AddParameter("maxResults", maxResults);

            restRequest.AddHeader("accept", "application/json");
            restRequest.AddHeader("Authorization", bearer);

            if (tDXEnvironment.ProxyURL != null) {
                restClient.Proxy = new WebProxy(tDXEnvironment.ProxyURL, tDXEnvironment.ProxyPort);
            }

            TaskCompletionSource<IRestResponse> taskCompletion = new TaskCompletionSource<IRestResponse>();

            RestRequestAsyncHandle handle = restClient.ExecuteAsync(
                restRequest,
                r => taskCompletion.SetResult(r)
            );

            RestResponse response = (RestResponse)(await taskCompletion.Task);

            if (response.StatusCode == HttpStatusCode.NotFound) {
                return null;
            }

            return JsonConvert.DeserializeObject<List<User>>(response.Content);
        }
    }
}
