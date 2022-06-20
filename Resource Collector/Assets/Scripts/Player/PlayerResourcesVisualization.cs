using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class PlayerResourcesVisualization : MonoBehaviour
{
    [SerializeField] Transform _player;

    [Inject] ResourcesSetup _resourcesSetup;

    private Transform _transform;
    private List<GameObject> _resources = new List<GameObject>();
    private int _lastActivatedIndex = -1;

    private void Start()
    {
        _transform = transform;
    }

    public void AddResourceOfType(ResourceType resourceType)
    {
        _lastActivatedIndex++;
        GameObject resource;
        if (_resources.Count == 0)
            resource = Instantiate(_resourcesSetup.GetPrefabByType(resourceType), _transform.position, _player.rotation, _transform);
        else
            resource = CreateNextTo(_resources[_lastActivatedIndex-1], _resourcesSetup.GetPrefabByType(resourceType));
        _resources.Add(resource);

        /*_resources[_lastActivatedIndex].material = _resourcesSetup.GetMaterialByType(resourceType); doesn't work in build*/
    }

    public void RemoveResourceOfType(ResourceType resourceType)
    {
        var lastResourceOfTypeIndex = _resources.FindLastIndex(resource => resource.tag ==
                                                        _resourcesSetup.GetTagByType(resourceType));

        var upperResources = _resources.Skip(lastResourceOfTypeIndex).ToArray();

        var lastIndex = upperResources.Length - 1;
        for (int i = lastIndex; i > 0; i--)
        {
            var previous = i - 1;
            upperResources[i].transform.position = upperResources[previous].transform.position;
        }
        _resources.Remove(upperResources[0]);
        Destroy(upperResources[0]);
        _lastActivatedIndex--;
    }

    private GameObject CreateNextTo(GameObject lastGameobject, GameObject prefab)
    {
        var lastGameobjectHeight = lastGameobject.GetComponent<Renderer>().bounds.size.y;
        var resultPosition = lastGameobject.transform.position + Vector3.up * lastGameobjectHeight;

        var result = Instantiate(prefab, resultPosition, _player.rotation, _transform);
        return result;
    }
}
