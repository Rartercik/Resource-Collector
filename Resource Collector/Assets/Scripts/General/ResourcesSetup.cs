using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourcesSetup : MonoBehaviour
{
    [SerializeField] ResourceType[] _keys;
    [SerializeField] GameObject[] _values;

    private Dictionary<ResourceType, GameObject> _resources;

    private void OnValidate()
    {
        _resources = new Dictionary<ResourceType, GameObject>();

        var distincedKeys = _keys.Distinct().ToArray();
        var resourcesCount = Enum.GetValues(typeof(ResourceType)).Length;

        for (int i = 0; i < resourcesCount; i++)
        {
            _resources.Add(distincedKeys[i], _values[i]);
        }
    }

    public GameObject GetPrefabByType(ResourceType type)
    {
        return _resources[type];
    }

    public string GetTagByType(ResourceType resourceType)
    {
        return _resources[resourceType].tag;
    }
}
