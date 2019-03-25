using System;
using BitCoind.Api.Infrastructure.Extensions;
using BitCoind.Core.Logic;
using BitCoind.Database;
using BitCoind.Logic.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace BitCoind.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly string _versionString;
        private readonly string _contentRootPath;

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _versionString = GetType().Assembly.GetName().Version.ToString(4);
            _contentRootPath = env.ContentRootPath;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var connection = _configuration["DbConnectionString"];
            if (connection.Contains("%AttachDbFilename%"))
            {
                var attachDbFilename = $"{_contentRootPath.Replace("BitCoind.Api", "BitCoind.Database")}\\BitCoindDatabase.mdf";
                connection = connection.Replace("%AttachDbFilename%", attachDbFilename);
            }

            services.AddDbContextPool<BitcoindDbContext>(options => options.UseSqlServer(connection));

            services.AddScoped<ITokenHelper, TokenHelper>();
            services.AddScoped<ITransferHelper, TransferHelper>();
            services.AddScoped<IHistoryHelper, HistoryHelper>();
            services.AddScoped<IWalletHelper, WalletHelper>();

            services.AddSwaggerDocumentation(_versionString, _configuration);

            services
                .AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(opts =>
                    opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwaggerDocumentation(env, _versionString);

            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");

            app.UseRewriter(option);
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
