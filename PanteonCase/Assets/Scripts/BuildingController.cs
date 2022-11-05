using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    private const string _cellTag = "Cell";
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(_cellTag))
        {
            return;
        }
        EventManager.Instance.CanDropItem(false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(_cellTag))
        {
            return;
        }
        EventManager.Instance.CanDropItem(true);
    }
}
