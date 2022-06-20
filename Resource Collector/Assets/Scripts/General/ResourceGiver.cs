using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IGiver))]
public class ResourceGiver : MonoBehaviour
{
    [SerializeField] float _giveResourceTime;

    private IEnumerable<ResourceType> _resourceTypes;
    private IGiver _giver;
    private bool _isGiving;
    private IReceiver _receiver;
    private Action _onGive;

    private void Start()
    {
        _giver = GetComponent<IGiver>();
    }

    public void StartGiving(IReceiver receiver, IEnumerable<ResourceType> resourceTypes , Action onGive)
    {
        _receiver = receiver;
        _resourceTypes = resourceTypes;
        _onGive = onGive;
        _isGiving = true;

        TryGiveResource();
    }

    public void StopGiving()
    {
        _isGiving = false;
    }

    private void TryGiveResource()
    {
        if (_isGiving == false) return;

        if (_giver.ResourceCount <= 0)
        {
            StartCoroutine(WaitBeforeTryGiveResource(_giveResourceTime));
            return;
        }

        StartCoroutine(GiveResourceAfter(_giveResourceTime));
    }

    private IEnumerator GiveResourceAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        if (_receiver.TryReceiveResources(_resourceTypes))
        {
            _onGive.Invoke();
        }
        TryGiveResource();
    }

    private IEnumerator WaitBeforeTryGiveResource(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        TryGiveResource();
    }
}