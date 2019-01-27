using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Reactor explodes, while player runs out of the room, fade sequence to white.
/// </summary>
public class ReactorExplosionSequence : SequenceObject
{
    [SerializeField]
    private CanvasGroup _customWhiteFadeCanvas;
    [SerializeField]
    private Transform _exitPosition;
    [SerializeField]
    private SceneLoader _sceneloader;

    [Header("Explosions")]
    private Explosion[] _explosions;

    public override IEnumerator DoSequenceAction() {
        //wait
        yield return new WaitForSeconds(1.5f);
        //StartCoroutine(DoExplosions());
        yield return new WaitForSeconds(0.33f);
        //perform a jump
        _affectingPlayer.DoMovements(0, true);
        //wait to land
        yield return new WaitForSeconds(1f);
        //start fading
        StartCoroutine(DoFade());
        float time = 0;
        while(time < 2f) {
            time += Time.deltaTime;
            _affectingPlayer.DoMovements(-1, false);
            yield return null;
        }
        _sceneloader.LoadScene(2, false);
        yield return null;
    }

    /// <summary>
    /// Perform some explosions n shit
    /// </summary>
    /// <returns></returns>
    private IEnumerator DoExplosions() {
        float time = 0;
        while (time < 15f) {
            time += Time.deltaTime;
            foreach(Explosion e in _explosions) {
                if (e.DoExplosion()) {
                    break;
                }
            }
            yield return new WaitForSeconds(0.33f);
        }
    }


    /// <summary>
    /// Perform the fade over time
    /// </summary>
    /// <returns></returns>
    private IEnumerator DoFade() {
        float timer = 0;
        float alphaValue = 0;

        _customWhiteFadeCanvas.gameObject.SetActive(true);
        while (timer < 2f) {
            timer += Time.deltaTime;
            alphaValue = Mathf.Lerp(0, 1, timer / 2f);
            _customWhiteFadeCanvas.alpha = alphaValue;
            yield return null;
        }
    }

}
