using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gh.Domain.Response
{
    public class ContributorResponse
    {
        public string GithubUserName { get; set; }
        public int NumberOfContributions { get; set; }
    }
}