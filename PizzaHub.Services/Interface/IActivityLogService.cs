using PizzaHurt.Models;


namespace PizzaHurt.Services.Interface
{
    public interface IActivityLogService
    {
       void Log(ActvityLogModel model);
    }
}
