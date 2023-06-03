using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gh.Proxies.Contracts;
using MediatR;

namespace gh.CQS.Queries.Github
{
    public class GetRepoContributorsQueryHandler : IRequestHandler<GetRepoContributorsQuery, string>
    {
        private readonly IGithubProxy _proxy;

        public GetRepoContributorsQueryHandler(IGithubProxy proxy)
            => _proxy = proxy ?? throw new ArgumentNullException(nameof(proxy));

        public async Task<string> Handle(GetRepoContributorsQuery request, CancellationToken cancellationToken)
        {
            try 
            {
                var result = await _proxy.GetRepoContributors(request.Org, request.Repo, cancellationToken);
                // map to repsonse model
                return result; 
            }
            catch (Exception e) 
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }
    }
}