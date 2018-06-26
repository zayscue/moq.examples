using System.Linq;
using Example1.Data;
using Example1.Data.Repositories;
using Example1.Data.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Example1.Tests.Unit
{
    public class ReminderRepositoryShould
    {
        private readonly ReminderRepository _reminders;

        public ReminderRepositoryShould()
        {
            var logger = new Mock<ILogger<ReminderRepository>>();
            var options = new DbContextOptionsBuilder<TodoContext>()
                .UseInMemoryDatabase(databaseName: "TodoDb")
                .Options;
            var context = new TodoContext(options);
            context.EnsureSeedData();
            _reminders = new ReminderRepository(context, logger.Object);
        }

        [Fact]
        public void ReturnFiveReminders()
        {
            var reminders = _reminders.GetAll();
            Assert.Equal(5, reminders.Count());
        }
    }
}
