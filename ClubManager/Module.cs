using Piranha;
using Piranha.Extend;
using Piranha.Manager;
using Piranha.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClubManager
{
    public class Module : IModule
    {
        private readonly List<PermissionItem> _permissions = new List<PermissionItem>
        {
            new PermissionItem { Name = Permissions.ClubEditor, Title = "List Club content", Category = "Club Editor", IsInternal = true },
            new PermissionItem { Name = Permissions.ClubEditorAdd, Title = "Add Club content", Category = "Club Editor", IsInternal = true },
            new PermissionItem { Name = Permissions.ClubEditorEdit, Title = "Edit Club content", Category = "Club Editor", IsInternal = true },
            new PermissionItem { Name = Permissions.ClubEditorDelete, Title = "Delete Club content", Category = "Club Editor", IsInternal = true },
            new PermissionItem { Name = Permissions.ClubEditorPreview, Title = "Preview Club content", Category = "Club Editor", IsInternal = true },
            new PermissionItem { Name = Permissions.ClubEditorPublish, Title = "Publish Club content", Category = "Club Editor", IsInternal = true },
            new PermissionItem { Name = Permissions.ClubEditorSave, Title = "Save Club content", Category = "Club Editor", IsInternal = true }
        };

        /// <summary>
        /// Gets the module author
        /// </summary>
        public string Author => "";

        /// <summary>
        /// Gets the module name
        /// </summary>
        public string Name => "";

        /// <summary>
        /// Gets the module version
        /// </summary>
        public string Version => Utils.GetAssemblyVersion(GetType().Assembly);

        /// <summary>
        /// Gets the module description
        /// </summary>
        public string Description => "";

        /// <summary>
        /// Gets the module package url
        /// </summary>
        public string PackageUrl => "";

        /// <summary>
        /// Gets the module icon url
        /// </summary>
        public string IconUrl => "/manager/PiranhaModule/piranha-logo.png";

        public void Init()
        {
            // Register permissions
            foreach (var permission in _permissions)
            {
                App.Permissions["ClubManager"].Add(permission);
            }

            // Add manager menu items
            Menu.Items.Add(new MenuItem
            {
                InternalId = "ClubManagement",
                Name = "Club Management",
                Css = "fas fa-box"
            });

            Menu.Items["ClubManagement"].Items.Add(new MenuItem
            {
                InternalId = "ClubManagerStart",
                Name = "Pages",
                Route = "~/manager/clubmanager",
                Policy = Permissions.ClubEditor,
                Css = "fas fa-box"
            });
        }
    }
}
