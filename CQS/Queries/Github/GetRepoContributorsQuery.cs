using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using gh.Domain.Response;

namespace gh.CQS.Queries.Github
{
    public class GetRepoContributorsQuery : IRequest<List<ContributorResponse>>
    {
        public string Org { get; set; }
        public string Repo { get; set; }
        public GetRepoContributorsQuery(string org, string repo)
        {
            Org = org;
            Repo = repo;
        }
    }
}