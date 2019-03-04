using System;
using System.Collections.Generic;
using System.IO;
using Dolittle.IO;
using Dolittle.Lifecycle;
using Dolittle.Serialization.Json;
using Dolittle.Tenancy;
using Octokit;

namespace Infrastructure.Services.Github.Webhooks.Handling
{
    [Singleton]
    public class TenantMapper : ITenantMapper
    {
        const string FileName = "installationToTenantMap.json";
        const string FilePathEnvName = "GITHUB_INSTALLATION_TENANT_MAP_PATH";

        readonly ISerializer _serializer;
        readonly IFileSystem _fileSystem;
        readonly string _filePath;

        Dictionary<long, Guid> _mapping;

        public TenantMapper(ISerializer serializer, IFileSystem fileSystem)
        {
            _serializer = serializer;
            _fileSystem = fileSystem;

            var rootPath = Environment.GetEnvironmentVariable(FilePathEnvName) ?? throw new Exception("GITHUB_INSTALLATION_TENANT_MAP_PATH not set");
            _filePath = Path.Combine(rootPath, FileName);
            
            _mapping = new Dictionary<long, Guid>();
            if (_fileSystem.Exists(_filePath))
            {
                var content = _fileSystem.ReadAllText(_filePath);
                _mapping = _serializer.FromJson<Dictionary<long, Guid>>(content);
            }
        }

        public TenantId GetTenantFor(InstallationId installationId)
        {
            if (_mapping.TryGetValue(installationId.Id, out var tenantId))
            {
                return tenantId;
            }
            return TenantId.Unknown;
        }

        public void SetTenantFor(InstallationId installationId, TenantId tenantId)
        {
            _mapping[installationId.Id] = tenantId;

            var content = _serializer.ToJson(_mapping);
            _fileSystem.WriteAllText(_filePath, content);
        }

        public void UnsetTenantFor(InstallationId installationId)
        {
            _mapping.Remove(installationId.Id);

            var content = _serializer.ToJson(_mapping);
            _fileSystem.WriteAllText(_filePath, content);
        }
    }
}