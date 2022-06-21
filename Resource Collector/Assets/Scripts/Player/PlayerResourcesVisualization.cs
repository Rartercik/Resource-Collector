using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class PlayerResourcesVisualization : MonoBehaviour
{
    [SerializeField] Transform _player;
    [SerializeField] GameObject _defaultResource;

    [Inject] ResourcesSetup _resourcesSetup;

    private Transform _transform;
    private List<MeshRenderer> _resources = new List<MeshRenderer>();
    private int _lastActivatedIndex = -1;

    public void Initialize(int maxResourceCount)
    {
        _transform = transform;

        var lastResoure = Instantiate(_defaultResource, _transform.position, _player.rotation, _transform);
        _resources.Add(lastResoure.GetComponent<MeshRenderer>());
        lastResoure.SetActive(false);

        for(int i = 1; i < maxResourceCount; i++)
        {
            lastResoure = CreateNextTo(lastResoure);
            _resources.Add(lastResoure.GetComponent<MeshRenderer>());
            lastResoure.SetActive(false);
        }
    }

    public void AddResourceOfType(ResourceType resourceType)
    {
        _lastActivatedIndex++;

        _resources[_lastActivatedIndex].gameObject.SetActive(true);

        _resources[_lastActivatedIndex].material = _resourcesSetup.GetMaterialByType(resourceType);
    }

    public void RemoveResourceOfType(ResourceType resourceType)
    {
        var lastResourceOfTypeIndex = _resources.FindLastIndex(resource => resource.sharedMaterial ==
                                                        _resourcesSetup.GetMaterialByType(resourceType));

        var upperResources = _resources.Skip(lastResourceOfTypeIndex).ToArray();

        var lastIndex = upperResources.Length - 1;
        for (int i = 0; i < upperResources.Length; i++)
        {
            var nextIndex = i + 1;
            if (i != lastIndex)
                upperResources[i].material = upperResources[nextIndex].material;
            else
                upperResources[i].material = null;
        }
        _resources[_lastActivatedIndex].gameObject.SetActive(false);
        _lastActivatedIndex--;
    }

    private GameObject CreateNextTo(GameObject lastGameobject)
    {
        var lastGameobjectHeight = lastGameobject.GetComponent<Renderer>().bounds.size.y;
        var resultPosition = lastGameobject.transform.position + Vector3.up * lastGameobjectHeight;

        var result = Instantiate(_defaultResource, resultPosition, _player.rotation, _transform);
        return result;
    }
}
