using System;

namespace Example1.Models
{
    public class Reminder
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public bool WasCompleted { get; set; }
    }
}
