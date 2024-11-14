

namespace PizzaHurt.Models
{
    public class ActvityLogModel
    {

        public int LogId { get; set; }

        public string UserName { get; set; }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public int? ExecutionTimeMs { get; set; }

        public DateTime? Timestamp { get; set; }

        public string Message { get; set; }

    }
}
