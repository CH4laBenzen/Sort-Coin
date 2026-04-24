using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class CoinDock : MonoBehaviour
{
    [SerializeField] private float MaxStack = 9f;
    [SerializeField] private float currentStack = 0f;

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
        CheckCurrentState();
        MovecointoDock();
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

    private void MovecointoDock()
    {
        for(int i = 0; i <= CoinInDock.Count - 1; i++)
        {
            CoinInDock[i].MoveToTarget(SlotPositions[i].position);
        }
    }

    public void Addcoin(CoinDock targetDock, Coin coin)
    {
        targetDock.CoinInDock.Add(coin);
        coin.MoveToTarget(targetDock.SlotPositions[targetDock.CoinInDock.Count - 1].position);
    }
    
    public void Removecoin(CoinDock choosenDock, Coin coin)
    {
        choosenDock.CoinInDock.Remove(coin);
    }

    public Coin TopCoin()
    {
        if (CoinInDock.Count > 0)
        {
            return CoinInDock[CoinInDock.Count - 1];
        }
        else
        {
            return null;
        }
    }

    public int GetCoinCount()
    {
        int countcoin = 0;
        for (int i = CoinInDock.Count - 1; i > 0; i--)
        {
            if (CoinInDock[i].coinValue == CoinInDock[i - 1].coinValue)
            {
                countcoin += 1;
            }
        }
        return countcoin;
    }
}
