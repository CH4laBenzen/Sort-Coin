using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDock : MonoBehaviour
{
    public int capacity;
    public List<Coin> coinStack = new List<Coin>();
    public Transform[] slotPositions;

    public Coin GetTopCoin()
    {
        if (coinStack.Count == 0) return null;
        return coinStack[coinStack.Count - 1];
    }

    public bool CanAddCoin(Coin newCoin)
    {
        if (coinStack.Count >= capacity) return false;
        if (coinStack.Count == 0) return true;

        return GetTopCoin().coinValue == newCoin.coinValue;
    }

    public void AddCoin(Coin coin)
    {
        coinStack.Add(coin);
        coin.transform.SetParent(this.transform);
        coin.MoveToTarget(slotPositions[coinStack.Count].position);
    }

    public Coin RemoveTopCoin()
    {
        if (coinStack.Count == 0) return null;
        Coin topCoin = GetTopCoin();
        coinStack.RemoveAt(coinStack.Count - 1);
        return topCoin;
    }
}
