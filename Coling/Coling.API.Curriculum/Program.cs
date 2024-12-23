using Coling.API.Curriculum.Contratos.Repositorios;
using Coling.API.Curriculum.Implementacion.Repositorios;
using Coling.Utilitarios.Attributes;
using Coling.Utilitarios.Middlewares;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

var ensamblado = Assembly.GetExecutingAssembly();
var tipoAtributo = typeof(ColingAuthorizeAttribute);
IList<string> DefaultFunctions = new List<string>();
var host = new HostBuilder()
    //.ConfigureFunctionsWebApplication(/*worker => worker.UseNewtonsoftJson()*/)
    .ConfigureServices(services =>
    {
        //services.AddAutoMapper(ensamblado);
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddScoped<IInstitucionRepositorio, InstitucionRepositorio>();
        services.AddScoped<IEstudiosRepositorio, EstudiosRepositorio>();
        services.AddScoped<IExperienciaLaboralRepositorio, ExperienciaLaboralRepositorio>();
        services.AddScoped<IProfesionRepositorio, ProfesionRepositorio>();
        services.AddSingleton<JwtMiddleware> ();
        services.AddSingleton<AutorizacionMiddleware> ();
        services.Configure<KestrelServerOptions>(options =>
        {
            options.AllowSynchronousIO = true;
        });
    }).ConfigureFunctionsWebApplication(x=>
    {
        x.UseMiddleware<JwtMiddleware>();
        x.UseMiddleware<AutorizacionMiddleware>();
    })
    .Build();
var lista = GetAuthorizedFunctions(ensamblado, tipoAtributo);
host.Services.GetService<AutorizacionMiddleware>()?.SetFunctionAutorizadas(lista);
host.Run();

Dictionary<string, string> GetAuthorizedFunctions2(Type containerType, Type? attributeType)
{
    var authorizedFunctions = new Dictionary<string, string>();

    if (attributeType == null) { return authorizedFunctions; }

    Type functionType = typeof(FunctionAttribute);
    var methods = ObtenerMetdosConAtributoPersonalizado(containerType, functionType);
    foreach (var method in methods)
    {
        var function = method.GetCustomAttribute(attributeType, true) as ColingAuthorizeAttribute;
        if (function is null)
        {
            continue;
        }

        var functionA = (FunctionAttribute?)method.GetCustomAttribute(functionType, true);
        if (functionA is null)
        {
            continue;
        }
        authorizedFunctions.Add(functionA.Name, function.Rols);
    }

    return authorizedFunctions;
}

MethodInfo[] ObtenerMetdosConAtributoPersonalizado(Type type, Type attributeType)
{
    return type.GetMethods()
        .Where(m => m.GetCustomAttributes(attributeType, inherit: true).Any())
        .ToArray();
}

Dictionary<string, string> GetAuthorizedFunctions(Assembly containerAssembly, Type? attributeType)
{
    if (containerAssembly == null) throw new ArgumentNullException(nameof(containerAssembly));
    if (attributeType == null) throw new ArgumentNullException(nameof(attributeType));

    var result = new Dictionary<string, string>();
    foreach (var item in containerAssembly.ExportedTypes)
    {
        var resulItem = GetAuthorizedFunctions2(item, attributeType);
        result.AddRange(resulItem);
    }

    return result;
}
