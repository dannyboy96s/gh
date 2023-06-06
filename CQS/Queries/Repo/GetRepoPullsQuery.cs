using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gh.Domain.Response;
using MediatR;

namespace gh.CQS.Queries.Repo
{
    public class GetRepoPullsQuery : IRequest<List<PullsResponse>>
    {
        // can add a base query class to add things common for all queries so we dont have code duplication
        public string Org { get; set; }
        public string Repo { get; set; }
        public GetRepoPullsQuery(string org, string repo)
        {
            Org = org;
            Repo = repo;
        }
    }
}