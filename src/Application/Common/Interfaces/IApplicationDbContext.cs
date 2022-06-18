using Webinar202103.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Task = Webinar202103.Domain.Entities.Task;

namespace Webinar202103.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<TaskList> TaskLists { get; set; }

        DbSet<Task> Tasks { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
