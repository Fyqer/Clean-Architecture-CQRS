using AutoMapper;
using Webinar202103.Application.Common.Mappings;
using Webinar202103.Domain.Entities;

namespace Webinar202103.Application.TaskLists.Queries.GetTodos
{
    public class TaskDto : IMapFrom<Task>
    {
        public int Id { get; set; }

        public int ListId { get; set; }

        public string Title { get; set; }

        public bool Done { get; set; }

        public int Priority { get; set; }

        public string Note { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Task, TaskDto>()
                .ForMember(d => d.Priority, opt => opt.MapFrom(s => (int)s.Priority));
        }
    }
}
