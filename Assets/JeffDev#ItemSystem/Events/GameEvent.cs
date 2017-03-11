using System;
using System.Collections.Generic;
using UnityEngine;

namespace FPS.EventSystem
{
    public class GameEvent
    {
        private Dictionary<string, string> messages = new Dictionary<string, string>();

        public string GetCustomMessage(string key)
        {
            string msg = "";
            if (messages.TryGetValue(key, out msg))
            {
                return msg;
            }
            return "Message not found for key [" + key + "]";
        }
    }
}