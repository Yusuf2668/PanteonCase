using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ISelector
{
    public GameObject CreateRayOnScreen(RaycastHit2D raycastHit2D, LayerMask mask);
}
