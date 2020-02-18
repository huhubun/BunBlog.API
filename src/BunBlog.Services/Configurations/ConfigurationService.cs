using BunBlog.Core.Domain.Configurations;
using BunBlog.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BunBlog.Services.Configurations
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly BunBlogContext _bunBlogContext;

        public ConfigurationService(BunBlogContext bunBlogContext)
        {
            _bunBlogContext = bunBlogContext;
        }

        public async Task<List<ConfigurationResourceItem>> GetConfigurationResourcesAsync()
        {
            using (var fs = File.OpenRead("configurationResources.json"))
            {
                var serializerOptions = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                serializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));

                return await JsonSerializer.DeserializeAsync<List<ConfigurationResourceItem>>(fs, serializerOptions);
            };
        }

        public async Task<ConfigurationResourceItem> GetConfigurationResourceByCodeAsync(string code)
        {
            return (await GetConfigurationResourcesAsync()).SingleOrDefault(r => r.Code == code);
        }

        public ConfigurationVerifyResult Verify(string value, ConfigurationResourceItem configurationResource)
        {
            if (!configurationResource.AllowNull && value == null)
            {
                return new ConfigurationVerifyResult("cannot be null");
            }

            return new ConfigurationVerifyResult(isVerify: true);
        }

        public async Task<List<Configuration>> GetListAsync()
        {
            return await _bunBlogContext.Configuration.AsNoTracking().ToListAsync();
        }

        public async Task<Configuration> GetByCodeAsync(string code, bool tracking = false)
        {
            var configuration = _bunBlogContext.Configuration.Where(c => c.Code == code);

            if (!tracking)
            {
                configuration = configuration.AsNoTracking();
            }

            return await configuration.SingleOrDefaultAsync();
        }

        public async Task<Configuration> AddAsync(Configuration configuration)
        {
            _bunBlogContext.Configuration.Add(configuration);
            await _bunBlogContext.SaveChangesAsync();

            return configuration;
        }

        public async Task<Configuration> EditAsync(Configuration configuration)
        {
            _bunBlogContext.Entry(configuration).State = EntityState.Modified;
            await _bunBlogContext.SaveChangesAsync();

            return configuration;
        }


    }
}
