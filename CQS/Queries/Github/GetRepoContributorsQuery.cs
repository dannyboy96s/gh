using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace gh.CQS.Queries.Github
{
    public class GetRepoContributorsQuery : IRequest<string>
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