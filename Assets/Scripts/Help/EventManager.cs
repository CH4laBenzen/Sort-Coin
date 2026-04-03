using MainGame;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class GameEvent { }
namespace MainGame
{
    public static class EventManager
    {
        static readonly Dictionary<Type, Action<GameEvent>> s_Events = new Dictionary<Type, Action<GameEvent>>();

        static readonly Dictionary<Delegate, Action<GameEvent>> s_EventLookups =
            new Dictionary<Delegate, Action<GameEvent>>();

        public static void AddListener<T>(Action<T> evt) where T : GameEvent
        {
            if (!s_EventLookups.ContainsKey(evt))
            {
                Action<GameEvent> newAction = (e) => evt((T)e);
                s_EventLookups[evt] = newAction;
                if (s_Events.TryGetValue(typeof(T), out Action<GameEvent> internalAction))
                    s_Events[typeof(T)] = internalAction += newAction;
                else
                    s_Events[typeof(T)] = newAction;
            }
        }

        public static void RemoveListener<T>(Action<T> evt) where T : GameEvent
        {
            if (s_EventLookups.TryGetValue(evt, out var action))
            {
                if (s_Events.TryGetValue(typeof(T), out var tempAction))
                {
                    tempAction -= action;
                    if (tempAction == null)
                        s_Events.Remove(typeof(T));
                    else
                        s_Events[typeof(T)] = tempAction;
                }

                s_EventLookups.Remove(evt);
            }
        }

        public static void Trigger(GameEvent evt)
        {
            if (s_Events.TryGetValue(evt.GetType(), out var action))
                action.Invoke(evt);
        }

        public static void Clear()
        {
            s_Events.Clear();
            s_EventLookups.Clear();
        }
    }

    public class OnCoinAdd : GameEvent
    {
        public int CoinAmount;
    }

    public class OnCoinPay : GameEvent
    {
        public int PayAmount;
    }
    public class OnNotice : GameEvent
    {
        public string Content;
    }
    public class OnStarLevel : GameEvent { }


    public class OnCompleteLevel : GameEvent
    {
        public int CustomerServedAmount;
    }

    public class OnLevelClose : GameEvent
    {
        public int CustomerServedAmount;
    }

    public class OnWatchAds : GameEvent { }
    public class SendInterData : GameEvent
    {
        public string interPlacement;
        public string levelInter;
    }

    public class SendRewardData : GameEvent
    {
        public string levelReward;
        public string button_name;
        public string reward_name;
        public string reward_type;
        public int value;
        public string rewardPlacement;
    }

    public class OnUseBooster : GameEvent { }
    public class OnCompleteLuckSpin : GameEvent { }

    public class OnIAPPurchase : GameEvent
    {
        public Product Product;
        public int Level;
        public string Pack_id;
        public string Pack_name;
        public string Placement;
    }

    public class OnBagClick : GameEvent { }

    public class OnCustomerServed : GameEvent { }

    public class OnConveyorDone : GameEvent { }

    public class OnTrayOpened : GameEvent
    {
        public WaitingTray OpenedTray; // Khay vừa mở
        public Vector3 Position;
    }

    public class OnPackVipPurchase : GameEvent { }
    public class OnPackVipUnlock : GameEvent { }


    public class OnBagBoxDone : GameEvent
    {
        public BagBox BagBox;
    }
}