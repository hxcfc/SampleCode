using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace CloudSign.Api.Middleware.ApiFilter
{
    public class ApiExplorerGroupPerApiConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            var controllerNamespace = controller.ControllerType.Namespace; // e.g. "Controllers.AtnaControllers"
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var groupName = controllerNamespace.Split('.').Last().ToLower();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            controller.ApiExplorer.GroupName = groupName;
        }
    }
}