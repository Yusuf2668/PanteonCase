using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionMenuController : MonoBehaviour
{
    [SerializeField] Transform productionsParent;

    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            FactoryManager.Instance.CreateNewObject("UIBarrackItem", productionsParent);
            FactoryManager.Instance.CreateNewObject("UIPowerPlantItem", productionsParent);
        }
    }
}
