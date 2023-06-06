using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gh.Domain.Repo;

namespace gh.Domain.Response
{
    public class PullsResponse
    {
        public string PrUrl { get; set; }
        public int PrNumber { get; set; }
        public string StateOfPr { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime ClosedAt { get; set; }
        public DateTime MergedAt { get; set; }
        public List<Requested_Reviewer> Reviewers { get; set; }
    }

    public class Requested_Reviewer {
        public string GithubUserName { get; set; }
    }
}