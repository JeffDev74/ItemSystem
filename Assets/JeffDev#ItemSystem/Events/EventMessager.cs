using System;
using System.Collections.Generic;
using UnityEngine;

namespace FPS.EventSystem
{
    public class EventMessenger
    {
        private bool isDebug = false;

        private static EventMessenger _instance;
        public static EventMessenger Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EventMessenger();
                }
                return _instance;
            }
        }

        public delegate void EventDelegate<T>(T e) where T : GameEvent;

        readonly Dictionary<Type, Delegate> _delegates = new Dictionary<Type, Delegate>();

        public void AddListner<T>(EventDelegate<T> listner) where T : GameEvent
        {
            Delegate d;
            if (_delegates.TryGetValue(typeof(T), out d))
            {
                _delegates[typeof(T)] = Delegate.Combine(d, listner);
            }
            else
            {
                _delegates[typeof(T)] = listner;
            }
        }

        public void RemoveListner<T>(EventDelegate<T> listner) where T : GameEvent
        {
            Delegate d;
            if (_delegates.TryGetValue(typeof(T), out d))
            {
                Delegate currentDel = Delegate.Remove(d, listner);
                if (currentDel == null)
                {
                    _delegates.Remove(typeof(T));
                }
                else
                {
                    _delegates[typeof(T)] = currentDel;
                }
            }
        }

        public void Raise<T>(T e) where T : GameEvent
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }

            if (isDebug)
            {
                Debug.Log("<color=#ff00ffff>EVENT</color> <color=#ffa500ff>[" + e.GetType().Name + "]</color> Searching for listners...");
                //Debug.Log(System.Environment.StackTrace);
            }

            Delegate d;
            if (_delegates.TryGetValue(typeof(T), out d))
            {
                EventDelegate<T> callback = d as EventDelegate<T>;
                if (callback != null)
                {
                    callback(e);
                    if (isDebug)
                    {
                        Debug.Log("<color=#ff00ffff>EVENT</color> <color=#ffa500ff>[" + e.GetType().Name + "]</color> RISED.");
                    }
                }
            }
        }
    }
}