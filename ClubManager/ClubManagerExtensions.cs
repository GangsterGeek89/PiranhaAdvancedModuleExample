using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Piranha;
using Piranha.AspNetCore;
using ClubManager;

public static class ClubManagerExtensions
{
    /// <summary>
    /// Adds the ClubManager module.
    /// </summary>
    /// <param name="services">The current service collection</param>
    /// <returns>The services</returns>
    public static IServiceCollection AddClubManager(this IServiceCollection services)
    {
        // Add the ClubManager module
        Piranha.App.Modules.Register<Module>();

        // Setup authorization policies
        services.AddAuthorization(o =>
        {
            // ClubManager policies
            o.AddPolicy(Permissions.ClubEditor, policy =>
            {
                policy.RequireClaim(Permissions.ClubEditor, Permissions.ClubEditor);
            });

            // ClubManager add policy
            o.AddPolicy(Permissions.ClubEditorAdd, policy =>
            {
                policy.RequireClaim(Permissions.ClubEditor, Permissions.ClubEditor);
                policy.RequireClaim(Permissions.ClubEditorAdd, Permissions.ClubEditorAdd);
            });

            // ClubManager edit policy
            o.AddPolicy(Permissions.ClubEditorEdit, policy =>
            {
                policy.RequireClaim(Permissions.ClubEditor, Permissions.ClubEditor);
                policy.RequireClaim(Permissions.ClubEditorEdit, Permissions.ClubEditorEdit);
            });

            // ClubManager delete policy
            o.AddPolicy(Permissions.ClubEditorDelete, policy =>
            {
                policy.RequireClaim(Permissions.ClubEditor, Permissions.ClubEditor);
                policy.RequireClaim(Permissions.ClubEditorDelete, Permissions.ClubEditorDelete);
            });
        });

        // Return the service collection
        return services;
    }

    /// <summary>
    /// Uses the ClubManager.
    /// </summary>
    /// <param name="builder">The application builder</param>
    /// <returns>The builder</returns>
    public static IApplicationBuilder UseClubManager(this IApplicationBuilder builder)
    {
        return builder.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new EmbeddedFileProvider(typeof(Module).Assembly, "ClubManager.assets.dist"),
            RequestPath = "/manager/ClubManager"
        });
    }

    /// <summary>
    /// Static accessor to ClubManager module if it is registered in the Piranha application.
    /// </summary>
    /// <param name="modules">The available modules</param>
    /// <returns>The ClubManager module</returns>
    public static Module ClubManager(this Piranha.Runtime.AppModuleList modules)
    {
        return modules.Get<Module>();
    }
}
