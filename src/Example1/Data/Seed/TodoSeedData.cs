using System;
using System.Collections.Generic;
using System.Linq;
using Example1.Models;

namespace Example1.Data.Seed
{
    public static class TodoSeedData
    {
        public static void EnsureSeedData(this TodoContext db)
        {
            if (db.Reminders.Any()) return;
            foreach (var reminder in Reminders)
            {
                db.Reminders.Add(reminder);
            }
            db.SaveChanges();
        }

        private static readonly IList<Reminder> Reminders = new List<Reminder>()
        {
            new Reminder {Id = Guid.NewGuid(), Description = "Buy Groceries"},
            new Reminder {Id = Guid.NewGuid(), Description = "Call Mother"},
            new Reminder {Id = Guid.NewGuid(), Description = "Get Oil Changed"},
            new Reminder {Id = Guid.NewGuid(), Description = "Clean House"},
            new Reminder {Id = Guid.NewGuid(), Description = "Do Laundry"}
        };
    }
}
