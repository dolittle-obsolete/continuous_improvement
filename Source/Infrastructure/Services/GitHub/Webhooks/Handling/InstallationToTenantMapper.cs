using System;
using System.Collections.Generic;
using System.IO;
using Dolittle.IO;
using Dolittle.Lifecycle;
using Dolittle.Serialization.Json;
using Dolittle.Tenancy;
using Concepts.SourceControl.GitHub;
using System.Linq;
using System.Collections.Concurrent;

namespace Infrastructure.Services.Github.Webhooks.Handling
{
    /// <inheritdoc />
    [Singleton]
    public class InstallationToTenantMapper : IInstallationToTenantMapper
    {
        const string _filePath = "Data/SourceControl/GitHub/installationToTenantMap.json";

        readonly ISerializer _serializer;
        readonly IFileSystem _fileSystem;

        ConcurrentDictionary<long, Guid> _mapping;

        /// <summary>
        /// Instantiates a new instance of <see cref="InstallationToTenantMapper" />
        /// </summary>
        /// <param name="serializer">A serializer for reading the mapping from the file</param>
        /// <param name="fileSystem">File system access</param>
        public InstallationToTenantMapper(ISerializer serializer, IFileSystem fileSystem)
        {
            _serializer = serializer;
            _fileSystem = fileSystem;
            var content = _fileSystem.ReadAllText(_filePath);
            _mapping = _serializer.FromJson<ConcurrentDictionary<long, Guid>>(content);
        }
        
        /// <inheritdoc />
        public TenantId GetTenantFor(InstallationId installationId)
        {
            if (_mapping.TryGetValue(installationId, out var tenantId))
            {
                return tenantId;
            }
            return TenantId.Unknown;
        }
        
        /// <inheritdoc />
        public void AssociateTenantWithInstallation(InstallationId installationId, TenantId tenantId)
        {
            _mapping[installationId] = tenantId;
            _mapping.AddOrUpdate(installationId, tenantId, (k,v) => tenantId);

            var content = _serializer.ToJson(_mapping);
            _fileSystem.WriteAllText(_filePath, content);
        }

        /// <inheritdoc />
        public void DisassociateTenantFromInstallation(InstallationId installationId)
        {
            Guid removed;
            if(_mapping.TryRemove(installationId, out removed))
            {
                var content = _serializer.ToJson(_mapping);
                _fileSystem.WriteAllText(_filePath, content);
            }
        }

        /// <inheritdoc />
        public IEnumerable<InstallationId> GetInstallationsFor(TenantId tenant)
        {
            return _mapping.Where(_ => _.Value == tenant).Select(_ => (InstallationId)_.Key).ToList();
        }
    }
}