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
                if (choosenDock == null && dock.CoinInDock.Count > 0)
                {
                    choosenDock = dock;
                }
                else if(choosenDock != null)
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
        targetDock.Addcoin(targetDock, choosenDock.TopCoin());
        choosenDock.Removecoin(choosenDock, choosenDock.TopCoin());
        ResetTarget();
    }

    private void ResetTarget()
    {
        this.choosenDock = null;
        this.targetDock = null;
    }
}