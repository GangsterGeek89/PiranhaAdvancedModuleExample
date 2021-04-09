using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Piranha;
using Piranha.AttributeBuilder;
using Piranha.AspNetCore.Identity.SQLite;
using Piranha.Data.EF.SQLite;
using Piranha.Manager.Editor;
using ClubManager;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using System.IO;
using Microsoft.Extensions.FileProviders;

namespace ClubSite
{
    public class Startup
    {
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment webHostEnvironment;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="configuration">The current configuration</param>
        public Startup(IConfiguration configuration, IWebHostEnvironment whe)
        {
            _config = configuration;
            webHostEnvironment = whe;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Service setup
            services.AddPiranha(options =>
            {
                options.AddRazorRuntimeCompilation = true;

                options.UseFileStorage(naming: Piranha.Local.FileStorageNaming.UniqueFolderNames);
                options.UseImageSharp();
                options.UseManager();
                options.UseTinyMCE();
                options.UseMemoryCache();
                // Use Club Manager
                options.UseClubManager();
                options.UseEF<SQLiteDb>(db =>
                    db.UseSqlite(_config.GetConnectionString("piranha")));
                options.UseIdentityWithSeed<IdentitySQLiteDb>(db =>
                    db.UseSqlite(_config.GetConnectionString("piranha")));

                /***
                 * Here you can configure the different permissions
                 * that you want to use for securing content in the
                 * application.
                options.UseSecurity(o =>
                {
                    o.UsePermission("WebUser", "Web User");
                });
                 */
            });

            services.Configure<MvcRazorRuntimeCompilationOptions>(options =>
            {
                var libPath = Path.GetFullPath(Path.Combine(webHostEnvironment.ContentRootPath, "..", "ClubManager"));
                options.FileProviders.Add(new PhysicalFileProvider(libPath));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApi api)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Initialize Piranha
            App.Init(api);

            // Build content types
            new ContentTypeBuilder(api)
                .AddAssembly(typeof(Startup).Assembly)
                //.AddAssembly(typeof(Module).Assembly)
                //Register Club Content Types
                .UseClubManager()
                .Build()
                .DeleteOrphans();

            // Configure Tiny MCE
            EditorConfig.FromFile("editorconfig.json");

            // Middleware setup
            app.UsePiranha(options => {
                options.UseManager();
                options.UseTinyMCE();
                options.UseIdentity();
                // Use the club manager
                options.UseClubManager();
            });
        }
    }
}
