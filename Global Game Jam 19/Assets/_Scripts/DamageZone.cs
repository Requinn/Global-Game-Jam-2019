using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public int damage = 1;

    public void OnTriggerStay(Collider c) {
        if (c.CompareTag("Player")) {
            c.GetComponent<Character>().ApplyDamage(this, damage, c.transform.position);
        }
    }
}
