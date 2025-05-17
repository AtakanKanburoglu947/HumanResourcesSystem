using HumanResourcesSystemCore.Models;

namespace HumanResourcesSystem.Models
{
        public class DailyTaskPageModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public bool? isFinished { get; set; }
            public List<DailyTask> DailyTasks { get; set; }
        }
}
