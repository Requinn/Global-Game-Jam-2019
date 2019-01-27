using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformParenting : MonoBehaviour
{
    private MovingPlatform mp;

    void Awake()
    {
        mp = transform.parent.gameObject.GetComponent<MovingPlatform>();
        if (!mp.activateOnPlayerTouch)
            mp.Toggle();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.transform.parent = transform;
        if (!mp.activateOnPlayerTouch) return;
        if (other.gameObject.CompareTag("Player"))
            mp.Toggle();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        other.gameObject.transform.parent = null;
        if (!mp.activateOnPlayerTouch) return;
        if (other.gameObject.CompareTag("Player"))
            mp.Toggle();
    }
}
