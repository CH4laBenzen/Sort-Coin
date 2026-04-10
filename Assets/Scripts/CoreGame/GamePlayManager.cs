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
                else
                {
                    targetDock = dock;
                    MoveCoin(choosenDock, targetDock);
                }
            }
            else
            {
                choosenDock = null;
                targetDock = null;
            }
        }
    }

    public void MoveCoin(CoinDock choosenDock, CoinDock targetDock)
    {
        List<Coin> currentdock = choosenDock.CoinInDock;

        targetDock.CoinInDock.AddRange(currentdock);
        targetDock.CoinInDock[targetDock.CoinInDock.Count - 1].MoveToTarget(targetDock.transform.position);
        choosenDock.CoinInDock.RemoveRange(0, 1);
        this.choosenDock = null;
        this.targetDock = null;
    }
}