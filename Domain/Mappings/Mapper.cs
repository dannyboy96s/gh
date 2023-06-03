using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gh.Domain.Repo;
using gh.Domain.Response;
using Mapster;

namespace gh.Domain.Mappings
{
    internal static class Mapper
    {
         internal static void CreateMappings() {
            // map from ContributorResponse to Contributor 
            TypeAdapterConfig<ContributorResponse, Contributor>.NewConfig()
                .Map(dest => dest.Login, src => src.GithubUserName)
                .Map(dest => dest.Contributions, src => src.NumberOfContributions);    
            // map from Contributor to ContributorResponse 
            TypeAdapterConfig<Contributor, ContributorResponse>.NewConfig()
                .Map(dest => dest.GithubUserName, src => src.Login)
                .Map(dest => dest.NumberOfContributions, src => src.Contributions);     
        }
        
    }
}