using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Piranha;
using Piranha.AspNetCore;
using Piranha.AttributeBuilder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubManager
{
    public static class ClubManagerStartup
    {
        /// <summary>
        /// Adds the ClubManager module
        /// </summary>
        /// <param name="serviceBuilder">The service builder</param>
        /// <returns>The updated service builder</returns>
        public static PiranhaServiceBuilder UseClubManager(this PiranhaServiceBuilder serviceBuilder)
        {
            serviceBuilder.Services.AddClubManager();

            //serviceBuilder.Services.AddRazorPages();

            

            return serviceBuilder;
        }

        /// <summary>
        /// Uses the ClubManager module
        /// </summary>
        /// <param name="serviceBuilder">The application builder</param>
        /// <returns>The updated application builder</returns>
        public static PiranhaApplicationBuilder UseClubManager(this PiranhaApplicationBuilder applicationBuilder)
        {
            applicationBuilder.Builder.UseClubManager();

            //applicationBuilder.Builder.UseEndpoints(endpoints => {
            //    endpoints.MapRazorPages();
            //});

            return applicationBuilder;
        }

        /// <summary>
        /// Adds the club manager content types
        /// </summary>
        /// <param name="contentTypeBuilder">The content type builder</param>
        /// <returns>The updated content type builder</returns>
        public static ContentTypeBuilder UseClubManager(this ContentTypeBuilder contentTypeBuilder)
        {
            // Register Our Content Types
            contentTypeBuilder.AddAssembly(typeof(Module).Assembly);

            return contentTypeBuilder;
        }
    }
}
