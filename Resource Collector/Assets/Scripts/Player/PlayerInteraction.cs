using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(ResourceGiver))]
public class PlayerInteraction : MonoBehaviour, IReceiver, IGiver
{
    [SerializeField] PlayerResourcesVisualization _visualization;
    [SerializeField] int _resourceCapacity;

    private IEnumerable<ResourceType> _currentGivingTypes;
    private uint[] _resourcesCount = new uint[Enum.GetValues(typeof(ResourceType)).Length];
    private ResourceGiver _giver;

    public int ResourceCount => GetTotalResourceCount();

    private bool IsFull => GetTotalResourceCount() >= _resourceCapacity;

    private void Start()
    {
        _giver = GetComponent<ResourceGiver>();
        _visualization.Initialize(_resourceCapacity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ResourceReceiver receiver))
        {
            var enableResourceTypes = receiver.RequredResourceTypes.Where(type => _resourcesCount[(int)type] > 0);
            _currentGivingTypes = enableResourceTypes;
            _giver.StartGiving(receiver, enableResourceTypes, TakeOneResourceFromCurrentTypes);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out ResourceReceiver receiver))
        {
            _giver.StopGiving();
        }
    }

    public bool TryReceiveResources(IEnumerable<ResourceType> resourceTypes)
    {
        if (IsFull) return false;

        var resourceType = resourceTypes.First();
        var resourceIndex = (int)resourceType;
        _resourcesCount[resourceIndex]++;
        _visualization.AddResourceOfType(resourceType);

        return true;
    }

    private void TakeOneResourceFromCurrentTypes()
    {
        foreach (var type in _currentGivingTypes)
        {
            _resourcesCount[(int)type]--;
            _visualization.RemoveResourceOfType(type);
        }
    }

    private int GetTotalResourceCount()
    {
        int total = 0;
        foreach (var resourceCount in _resourcesCount)
            total += (int)resourceCount;
        return total;
    }
}
