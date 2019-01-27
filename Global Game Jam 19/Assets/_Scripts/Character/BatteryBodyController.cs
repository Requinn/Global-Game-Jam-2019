using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MichaelWolfGames.DamageSystem;
using MichaelWolfGames;

public class BatteryBodyController : SubscriberBase<HealthManager>
{
    [SerializeField] private SpriteRenderer[] bodySprites;
    [SerializeField] private SpriteRenderer smileFace;
    [SerializeField] private SpriteRenderer sadFace;
    [SerializeField] private SpriteRenderer surprisedFace;

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
        if (charge <= 1)
            SetFace_Sad();
        else if (charge > 1)
            SetFace_Smile();
    }


    public void SetFace_Sad()
    {
        smileFace.enabled = false;
        surprisedFace.enabled = false;
        sadFace.enabled = true;
    }

    public void SetFace_Smile()
    {
        smileFace.enabled = true;
        surprisedFace.enabled = false;
        sadFace.enabled = false;
    }
    public void SetFace_Surprised()
    {
        smileFace.enabled = false;
        surprisedFace.enabled = true;
        sadFace.enabled = false;
    }
}
