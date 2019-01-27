using MichaelWolfGames.DamageSystem;
using UnityEngine;

namespace MichaelWolfGames.Examples
{
    /// <summary>
    /// Simple Health Pickup
    /// 
    /// Michael Wolf
    /// February, 2018
    /// </summary>
    public class HealthPickup : PickupBase
    {
        public float HealthValue = 25f;

        public AudioSource audioSource;
        public AudioClip chargeSound;

        protected override void DoOnPickedUp(HealthManagerBase healthManager)
        {
            healthManager.AddHealth(HealthValue);
            if(audioSource)
                audioSource.PlayOneShot(chargeSound);
        }

        protected override bool CheckPickUpConditions(HealthManagerBase healthManager)
        {
            return true;
        }
    }
}