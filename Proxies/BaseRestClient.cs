using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flurl.Http;

namespace gh.Proxies
{
    public abstract class BaseRestClient
    {
        protected string Url;
        protected HttpClient HttpClient;
        protected string Token;
        protected Dictionary<string, string> Headers;

        protected BaseRestClient(string url)
            => Url = url ?? throw new ArgumentNullException(nameof(url));

        protected async Task<T> GetAsync<T>(string segment, 
                                            Dictionary<string, string> query = null, 
                                            int timeout = 30, 
                                            CancellationToken cancellationToken = default) 
        {
            async Task<T> Func() 
                => await Request($"{Url}{segment}", timeout)
                .GetJsonAsync<T>(cancellationToken);
            //
            return await RunAsync(Func);
        }

        protected static async Task<T> RunAsync<T>(Func<Task<T>> run) 
        {
            string errorMsg;
            try
            {
                return await run.Invoke();
            } 
            catch(FlurlHttpException fex) 
            {
                // can do some custom error handling here
                errorMsg = fex.Message;
            }
            throw new Exception(errorMsg);
        }

        protected IFlurlRequest Request(string url, int timeout) 
        {
            var request = url.WithTimeout(timeout);
            if(HttpClient is not null)
                request = request.WithClient(new FlurlClient(HttpClient));
            if(Token is not null)
                request = request.WithOAuthBearerToken(Token);
            if(Headers is not null)
                foreach (var h in Headers)
                    request = request.WithHeader(h.Key, h.Value);
            //
            return request;
        }

        
    }
}