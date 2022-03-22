using BunBlog.API.Models.Settings;
using BunBlog.Core.Domain.Settings;

namespace BunBlog.API.MappingProfiles
{
    public class SettingsProfile: Profile
    {
        public SettingsProfile()
        {
            CreateMap<Setting, SettingsValueModel>();
        }
    }
}
