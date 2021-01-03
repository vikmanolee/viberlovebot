using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using viberlovebot.Abstractions;
using viberlovebot.Services;
using ViberBotApi.Configuration;

namespace viberlovebot
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ViberBotApiConfiguration>(viberConfig =>
            {
                viberConfig.AuthToken = Configuration.GetValue<string>("ViberApi_AuthToken");
                viberConfig.BaseUrl = Configuration.GetValue<string>("ViberApi:BaseUrl");
            });
            services.Configure<BotConfiguration>(Configuration.GetSection("Bot"));
            services.AddScoped<IMessageResponseService, MessageResponseService>();
            services.AddScoped<ISendMessageService, SendMessageService>();
            services.AddScoped<IReceivedMessageService, ReceivedMessageService>();
            services.AddControllers(options =>
            {
                options.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "viberlovebot", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "viberlovebot v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
