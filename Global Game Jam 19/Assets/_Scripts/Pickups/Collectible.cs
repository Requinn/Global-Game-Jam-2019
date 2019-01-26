using MichaelWolfGames.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MichaelWolfGames.DamageSystem;
using System;

/// <summary>
/// Generic Collectable Object
/// </summary>
public class Collectible : PickupBase
{
    protected override void DoOnPickedUp(HealthManagerBase healthManager) {
        Debug.Log("collectable acquired");
    }
}
