
using PizzaHurt.Models;
using PizzaHurt.Repositories.Interfaces;
using PizzaHurt.Services.Interface;
using PizzaHut.Core.Entities;


namespace PizzaHurt.Services.Implementation
{
    public class ActivityLogService : IActivityLogService
    {
        private IActivityLogRepository _activityLogRepository;

        public ActivityLogService(IActivityLogRepository activityLogRepository)
        {
            _activityLogRepository = activityLogRepository;
        }

        public void Log(ActvityLogModel model)
        {
            TrackActivityLog activity = new TrackActivityLog
            {
                UserName= model.UserName,
                ControllerName=model.ControllerName,
                ActionName=model.ActionName,
                ExecutionTimeMs=model.ExecutionTimeMs,
                Timestamp = DateTime.Now,
                Message=model.Message
            };
            _activityLogRepository.Add(activity);
           
           
        }
    }
}
