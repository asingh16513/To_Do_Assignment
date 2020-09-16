using Application.Helper;
using Application.Interface;
using Application.Label.Command.AddLabel;
using Application.Label.Queries.GetLabels;
using Application.Mutation;
using Application.QueryTypes;
using Database;
using Domain.GraphQlModels;
using FluentValidation;
using FluentValidation.AspNetCore;
using HotChocolate;
using HotChocolate.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Persistence;
using System.Collections.Generic;
using System.Reflection;
using ToDoService.API.Middleware;
using ToDoService.Helpers;
using ToDoService.Middleware;

namespace ToDoService
{
    /// <summary>
    /// startup class 
    /// </summary>
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services
                .AddDataLoaderRegistry()
                .AddGraphQL(s => SchemaBuilder.New()
                .AddServices(s)
                .AddType<LabelType>()
                .AddType<ToDoListType>()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddAuthorizeDirectiveType()
                .Create());
            services.AddApiVersioning(x =>
            {
                x.DefaultApiVersion = new ApiVersion(1, 0);
                x.AssumeDefaultVersionWhenUnspecified = true;
                x.ReportApiVersions = true;
            });


            ConfigurationManager.LoadConfigurationSettings(services, Configuration);
            services.AddSingleton<IPostConfigureOptions<JwtBearerOptions>, ConfigureJwtBearerOptions>();
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer();

            services.AddMvc(p => p.RespectBrowserAcceptHeader = true).AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.PropertyNamingPolicy = null;
                o.JsonSerializerOptions.DictionaryKeyPolicy = null;
            })
           .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LabelValidator>())
           .AddXmlSerializerFormatters();

            RegisterServices(services);
            //s//ervices.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            //RegisterValidatorService(services);

            services.AddSwaggerGen(s =>
            {

                s.SwaggerDoc("v2", new OpenApiInfo { Title = "To Do Service API", Version = "v2" });
                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                s.AddSecurityRequirement(new OpenApiSecurityRequirement {
                   {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                       }
                      },
                      new string[] { }
                    }
                  });
            });
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(GetLabelListHandler).Assembly);
            services.AddSingleton<ILabelDBManager, LabelDbManager>();
            services.AddSingleton<IToDoItemDbManager, ToDoItemDbManager>();
            services.AddSingleton<IToDoListDbManager, ToDoListDbManager>();
            services.AddSingleton<IUserDbManager, UserDbManager>();
            services.AddSingleton<IInstanceDB, GetInstance>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IPatchToDo, PatchHelper>();
            services.AddScoped<IDTO, DTOHelper>();
            services.AddTransient<IUserManager, UserManager>();
            services.AddSingleton<IMD5Hash, MD5HashHelper>();
        }

        private static void RegisterValidatorService(IServiceCollection services)
        {
            var assembliesToRegister = new List<Assembly>() { typeof(LabelValidator).Assembly };
            AssemblyScanner.FindValidatorsInAssemblies(assembliesToRegister).ForEach(pair =>
            {
                services.Add(ServiceDescriptor.Transient(pair.InterfaceType, pair.ValidatorType));
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseMiddleware(typeof(ErrorHandling));
            app.UseMiddleware(typeof(RequestLogging));
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseGraphQL();
            app.UsePlayground();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v2/swagger.json", "ToDoService API V2");
            });

        }
    }
}
