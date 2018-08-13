namespace SpanHead.VA.Web
{
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Authorization;
    using Microsoft.AspNetCore.SpaServices.Webpack;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using SpanHead.VA.Web.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Startup
    {
        /*
           It is very, very important that you retrieve the SecretKey from some secure location such as Environment Settings 
         */
        private const string SecretKey = "$p@nHe@dVot!ngApp";
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region DI
            services.RegisterServices();
            #endregion

            #region JWT Auth
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));


            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(cfg =>
                    {
                        cfg.RequireHttpsMetadata = false;
                        cfg.SaveToken = true;

                        cfg.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],
                            ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],
                            IssuerSigningKey = _signingKey
                        };

                    });


            #endregion

            #region Auth old
            //// Add framework services.
            //services.AddOptions();

            //// Get options from app settings
            //var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            //services.AddAuthentication()
            //        .AddJwtBearer(cfg =>
            //        {
            //            cfg.RequireHttpsMetadata = false;
            //            cfg.SaveToken = true;

            //            cfg.TokenValidationParameters = new TokenValidationParameters()
            //            {
            //                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],
            //                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],
            //                IssuerSigningKey = _signingKey
            //            };

            //        });

            //// // Configure JwtIssuerOptions
            //// services.Configure<JwtIssuerOptions>(options =>
            //// {
            ////     options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
            ////     options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
            ////     options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            //// });


            //// services.AddAuthorization(options =>
            //// {
            ////     options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
            ////         .RequireAuthenticatedUser()
            ////         .Build();
            //// }
            ////);
            //// services.AddAuthentication(o =>
            //// {
            ////     o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            ////     o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //// });


            //// services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            ////         .AddJwtBearer(options =>
            ////         {
            ////             options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
            ////             options.Authority = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
            ////         });
            //// api user claim policy
            ////services.AddAuthorization(options =>
            ////{
            ////    options.AddPolicy("ApiUser", policy => policy.RequireClaim("rol", ""));
            ////});
            #endregion

            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                 .RequireAuthenticatedUser()
                                 .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            #region Auth
            app.UseAuthentication();
            #endregion

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
