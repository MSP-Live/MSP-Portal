using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSP_Portal.Controllers;
using MSP_Portal.MessageProcessors;

namespace MSP_Portal.MessageProcessors
{
    public class ActivitiesProcessor : MessagesController.MessageProcessor
    {
        public ActivitiesProcessor()
        {
            Processors.Add("add",
                new MessagesController.MessageProcess(Add));
        }

        public string Add(int id, string[] args)
        {
            Entities.Activity newActivity = new Entities.Activity();
            newActivity.UserId = id;
            if (args.Length > 3)
            {
                newActivity.Name = args[0];
                newActivity.ActivityTypeId = GetActivityTypeId(args[1]);
                StringBuilder builder = new StringBuilder();
                for (int i = 2; i < args.Length; i++)
                {
                    builder.Append(args[i]).Append(' ');
                }
                newActivity.Note = builder.ToString();
                using (DBContext.PortalContext db = new DBContext.PortalContext())
                {
                    db.ActivitiesCollection.Add(newActivity);
                    return "New activity successful added.";
                }
            }
            else
            {
                throw new ArgumentException("Not corrected input data.");
            }
        }

        private int GetActivityTypeId(string name)
        {
            using (DBContext.PortalContext db = new DBContext.PortalContext())
            {
                Entities.ActivityType activityType = db.ActivityTypesCollection.FirstOrDefault(x => x.Name == name);
                if (activityType == null)
                {
                    return activityType.Id;
                }
                else
                {
                    throw new ArgumentException("Activity type not found.");
                }
            }
        }

        public string Update(int id, string[] args)
        {
            using (DBContext.PortalContext db = new DBContext.PortalContext())
            {
                Entities.Activity activity = db.ActivitiesCollection.FirstOrDefault(x => x.Id == int.Parse(args[0]));
                if (activity == null)
                {
                    throw new ArgumentException("Activity not found.");
                }
                else
                {
                    if (args.Length > 4)
                    {
                        activity.Name = args[1];
                        activity.ActivityTypeId = GetActivityTypeId(args[2]);
                        StringBuilder builder = new StringBuilder();
                        for (int i = 3; i < args.Length; i++)
                        {
                            builder.Append(args[i]).Append(' ');
                        }
                        activity.Note = builder.ToString();
                        return "Activity successful updated.";
                    }
                    else
                    {
                        throw new ArgumentException("Not corrected input data.");
                    }
                }
            }
        }
    }
}
