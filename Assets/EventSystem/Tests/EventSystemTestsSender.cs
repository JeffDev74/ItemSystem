using System.Collections;
using UnityEngine;

namespace FPS.EventSystem
{
	public class EventSystemTestsSender : MonoBehaviour
	{
        private void Start()
        {
            StartCoroutine(FireEvent());
        }

        private int counter = 0;
        IEnumerator FireEvent()
        {
            while(true)
            {
                counter++;
                EventMessenger.Instance.Raise(new EventTest("itemid_" + counter));
                yield return new WaitForSeconds(1);
            }
        }
    }
}