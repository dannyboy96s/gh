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
            // map from Contributor to ContributorResponse and vice versa, two ways
            TypeAdapterConfig<Contributor, ContributorResponse>.NewConfig()
                .Map(dest => dest.GithubUserName, src => src.Login)
                .Map(dest => dest.NumberOfContributions, src => src.Contributions)
                .TwoWays();   
            // map from Pull to PullsResponse 
            TypeAdapterConfig<Pull2, PullsResponse>.NewConfig()
                .Map(dest => dest.PrUrl, src => src.url)
                .Map(dest => dest.PrNumber, src => src.number)
                .Map(dest => dest.Title, src => src.title)
                .Map(dest => dest.StateOfPr, src => src.state)
                .Map(dest => dest.CreatedAt, src => src.created_at)
                .Map(dest => dest.UpdatedAt, src => src.updated_at)
                .Map(dest => dest.MergedAt, src => src.merged_at)
                .Map(dest => dest.ClosedAt, src => src.closed_at);
                //.Map(dest => dest.Reviewers, src => src.requested_reviewers.AsQueryable().ProjectToType<Requested_Reviewer>().ToList()); 
            // Do the Mapping for RequestedReviewer to Requested_Reviewer
            TypeAdapterConfig<RequestedReviewer, Requested_Reviewer>.NewConfig()
                .Map(dest => dest.GithubUserName, src => src.login)
                .TwoWays();        
        }
        
    }
}