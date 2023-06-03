using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gh.Domain.Repo
{
    public class Contributor
    {
        public string Login { get; set; }
        public int Contributions { get; set; }
    }
}