using Microsoft.AspNetCore.Mvc.Filters;
using PizzaHurt.Models;
using PizzaHurt.Services.Interface;
using System.Diagnostics;


namespace PizzaHurt.UI.Helpers
{
    public class ActivityLogFilter : Attribute, IActionFilter
    {
        private Stopwatch _stopwatch;
        private IActivityLogService _activityLogService;

        public ActivityLogFilter(IActivityLogService activityLogService)
        {

            _activityLogService = activityLogService;
        }

        //Pre Runs before the action method is executed.
        public void OnActionExecuting(ActionExecutingContext context)
        {
            _stopwatch = Stopwatch.StartNew();
            var controller = context.ActionDescriptor.RouteValues["controller"];
            var action = context.ActionDescriptor.RouteValues["action"];
            var username = context.HttpContext.User.Identity.IsAuthenticated ? context.HttpContext.User.Identity.Name : "Anonymous";

            var message = $"User '{username}' started executing '{controller}/{action}'";
            // var message = $" User {username} Started Executing {controller}/{action}";

            var logend = new ActvityLogModel
            {
                UserName = username,
                ControllerName = controller,
                ActionName = action,
                Message = message,
                ExecutionTimeMs = 0
            };


            _activityLogService.Log(logend);
        }

        //Post Runs after the action method is executed.
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _stopwatch.Stop();
            var controller = context.ActionDescriptor.RouteValues["controller"];
            var action = context.ActionDescriptor.RouteValues["action"];
            //var username = context.HttpContext.User.Identity.IsAuthenticated ?context.HttpContext.User.Identity.Name : "Anonymous";
            var username = context.HttpContext.User.Identity.IsAuthenticated ? context.HttpContext.User.Identity.Name : "Anonymous";

            var executionTime = (int)_stopwatch.ElapsedMilliseconds;
            var message = $"User '{username}' completed executing '{controller}/{action}' in {executionTime} ms.";


            var logend = new ActvityLogModel
            {
                UserName = username,
                ControllerName = controller,
                ActionName = action,
                Message = message,
                ExecutionTimeMs = executionTime
            };

            _activityLogService.Log(logend);
        }






    }
}
