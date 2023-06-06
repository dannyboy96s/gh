using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gh.Domain.Repo
{
    public class Pull
    {
        public string Url { get; set; }
        public int Number { get; set; }
        public string State { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
        public DateTime Closed_At { get; set; }
        public DateTime Merged_At { get; set; }
        public List<RequestedReviewer> Requested_Reviewers { get; set; }

    }
}