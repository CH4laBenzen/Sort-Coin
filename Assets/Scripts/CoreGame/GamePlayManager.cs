using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : Singleton<GamePlayManager>
{
    [Header("References")]
    public CoinDock choosenDock;
    public CoinDock targetDock;
    [Header("Prefabs")]
    public GameObject[] coinPrefabs;

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
                    int countCoin = dock.FetchTotalCoin();
                    if(choosenDock.CoinInDock.Count > 0)
                    {
                        Debug.Log("Choosen Dock: " + countCoin + " coins of value " + choosenDock.CoinInDock[choosenDock.CoinInDock.Count - 1].coinValue);
                    }
                }
                else if(choosenDock != null)
                {
                    if(dock != choosenDock)
                    {
                        targetDock = dock;
                        //MoveCoin(choosenDock, targetDock);
                        MoveIndexCoin(choosenDock, targetDock);
                    }
                    else
                    {
                        ResetTarget();
                    }
                }
            }
            else
            {
                choosenDock = null;
                targetDock = null;
            }
        }
    }

    // public void MoveAllCoin(CoinDock fromDock, CoinDock toDock)
    // {
    //     if (fromDock.CoinInDock.Count > 0 && toDock.CoinInDock.Count < toDock.MaxStack)
    //     {
    //         List<Coin> coinsToMove = new List<Coin>(fromDock.CoinInDock);
    //         fromDock.CoinInDock.Clear();
    //         toDock.CoinInDock.AddRange(coinsToMove);
    //         for (int i = coinsToMove.Count - 1; i >= 0; i--)
    //         {
    //             coinsToMove[i].MoveToTarget(toDock.SlotPositions[toDock.CoinInDock.Count - 1 - i].position);
    //         }
    //     }
    //     ResetTarget();
    // }
    
    // public void MoveCoin(CoinDock fromDock, CoinDock toDock)
    // {
    //     if (fromDock.CoinInDock.Count > 0 && toDock.CoinInDock.Count < toDock.MaxStack)
    //     {
    //         Coin coinToMove = fromDock.CoinInDock[fromDock.CoinInDock.Count - 1];
    //         fromDock.CoinInDock.Remove(coinToMove);
    //         toDock.CoinInDock.Add(coinToMove);
    //         coinToMove.MoveToTarget(toDock.SlotPositions[toDock.CoinInDock.Count - 1].position);
    //     }
    //     ResetTarget();
    // }

    public void MoveIndexCoin(CoinDock fromDock, CoinDock toDock)
    {
        if(fromDock.CoinInDock.Count == 0 || toDock.CoinInDock.Count >= toDock.MaxStack)
        {
            ResetTarget();
            return;
        }
        bool canMove = false;
        if(toDock.CoinInDock.Count == 0)
        {
            canMove = true;
        }
        else
        {
            string fromCoinValue = fromDock.CoinInDock[fromDock.CoinInDock.Count - 1].coinValue;
            string toCoinValue = toDock.CoinInDock[toDock.CoinInDock.Count - 1].coinValue;
            if(fromCoinValue == toCoinValue)
            {
                canMove = true;
            }
            else
            {
                ResetTarget();
                return;
            }
        }
        if(canMove)
        {
            // int count = fromDock.FetchTotalCoin();
            // float availableSpace = toDock.MaxStack - toDock.CoinInDock.Count;
            // if(count > availableSpace)
            // {
            //     count = (int)availableSpace;
            // }
            // List<Coin> coinsToMove = fromDock.CoinInDock.GetRange(fromDock.CoinInDock.Count - count, count);
            // fromDock.CoinInDock.RemoveRange(fromDock.CoinInDock.Count - count, count);
            // toDock.CoinInDock.AddRange(coinsToMove);
            // for (int i = coinsToMove.Count - 1; i >= 0; i--)
            // {
            //     coinsToMove[i].MoveToTarget(toDock.SlotPositions[toDock.CoinInDock.Count - 1 - i].position);
            // }
            // ResetTarget();

            int count = fromDock.FetchTotalCoin();
            int currentInTarget = toDock.CoinInDock.Count;
            float availableSpace = toDock.MaxStack - currentInTarget;
            if (count > availableSpace) 
            {
                count = (int)availableSpace;
            }
            if (count > 0)
            {
                int startIndex = fromDock.CoinInDock.Count - count;
                List<Coin> coinsToMove = fromDock.CoinInDock.GetRange(startIndex, count);
                fromDock.CoinInDock.RemoveRange(startIndex, count);
                toDock.CoinInDock.AddRange(coinsToMove);
                for (int i = 0; i < coinsToMove.Count; i++)
                {
                    int targetSlotIndex = currentInTarget + i;
                    if (targetSlotIndex < toDock.SlotPositions.Length)
                    {
                        coinsToMove[i].MoveToTarget(toDock.SlotPositions[targetSlotIndex].position);
                    }
                    if(toDock.CoinInDock.Count >= toDock.MaxStack)
                    {
                        CheckCoinToMerge();
                    }
                }
            }
            ResetTarget();
        }
    }

    public void CheckCoinToMerge()
    {
        string lastValue = targetDock.CoinInDock[targetDock.CoinInDock.Count - 1].coinValue;
        bool allSame = true;
        foreach(Coin coin in targetDock.CoinInDock)
        {
            if(coin.coinValue != lastValue)
            {
                allSame = false;
                break;
            }
        }
        if(allSame)
        {
            Debug.Log("Merge Coin with value: " + lastValue);

            int currentValue = int.Parse(lastValue);
            int nextValue = currentValue + 1;
            foreach(Coin coin in targetDock.CoinInDock)
            {
                Destroy(coin.gameObject);
            }
            targetDock.CoinInDock.Clear();
            if(nextValue < coinPrefabs.Length)
            {
                for(int i = 0; i < 2; i++)
                {
                    GameObject newCoinObj = Instantiate(coinPrefabs[nextValue], targetDock.SlotPositions[0].position, Quaternion.identity);
                    Coin newCoin = newCoinObj.GetComponent<Coin>();
                    newCoin.coinValue = nextValue.ToString();
                    targetDock.CoinInDock.Add(newCoin);
                    newCoin.MoveToTarget(targetDock.SlotPositions[i].position);
                }
            }
        }
    }

    private void ResetTarget()
    {
        this.choosenDock = null;
        this.targetDock = null;
    }
}