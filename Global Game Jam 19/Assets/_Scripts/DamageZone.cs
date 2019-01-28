using MichaelWolfGames.DamageSystem;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public int damage = 1;

    public AudioSource audioSource;
    public AudioClip deathSound;

    public void OnTriggerEnter2D(Collider2D c) {
        if (c.CompareTag("Player")) {
            c.GetComponent<Character>().Reset();
            if (audioSource)
                audioSource.PlayOneShot(deathSound);
        }
    }
}
