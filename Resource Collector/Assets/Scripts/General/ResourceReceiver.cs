using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceReceiver : MonoBehaviour, IReceiver
{
    [SerializeField] ResourceType[] _requredResourceTypes;
    [SerializeField] Text _factoryInterface;

    private int[] _resources = new int[Enum.GetValues(typeof(ResourceType)).Length];
    private Action _onRecieve;

    public IEnumerable<ResourceType> RequredResourceTypes => _requredResourceTypes;

    public void SetOnRecieveAction(Action onRecieve)
    {
        _onRecieve = onRecieve;
    }

    public bool TryReceiveResources(IEnumerable<ResourceType> resourceTypes)
    {
        foreach (var type in resourceTypes)
        {
            var resourceIndex = (int)type;
            _resources[resourceIndex]++;
        }

        _onRecieve?.Invoke();
        return true;
    }

    public void DeleteResources()
    {
        foreach (var type in _requredResourceTypes)
        {
            var resourceIndex = (int)type;
            _resources[resourceIndex]--;
        }
    }

    public bool HasAllComponents()
    {
        foreach (var resourceType in _requredResourceTypes)
        {
            var resource = _resources[(int)resourceType];
            if (resource == 0)
                return false;
        }
        return true;
    }

    public void UpdateInterface(string argument)
    {
        _factoryInterface.text = argument;
    }
}
