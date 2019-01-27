using MichaelWolfGames.DamageSystem;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public int damage = 1;

    public void OnTriggerEnter2D(Collider2D c) {
        if (c.CompareTag("Player")) {
            c.GetComponent<Character>().Reset();
        }
    }
}
