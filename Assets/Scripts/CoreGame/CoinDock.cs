using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDock : MonoBehaviour
{
    [SerializeField] private float CoinStack = 9f;
    [SerializeField] private string coinvalue;

    private float count;

    public List<Coin> coins = new List<Coin>();
    public Transform[] SlotPositions;

    public void AddCoin(Coin coin)
    {
        if (coins.Count >= CoinStack)
        {
            return;
        }
        else if(count > (CoinStack - coins.Count))
        {
            return;
        }
        else
        {
            coins.Add(coin);
            coin.MoveToTarget(SlotPositions[coins.Count - 1].position);
        }
    }

    public void ExchangeCoin(Coin topcoin)
    {
        count = 1;
        coinvalue = topcoin.coinValue;
        for(int i = coins.Count - 1; i > 0; i++)
        {
            if (coins[i].coinValue == coinvalue)
            {
                count++;
                AddCoin(coins[i]);
            }
            else
            {
                break;
            }
        }
    }


}
