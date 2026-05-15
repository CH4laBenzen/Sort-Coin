using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Coin : MonoBehaviour
{
    public string coinValue;

    public void MoveToTarget(Vector3 targetPosition)
    {
        transform.DOJump(targetPosition, 2.0f, 1, 0.5f).SetEase(Ease.OutQuad);
        //transform.DORotate(new Vector3(0, 360, 0), 0.5f, RotateMode.FastBeyond360);
    }
}
