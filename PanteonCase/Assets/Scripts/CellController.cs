using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellController : MonoBehaviour
{
    public bool IsEmpty
    {
        get
        {
            return _isEmpty;
        }
    }

    [SerializeField] private bool _isEmpty = true;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (ItemSelectorManager.Instance.SelectedItem)
        {
            return;
        }
        if (!collision.CompareTag("Cell"))
        {
            _isEmpty = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ItemSelectorManager.Instance.SelectedItem)
        {
            return;
        }
        if (!collision.CompareTag("Cell"))
        {
            _isEmpty = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (ItemSelectorManager.Instance.SelectedItem)
        {
            return;
        }
        if (!collision.CompareTag("Cell"))
        {
            _isEmpty = true;
        }
    }
}
