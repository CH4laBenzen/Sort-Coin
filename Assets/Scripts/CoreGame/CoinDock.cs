using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDock : MonoBehaviour
{
    [SerializeField] private float maxSlots = 10f;
    public List<Coin> coinStack = new List<Coin>();
    public GameObject[] SlotPositions;

    public void MoveToDock(Coin coin)
    {
        if(coinStack.Count <= maxSlots)
        {
            coinStack.Add(coin);
            coin.MoveToTarget(SlotPositions[coinStack.Count].transform.position);
        }
        else
        {
            return;
        }
    }

    public void OnMouseDown()
    {
        Debug.Log("Da click vao" + gameObject.name);
    }

    public void ClearDock()
    {
        for (int i = 0; i < coinStack.Count; i++)
        {
            Destroy(coinStack[i].gameObject);
        }
        coinStack.Clear();
    }

}
