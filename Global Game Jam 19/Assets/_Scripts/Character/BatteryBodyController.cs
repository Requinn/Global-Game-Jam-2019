using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MichaelWolfGames.DamageSystem;
using MichaelWolfGames;

public class BatteryBodyController : SubscriberBase<HealthManager>
{
    [SerializeField] private SpriteRenderer[] bodySprites;

    protected override void DoOnInitialize()
    {
        base.DoOnInitialize();
        if(enabled)
            DoOnUpdateHealth(SubscribableObject.CurrentHealth);
    }
    protected override void SubscribeEvents()
    {
        SubscribableObject.OnUpdateHealth += DoOnUpdateHealth;
    }

    protected override void UnsubscribeEvents()
    {
        SubscribableObject.OnUpdateHealth -= DoOnUpdateHealth;
    }

    private void DoOnUpdateHealth(float value)
    {
        int charge = (int)value;
        for (int i = 0; i < bodySprites.Length; i++)
        {
            bodySprites[i].enabled = (i == charge);
        }
    }

}
