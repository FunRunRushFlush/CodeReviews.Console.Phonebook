using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Phonebook.FunRunRushFlush.App;
using Phonebook.FunRunRushFlush.Data.DataAccess;
using Phonebook.FunRunRushFlush.Data.Database;
using Phonebook.FunRunRushFlush.Services;
using Phonebook.FunRunRushFlush.Services.Interface;

var host = Host.CreateDefaultBuilder()
    .ConfigureServices((context, service) =>
    {
        service.AddDbContext<AppDbContext>( optionsBuilder =>
        {
            optionsBuilder.UseSqlServer(context.Configuration.GetConnectionString("SQLServerConnection"));
        });



        service.AddScoped<PhonebookApp>();
        service.AddScoped<PhonebookDataAccess>();

        service.AddScoped<ICrudService, CrudService>();
        service.AddScoped<IUserInputValidationService, UserInputValidationService>();

    })
    .ConfigureLogging((context, logger) => {

        logger.ClearProviders();
        logger.AddDebug();
    }).Build();


var app = host.Services.GetRequiredService<PhonebookApp>();
await app.RunApp();
