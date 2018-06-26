using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Example1.Data.Abstractions;
using Example1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Example1.Data.Repositories
{
    public class ReminderRepository : RepositoryBase<Reminder>
    {
        private readonly ILogger<ReminderRepository> _logger;

        public ReminderRepository(TodoContext context, ILogger<ReminderRepository> logger) : base(context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (logger == null) throw new ArgumentNullException(nameof(logger));
            _logger = logger;
        }

        public override IEnumerable<Reminder> GetPaged(int top = 20, int skip = 0, Expression<Func<Reminder, bool>> filter = null, object orderBy = null)
        {
            try
            {
                var result = base.dbSet.AsNoTracking().AsQueryable();
                if (filter != null)
                    result = result.Where(filter);
                if (orderBy != null)
                {
                    var orderByStr = orderBy as string;
                    if (!string.IsNullOrWhiteSpace(orderByStr))
                    {
                        switch (orderByStr.ToLower().Trim())
                        {
                            case "id":
                                result = result.OrderBy(x => x.Id);
                                break;
                            case "description":
                                result = result.OrderBy(x => x.Description);
                                break;
                            case "wascompleted":
                                result = result.OrderBy(x => x.WasCompleted);
                                break;
                        }
                    }
                }
                return result.Skip(top * skip).Take(top);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error occurred while getting paged reminders.");
                throw;
            }
        }
    }
}
