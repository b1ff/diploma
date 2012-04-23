using Gamification.Core.Entities;

namespace Gamification.Core.Domain
{
    public class ActionPerformError
    {
        public ActionPerformError(string message)
        {
            Message = message;
        }

        public Gamer Gamer { get; set; }

        public string Message { get; set; }
    }
}
