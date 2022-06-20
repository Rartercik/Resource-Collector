using UnityEngine;

[RequireComponent(typeof(ResourceGiver))]
public class ResourceFactory : MonoBehaviour, IGiver
{
    [SerializeField] ResourceType _resourceType;
    [SerializeField] ResourceReceiver _resourceReceiver;
    [SerializeField] int _resourceCapacity;
    [SerializeField] float _producingDuration;

    private ResourceGiver _giver;
    private float _progress;

    public int ResourceCount { get; private set; }

    private bool IsFull => ResourceCount >= _resourceCapacity;

    private void Start()
    {
        _giver = GetComponent<ResourceGiver>();
    }

    private void Update()
    {
        Produce();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerInteraction player))
        {
            _giver.StartGiving(player, new ResourceType[] { _resourceType }, () => ResourceCount--);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerInteraction player))
        {
            _giver.StopGiving();
        }
    }

    private void Produce()
    {
        if (IsFull)
        {
            _resourceReceiver.UpdateInterface("Is Full!");
            return;
        }
        if(_resourceReceiver.HasAllComponents() == false)
        {
            _resourceReceiver.UpdateInterface("No Requred Components!");
            return;
        }

        _resourceReceiver.UpdateInterface("Free Resources!");

        if (_progress < 1)
        {
            _progress += Time.deltaTime / _producingDuration;
        }
        else
        {
            _progress = 0;
            _resourceReceiver.DeleteResources();
            ResourceCount++;
        }
    }
}
