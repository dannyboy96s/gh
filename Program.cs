using Microsoft.Extensions.Hosting;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.Extensions.DependencyInjection;
using gh.Proxies.Contracts;
using gh.Proxies.GithubProxy;
using MediatR;
using gh.Domain.Mappings;

var GH_BASE_URL = Environment.GetEnvironmentVariable("GH_BASE_URL") ?? throw new ArgumentNullException("Missing / incorrect GH base url in settings");

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureOpenApi()
    .ConfigureServices(s => {

        s.AddTransient<IGithubProxy, GithubProxy>((s) 
            => new GithubProxy(GH_BASE_URL));

        // MediatR
        s.AddMediatR(typeof(Program));

    })
    .Build();

// Init mappings
Mapper.CreateMappings();    

host.Run();
