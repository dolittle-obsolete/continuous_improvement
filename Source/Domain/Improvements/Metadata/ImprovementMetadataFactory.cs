using System;
using System.Collections.Generic;
using System.Text;
using Concepts.Improvables;
using Concepts.Improvements;
using Dolittle.Collections;
using Dolittle.Logging;
using Dolittle.Tenancy;
using Domain.Improvements.Metadata;

namespace Domain.Improvements.Metadata
{
    public class ImprovementMetadataFactory : IImprovementMetadataFactory
    {
        ImprovementMetadataValidator _validator; 

        public ImprovementMetadataFactory(ImprovementMetadataValidator validator) => _validator = validator;
        public ImprovementMetadata BuildFrom(IDictionary<string,string> source, string sourceId)
        {
            var metadata = new ImprovementMetadata(
                ExtractLabelAsGuid(source, Constants.Tenant),
                ExtractLabelAsString(source,Constants.RecipeType),
                ExtractLabelAsGuid(source, Constants.Improvement),
                ExtractLabelAsGuid(source, Constants.Improvable),
                ExtractLabelAsString(source, Constants.Version)
            );

            var valid  = _validator.Validate(metadata);
            if(!valid.IsValid)
            {
                var sb = new StringBuilder();
                sb.AppendLine($"Pod '{sourceId}' in build-pod namespace has invalid metadata. The pod will be deleted.");
                valid.Errors.ForEach(_ => sb.AppendLine($"{_.PropertyName} is missing or invalid"));
                throw new InvalidImprovementMetadata(sb.ToString());
            }

            return metadata;
        }

        Guid ExtractLabelAsGuid(IDictionary<string,string> source, string labelKey)
        {
            if (!source.ContainsKey(labelKey))
                return Guid.Empty;

            Guid guid;  
            return Guid.TryParse(source[labelKey], out guid) ? guid : Guid.Empty;  
        }

        string ExtractLabelAsString(IDictionary<string,string> source, string labelKey)
        {
            if (!source.ContainsKey(labelKey))
                return null;
            return source[labelKey];
        }
    }
}