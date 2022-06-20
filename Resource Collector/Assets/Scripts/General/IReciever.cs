using System.Collections.Generic;

public interface IReceiver
{
    public bool TryReceiveResources(IEnumerable<ResourceType> resourceType);
}