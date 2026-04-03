using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private CoinDock selectedDock;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleSelection();
        }
    }

    void HandleSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            CoinDock clickedDock = hit.collider.GetComponent<CoinDock>();

            if (clickedDock != null)
            {
                if (selectedDock == null)
                {
                    if (clickedDock.coinStack.Count > 0)
                    {
                        selectedDock = clickedDock;
                        Debug.Log("Da chon Dock nguon");
                    }
                }
                else if (selectedDock == clickedDock)
                {
                    selectedDock = null;
                }
                else
                {
                    TryMoveCoins(selectedDock, clickedDock);
                    selectedDock = null;
                }
            }
        }
    }

    void TryMoveCoins(CoinDock source, CoinDock target)
    {
        if (source.coinStack.Count == 0) return;

        Coin topCoin = source.GetTopCoin();

        if (target.CanAddCoin(topCoin))
        {
            int valueToMove = topCoin.coinValue;
            List<Coin> coinsToMove = new List<Coin>();

            for (int i = source.coinStack.Count - 1; i >= 0; i--)
            {
                if (source.coinStack[i].coinValue == valueToMove && target.coinStack.Count + coinsToMove.Count < target.capacity)
                {
                    coinsToMove.Add(source.coinStack[i]);
                }
                else
                {
                    break;
                }
            }

            foreach (Coin c in coinsToMove)
            {
                source.RemoveTopCoin();
                target.AddCoin(c);
            }
        }
    }
}