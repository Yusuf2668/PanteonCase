using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StringTypeDeneme : MonoSingleton<StringTypeDeneme>
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(nameof(Deneme.MyEnum.PowerPlant)))
        {
            Debug.Log("Girdi");
        }
    }
}
