using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class CoinDock : MonoBehaviour
{
    public float MaxStack = 9f;
    public float currentStack = 0f;

    public List<Coin> CoinInDock = new List<Coin>();
    public Transform[] SlotPositions;
    
    public DockState currentState;

    public enum DockState
    {
        empty,
        stillhasSpace,
        full,
    }

    private void Start()
    {
        MoveCoinToDock();
        CheckCurrentState();
    }

    private void CheckCurrentState()
    {
        if (CoinInDock.Count == 0)
        {
            currentState = DockState.empty;
        }
        else if (CoinInDock.Count == MaxStack)
        {
            currentState = DockState.full;
        }
        else
        {
            currentState = DockState.stillhasSpace;
        }
    }

    public void MoveCoinToDock()
    {
        for(int i = 0; i < CoinInDock.Count; i++)
        {
            CoinInDock[i].MoveToTarget(SlotPositions[i].position);
        }
    }

    public int FetchTotalCoin()
    {
        if(CoinInDock.Count == 0)
        {
            return 0;
        }
        string lastValue = CoinInDock[CoinInDock.Count - 1].coinValue; 
        int countCoin = 0;
        for(int i = CoinInDock.Count - 1; i >= 0; i--)
        {
            if(CoinInDock[i].coinValue == lastValue)
            {
                countCoin++;
            }
            else{
                break;
            }
        }
        return countCoin;
    }
}