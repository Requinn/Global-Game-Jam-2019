using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private bool _isExploding = false;
    [SerializeField]
    private GameObject _explosionImage;

    public bool DoExplosion() {
        if (!_isExploding) {
            StartCoroutine(DelayedShutOff());
            return true;
        }
        return false;
    }

    private IEnumerator DelayedShutOff() {
        _isExploding = true;
        _explosionImage.SetActive(true);
        _explosionImage.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.75f);
        _explosionImage.SetActive(false);
        _isExploding = false;
    }
}
