using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Contracts;
using Database.Implementation;
using Database.Models;
using Repository;
using Repository.Contracts;
using Repository.Implimentation;


using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Logging.EventLog;
using MyTest.Automation;

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options =>
    {
        options.ServiceName = "MyTest.Automation Service";
    })
    .ConfigureServices((hostContext, services) =>
    {
        LoggerProviderOptions.RegisterProviderOptions<
            EventLogSettings, EventLogLoggerProvider>(services);

        // Add services to container here...//Adding Workers

        services.AddHostedService<ParkingClearenceWorker>();
        services.AddHostedService<InterviewReminderMailWorker>();
        services.AddHostedService<DailyScheduleStatusMailWorker>();

        //Setting DB Connection String & appSettings
        services.Configure<WorkerSettings>(hostContext.Configuration.GetSection("appSettings"));
        string connectionString = hostContext.Configuration.GetConnectionString("VProPleDBConnection").ToString();
        services.AddDbContext<VProPleContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        //Scopring All Required Service
        services.AddScoped<ParkingClearanceRepository>();
        services.AddScoped<ICommunicationRepository, CommunicationRepository>();
        services.AddScoped<InterviewReminderMailRepository>();
        services.AddScoped<ITechnicalDAL, TechnicalDAL>();
        services.AddScoped<DailyScheduleStatusMailRepository>();
    })
    .UseSerilog((ctx, config) => { config.ReadFrom.Configuration(ctx.Configuration); })

    .Build();

await host.RunAsync();
