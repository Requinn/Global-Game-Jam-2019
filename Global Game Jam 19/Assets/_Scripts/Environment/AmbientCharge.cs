using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MichaelWolfGames.DamageSystem;

public class AmbientCharge : MonoBehaviour
{
    public HealthManager chargeManager;
    private Coroutine addChargeCoroutine = null;
    public float rechargeDelay = 1.0f;

    public AudioSource audioSource;
    public AudioClip chargeSound;

    void Start()
    {
        if(chargeManager == null)
        {
            chargeManager = FindObjectOfType<HealthManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(addChargeCoroutine == null && chargeManager.CurrentValue < chargeManager.MaxValue)
        {
            addChargeCoroutine = StartCoroutine(CoAddCharge(rechargeDelay));
        }
    }

    IEnumerator CoAddCharge(float delay)
    {
        while (chargeManager.CurrentValue < chargeManager.MaxValue && this.isActiveAndEnabled)
        {
            yield return new WaitForSeconds(delay);
            chargeManager.AddHealth(1f);
            audioSource.PlayOneShot(chargeSound);
        }
        addChargeCoroutine = null;
    }
}
