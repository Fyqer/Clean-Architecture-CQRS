using Webinar202103.Domain.Common;
using System.Collections.Generic;

namespace Webinar202103.Domain.Entities
{
    public class TaskList : AuditableEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public IList<Task> Items { get; private set; } = new List<Task>();
    }
}
