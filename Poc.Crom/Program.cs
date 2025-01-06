using BM.MissionProcessor.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.Identity.Abstractions;
using Microsoft.Identity.Web.Resource;
using Quartz;
using Quartz.AspNetCore;

namespace Poc.Crom;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddScoped<ISecurityService, SecurityService>();
        
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Host.UseOrleans(static siloBuilder =>
        {
            siloBuilder.UseLocalhostClustering();
            siloBuilder.AddMemoryGrainStorage("MemoryStore");
        });


        builder.Services.AddQuartz(q =>
        {
            // base Quartz scheduler, job and trigger configuration

            // Just use the name of your job that you created in the Jobs folder.
            var jobKey = new JobKey("RunningJob");
            q.AddJob<RunningJob>(opts => opts.WithIdentity(jobKey));

            // Para validar possiveis Agendamentos utilizar o site -- https://crontab.cronhub.io/
            q.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity("RunningJob-trigger")
                //This Cron interval can be described as "run every minute" (when second is zero)
                .WithCronSchedule("0 * * ? * *")
            );
        });

        // ASP.NET Core hosting
        builder.Services.AddQuartzServer(options =>
        {
            // when shutting down we want jobs to complete gracefully
            options.WaitForJobsToComplete = true;
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.MapControllers();

        app.Run();
    }
}