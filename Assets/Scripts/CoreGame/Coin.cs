using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Coin : MonoBehaviour
{
    public string coinValue;
    private Vector3 originalLocalPos;

    public void HoverUp(){
        originalLocalPos = transform.localPosition;
        transform.DOLocalMoveY(originalLocalPos.y + 0.5f, 0.2f).SetEase(Ease.OutQuad);
    }

    public void HoverDown(){
        transform.DOLocalMove(originalLocalPos, 0.2f).SetEase(Ease.OutQuad);
    }

    public void MoveToTarget(Vector3 targetPosition)
    {
        transform.DOJump(targetPosition, 1.5f, 1, 0.4f).SetEase(Ease.OutQuad).OnComplete(() => {
            transform.DOPunchScale(new Vector3(0.1f, -0.1f, 0.1f), 0.15f, 5, 0.5f);
        });
    }
    public void SpawnAtPosition(Vector3 targetPos)
    {
    transform.position = targetPos;
    transform.localScale = Vector3.zero;
    transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
    }
}
