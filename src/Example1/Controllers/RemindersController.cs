using System;
using System.Threading.Tasks;
using Example1.Data.Abstractions;
using Example1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Example1.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class RemindersController : ControllerBase
    {
        private readonly IRepositoryBase<Reminder> _reminders;

        public RemindersController(IRepositoryBase<Reminder> reminders)
        {
            if(reminders == null) throw new ArgumentNullException(nameof(reminders));
            _reminders = reminders;
        }

        [HttpGet]
        public IActionResult GetAll(bool? completed = null)
        {
            var reminders = completed != null
                ? _reminders.GetAll(x => x.WasCompleted == completed.Value)
                : _reminders.GetAll();
            return Ok(reminders);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var reminder = await _reminders.GetByIdAsync(id);
                return Ok(reminder);
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Reminder reminder)
        {
            if (reminder == null) return BadRequest((new ArgumentNullException(nameof(reminder))).Message);
            reminder.Id = new Guid();
            await _reminders.InsertAsync(reminder);
            await _reminders.CommitAsync();
            return Ok(reminder);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Reminder reminder)
        {
            if (reminder == null) return BadRequest((new ArgumentNullException(nameof(reminder))).Message);
            if (reminder.Id != id) return BadRequest("Id and entity identification miss match");
            await _reminders.UpdateAsync(reminder);
            await _reminders.CommitAsync();
            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var reminder = await _reminders.DeleteAsync(id);
            await _reminders.CommitAsync();
            return Ok(reminder);
        }
    }
}
