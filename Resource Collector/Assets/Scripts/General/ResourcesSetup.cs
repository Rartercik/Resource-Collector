using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class ResourcesSetup : ScriptableObject
{
    [SerializeField] ResourceType[] _keys;
    [SerializeField] Material[] _values;

    private Dictionary<ResourceType, Material> _resources;

    private void OnValidate()
    {
        _resources = new Dictionary<ResourceType, Material>();

        var distincedKeys = _keys.Distinct().ToArray();
        var resourcesCount = Enum.GetValues(typeof(ResourceType)).Length;

        for (int i = 0; i < resourcesCount; i++)
        {
            _resources.Add(distincedKeys[i], _values[i]);
        }
    }

    public Material GetMaterialByType(ResourceType type)
    {
        return _resources[type];
    }
}
