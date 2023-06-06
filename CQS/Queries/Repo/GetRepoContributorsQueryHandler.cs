using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gh.Domain.Response;
using gh.Proxies.Contracts;
using Mapster;
using MediatR;

namespace gh.CQS.Queries.Repo
{
    public class GetRepoContributorsQueryHandler : IRequestHandler<GetRepoContributorsQuery, List<ContributorResponse>>
    {
        private readonly IGithubProxy _proxy;

        public GetRepoContributorsQueryHandler(IGithubProxy proxy)
            => _proxy = proxy ?? throw new ArgumentNullException(nameof(proxy));

        public async Task<List<ContributorResponse>> Handle(GetRepoContributorsQuery request, CancellationToken cancellationToken)
        {
            try 
            {
                var contributors = await _proxy.GetRepoContributors(request.Org, request.Repo, cancellationToken);
                // map to ContributorResponse model
                var result = new List<ContributorResponse>();
                if (contributors is not null)
                    foreach (var contributor in contributors)
                        // map to ContributorResponse model and add to result list
                        result.Add(contributor.Adapt<ContributorResponse>());
                // 
                return result; 
            }
            catch (Exception e) 
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }
    }
}