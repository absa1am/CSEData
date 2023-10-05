using Autofac;
using Autofac.Extensions.DependencyInjection;
using CSEData.Infrastructure;
using CSEData.Persistence;
using CSEData.Worker;
using Microsoft.EntityFrameworkCore;
using Serilog;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", false)
    .AddEnvironmentVariables()
    .Build();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(config)
    .CreateLogger();

try
{
    var connectionString = config.GetConnectionString("DefaultConnection");
    var migrationAssembly = typeof(Worker).Assembly.FullName;

    IHost host = Host.CreateDefaultBuilder(args)
        .UseSerilog()
        .UseServiceProviderFactory(new AutofacServiceProviderFactory())
        .UseWindowsService()
        .ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterModule(new PersistenceModule(connectionString, migrationAssembly));
            containerBuilder.RegisterModule(new InfrastructureModule());
        })
        .ConfigureServices(services =>
        {
            services.AddHostedService<Worker>();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString, option => option.MigrationsAssembly(migrationAssembly)));
        })
        .Build();

    Log.Information("Application is starting...");

    host.Run();
}
catch (Exception exp)
{
    Log.Error(exp.Message);
}
finally
{
    Log.CloseAndFlush();
}