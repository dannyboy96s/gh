using Microsoft.Extensions.Hosting;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.Extensions.DependencyInjection;
using gh.Proxies.Contracts;
using gh.Proxies.GithubProxy;

var GH_BASE_URL = Environment.GetEnvironmentVariable("GH_BASE_URL") ?? throw new ArgumentNullException("Missing / incorrect GH base url in settings");

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureOpenApi()
    .ConfigureServices(s => {

        s.AddTransient<IGithubProxy, GithubProxy>((s) 
            => new GithubProxy(GH_BASE_URL));

    })
    .Build();

host.Run();
