using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformParenting : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.transform.parent = transform;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        other.gameObject.transform.parent = null;
    }
}
