using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : Singleton<GamePlayManager>
{
    public CoinDock choosenDock;
    public CoinDock targetDock;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FindTargetDock();
        }
    }

    public void FindTargetDock()
    {
        Ray mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(mousePos, out hit))
        {
            CoinDock dock = hit.collider.GetComponent<CoinDock>();
            if (dock != null)
            {
                if (choosenDock == null)
                {
                    choosenDock = dock;
                }
                else if(choosenDock != null)
                {
                    targetDock = dock;
                    //MoveCoin(choosenDock, targetDock);
                    MoveAllCoin(choosenDock, targetDock);
                }
            }
            else
            {
                choosenDock = null;
                targetDock = null;
            }
        }
    }

    public void MoveAllCoin(CoinDock fromDock, CoinDock toDock)
    {
        if (fromDock.CoinInDock.Count > 0 && toDock.CoinInDock.Count < toDock.MaxStack)
        {
            List<Coin> coinsToMove = new List<Coin>(fromDock.CoinInDock);
            fromDock.CoinInDock.Clear();
            toDock.CoinInDock.AddRange(coinsToMove);
            for (int i = coinsToMove.Count - 1; i >= 0; i--)
            {
                coinsToMove[i].MoveToTarget(toDock.SlotPositions[toDock.CoinInDock.Count - 1 - i].position);
            }
        }
        ResetTarget();
    }

    public void MoveCoin(CoinDock fromDock, CoinDock toDock)
    {
        if (fromDock.CoinInDock.Count > 0 && toDock.CoinInDock.Count < toDock.MaxStack)
        {
            Coin coinToMove = fromDock.CoinInDock[fromDock.CoinInDock.Count - 1];
            fromDock.CoinInDock.Remove(coinToMove);
            toDock.CoinInDock.Add(coinToMove);
            coinToMove.MoveToTarget(toDock.SlotPositions[toDock.CoinInDock.Count - 1].position);
        }
        ResetTarget();
    }

    private void ResetTarget()
    {
        this.choosenDock = null;
        this.targetDock = null;
    }
}