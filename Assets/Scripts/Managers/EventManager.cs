using System;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Collections;

public class EventManager : MonoSingleton<EventManager>
{
    public event Action<GameObject> onSelectedItem;
    public event Action<bool> canDropItem;

    public event Action buildingDropped;
    public event Action onLeftMouseClick;
    public event Action onRightMouseClick;

    public void LeftMouseClick() { onLeftMouseClick?.Invoke(); }
    public void RightMouseClick() { onRightMouseClick?.Invoke(); }


    public void SelectedBuilding(GameObject _gameObject)
    {
        if (onSelectedItem != null)
        {
            onSelectedItem(_gameObject);
        }
    }
    public void CanDropItem(bool value)
    {
        if (canDropItem != null)
        {
            canDropItem(value);
        }
    }

    public void BuildingDropped() { buildingDropped?.Invoke(); }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LeftMouseClick();
        }
        if (Input.GetMouseButtonDown(1))
        {
            RightMouseClick();
        }
    }



}
