using CorporateArenasBackend.Repositories.Role;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace CorporateArenasBackend.Utilities
{
    public class HasPermissionAttribute : ActionFilterAttribute
    {
        private readonly Actions _action;
        private readonly Entities _entity;
        private readonly int _roleId;
        private IRoleRepository _roleRepository;

        public HasPermissionAttribute(int roleId, Actions action, Entities entity)
        {
            _action = action;
            _entity = entity;
            _roleId = roleId;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (HasPermission())
            {
                context.HttpContext.Response.StatusCode = 401;
            }
            base.OnActionExecuting(context);
        }

        public bool HasPermission()
        {
            var role = _roleRepository.GetById(_roleId).GetAwaiter().GetResult();
            var canPerformAction = role.Permissions.FirstOrDefault(p => p.Entity == _entity.ToString() && p.Action == _action.ToString());
            return canPerformAction != null;
        }
    }
}