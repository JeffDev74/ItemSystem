using ItemSystem;
using UnityEngine;

namespace FPS
{
	public class SODatabaseManager : MonoBehaviour
	{
        public static SODatabaseManager Instance;

        public AmmoDBModel AmmoSODBModel;
        public ResourceDBModel ResourceSODBModel;
        public WeaponDBModel WeaponSODBModel;

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if(Instance != this)
            {
                Destroy(gameObject);
            }

            AmmoSODBModel = AmmoDBModel.LoadDb();
            ResourceSODBModel = ResourceDBModel.LoadDb();
            WeaponSODBModel = WeaponDBModel.LoadDb();
        }
    }
}