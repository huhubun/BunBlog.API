using BunBlog.Core.Domain.Settings;
using BunBlog.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BunBlog.Services.Settings
{
    public class SettingService : ISettingService
    {
        private readonly BunBlogContext _bunBlogContext;

        public SettingService(BunBlogContext bunBlogContext)
        {
            _bunBlogContext = bunBlogContext;
        }

        public async Task<List<SettingDefinition>> GetDefinitionsAsync()
        {
            using (var fs = File.OpenRead("blogSettingResources.json"))
            {
                var serializerOptions = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                serializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));

                return await JsonSerializer.DeserializeAsync<List<SettingDefinition>>(fs, serializerOptions);
            };
        }

        public async Task<SettingDefinition> GetDefinitionByCodeAsync(string code)
        {
            return (await GetDefinitionsAsync()).SingleOrDefault(r => r.Code == code);
        }

        public SettingVerifyResult Verify(string value, SettingDefinition definition)
        {
            if (!definition.AllowNull && value == null)
            {
                return new SettingVerifyResult("cannot be null");
            }

            return new SettingVerifyResult(isVerify: true);
        }

        public async Task<List<Setting>> GetListAsync()
        {
            return await _bunBlogContext.Setting.AsNoTracking().ToListAsync();
        }

        public async Task<Setting> GetByCodeAsync(string code, bool tracking = false)
        {
            var settings = _bunBlogContext.Setting.Where(c => c.Code == code);

            if (!tracking)
            {
                settings = settings.AsNoTracking();
            }

            return await settings.SingleOrDefaultAsync();
        }

        public async Task<Setting> AddAsync(Setting setting)
        {
            _bunBlogContext.Setting.Add(setting);
            await _bunBlogContext.SaveChangesAsync();

            return setting;
        }

        public async Task<Setting> EditAsync(Setting setting)
        {
            _bunBlogContext.Entry(setting).State = EntityState.Modified;
            await _bunBlogContext.SaveChangesAsync();

            return setting;
        }


    }
}
