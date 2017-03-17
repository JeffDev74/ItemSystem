﻿using System;
using FPS.EventSystem;
using FPS.InventorySystem.Events;
using UnityEngine;

namespace FPS
{
	public class TestInventoryEvents : MonoBehaviour
	{
        private void OnEnable()
        {
            EventMessenger.Instance.AddListner<EventItemWasAddedToInventory>(OnItemWasAddedEvent);
            EventMessenger.Instance.AddListner<EventItemWasRemovedToInventory>(OnItemWasRemovedEvent);
            EventMessenger.Instance.AddListner<EventItemWasUpdatedOnInventory>(OnItemWasUpdatedEvent); 
        }

        private void OnDisable()
        {
            EventMessenger.Instance.RemoveListner<EventItemWasAddedToInventory>(OnItemWasAddedEvent);
            EventMessenger.Instance.RemoveListner<EventItemWasRemovedToInventory>(OnItemWasRemovedEvent);
            EventMessenger.Instance.RemoveListner<EventItemWasUpdatedOnInventory>(OnItemWasUpdatedEvent);
        }

        private void OnItemWasAddedEvent(EventItemWasAddedToInventory e)
        {
            //Debug.Log("The item ["+e.Item.BaseData.Name+"] was added from inventory ["+e.InventoryUUID+"]");
        }

        private void OnItemWasRemovedEvent(EventItemWasRemovedToInventory e)
        {
            //Debug.Log("The item [" + e.Item.BaseData.Name + "] was removed from inventory [" + e.InventoryUUID + "]");
        }

        private void OnItemWasUpdatedEvent(EventItemWasUpdatedOnInventory e)
        {
            //Debug.Log("The item [" + e.Item.BaseData.Name + "] was updated from inventory [" + e.InventoryUUID + "]");
        }

        
    }
}