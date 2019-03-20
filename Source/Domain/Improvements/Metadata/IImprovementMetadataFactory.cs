using System.Collections.Generic;

namespace Domain.Improvements.Metadata
{
    public interface IImprovementMetadataFactory
    {
        ImprovementMetadata BuildFrom(IDictionary<string,string> source, string sourceId);
    }
}