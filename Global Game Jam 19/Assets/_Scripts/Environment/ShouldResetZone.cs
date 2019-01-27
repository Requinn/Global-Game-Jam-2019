using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// if the player is in this collider then tell them to reset
/// </summary>
public class ShouldResetZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D c) {
        Character player = c.GetComponent<Character>();
        if (player) {
            player.SetResetState(true);
        }
    }

    void OnTriggerExit2D(Collider2D c) {
        Character player = c.GetComponent<Character>();
        if (player) {
            player.SetResetState(false);
        }
    }
}
