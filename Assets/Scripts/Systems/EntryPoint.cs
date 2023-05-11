using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : EntryLeaf
{
    [SerializeReference] protected List<IHaveToBeCalledBeforeTheStart> _startComponents;

    private void Awake()
    {
        if (_startComponents.Count != 0)
        {
            foreach (IHaveToBeCalledBeforeTheStart component in _startComponents)
            {
                component.Call();
            }
        }

        AwakeComponent();
    }

    private void Start()
    {
        StartComponent();
    }

    private void Update()
    {
        UpdateComponent();
    }

    private void OnEnable()
    {
        EnableComponent();
    }

    private void OnDisable()
    {
        DisableComponent();
    }
}
