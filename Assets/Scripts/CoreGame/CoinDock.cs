using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class CoinDock : MonoBehaviour
{
    [SerializeField] private float CoinStack = 9f;

    public List<Coin> CoinInDock = new List<Coin>();
    public Transform[] SlotPositions;

    public enum DockState
    {
        empty,
        stillhasSpace,
        full,
    }

    public DockState currentState;

    private void Start()
    {
        if(CoinInDock.Count == 0)
        {
            currentState = DockState.empty;
        }
        else if(CoinInDock.Count == CoinStack)
        {
            currentState = DockState.full;
        }
        else
        {
            currentState = DockState.stillhasSpace;
        }
        Movecoin();
    }

    //private void CheckToAddCoin(CoinDock dock)
    //{
    //    if(dock.currentState == DockState.empty)
    //    {
    //        dock.CoinInDock.Add(CoinInDock[CoinInDock.Count - 1]);
    //        CoinInDock[CoinInDock.Count - 1].MoveToTarget(SlotPositions[0].position);
    //        dock.currentState = DockState.stillhasSpace;
    //    }
    //}

    private void Movecoin()
    {
        for(int i = 0; i <= CoinInDock.Count - 1; i++)
        {
            CoinInDock[i].MoveToTarget(SlotPositions[i].position);
        }
    }

    private int TopCoin()
    {

        return 0;
    }
    
}
