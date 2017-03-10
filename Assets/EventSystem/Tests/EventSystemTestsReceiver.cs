using System;
using UnityEngine;

namespace FPS.EventSystem
{
	public class EventSystemTestsReceiver : MonoBehaviour
	{
        void OnEnable()
        {
            EventMessenger.Instance.AddListner<EventTest>(OnTestEvent);
        }

        void OnDisable()
        {
            EventMessenger.Instance.RemoveListner<EventTest>(OnTestEvent);
        }

        private void OnTestEvent(EventTest e)
        {
            Debug.Log("I Received the test event with [" + e.ItemUniqueUUID + "]");
            if(e.ItemUniqueUUID == "itemid_10")
            {
                Debug.LogWarning("Call code to drop an item...");
            }
        }
    }
}