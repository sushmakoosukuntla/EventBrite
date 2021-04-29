using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebMvc.Infrastructure
{
    public interface IHttpClient
    {
        Task<string> GetStringAsync(string uri,
            string authorizationToken = null,
            string authorizationMethod = "Bearer");

        //T mean, its a generic method
        //PostAsync<T>, We made as generic because we might send the product info to the cart or the cart info to the order
        //string uri,  should tell me where the user is posting the data
        Task<HttpResponseMessage> PostAsync<T>(string uri, T item, 
            string authorizationToken = null,
            string authorizationMethod = "Bearer");

        Task<HttpResponseMessage> DeleteAsync(string uri,
            string authorizationToken = null,
            string authorizationMethod = "Bearer");

        //PutAsync is an another version of post
        //Generally people use post to add something and put to update something
        Task<HttpResponseMessage> PutAsync<T>(string uri, T item,
            string authorizationToken = null,
            string authorizationMethod = "Bearer");

    }
}
