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
    public class GetRepoPullsQueryHandler : IRequestHandler<GetRepoPullsQuery, List<PullsResponse>>
    {
        private readonly IGithubProxy _proxy;

        public GetRepoPullsQueryHandler(IGithubProxy proxy)
            => _proxy = proxy ?? throw new ArgumentNullException(nameof(proxy));
            
        public async Task<List<PullsResponse>> Handle(GetRepoPullsQuery request, CancellationToken cancellationToken)
        {
            try 
            {
                var pulls = await _proxy.GetRepoPulls(request.Org, request.Repo, cancellationToken);
                // map to PullsResponse model
                var result = new List<PullsResponse>();
                if (pulls is not null)
                    foreach (var pull in pulls) {
                        // map to PullsResponse model and add to result list
                        var p = pull.Adapt<PullsResponse>();
                        p.Reviewers = pull.requested_reviewers.AsQueryable().ProjectToType<Requested_Reviewer>().ToList();
                        result.Add(p);
                    }
                // order by created date in desc order, like how you see it in gh
                result.OrderByDescending(x => x.CreatedAt);
                return result; 
            }
            catch (Exception e) 
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }
    }
}