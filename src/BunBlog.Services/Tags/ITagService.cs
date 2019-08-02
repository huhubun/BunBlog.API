using BunBlog.Core.Domain.Tags;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BunBlog.Services.Tags
{
    public interface ITagService
    {
        Task<List<Tag>> GetListAsync();

        Task<List<Tag>> GetListByLinkNameAsync(bool tracking = false, params string[] linkNames);

        Task<Tag> GetByLinkNameAsync(string linkName, bool tracking = false);

        Task<Tag> AddAsync(Tag tag);

        Task<Tag> EditAsync(Tag tag);

        Task DeleteAsync(Tag tag);
    }
}
