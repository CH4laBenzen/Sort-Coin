using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public string coinValue;

    public void MoveToTarget(Vector3 targetPosition)
    {
        transform.position = targetPosition;
    }
}
