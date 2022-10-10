using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Cell"))
        {
            return;
        }
        EventManager.Instance.CanDropItem(false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Cell"))
        {
            return;
        }
        EventManager.Instance.CanDropItem(true);
    }
}
