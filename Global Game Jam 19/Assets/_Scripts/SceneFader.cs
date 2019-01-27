using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Fades the screen and blocks UI input on scene change
/// </summary>
public class SceneFader : MonoBehaviour {
    static SceneFader Instance;
    [SerializeField]
    private CanvasGroup _fadeCanvas;
    [SerializeField]
    private float _fadeDuration = 1.5f;
    private Coroutine _fadeRoutine;

    //Always fade on scene entry
    private void Awake() {
        FadeOut();
    }

    private void Start() {
        Instance = this;
        //LevelLoader LL = FindObjectOfType<LevelLoader>();
        //LL.OnStartLoad += FadeIn;
        //LL.OnEndLoad += FadeOut;
    }

    /// <summary>
    /// Fade the canvas in
    /// </summary>
    public void FadeIn(Action fadeCompleteAction = null) {
        InitiateFade(0f, 1f, fadeCompleteAction);
    }

    /// <summary>
    /// Fade the canvas out
    /// </summary>
    public void FadeOut(Action fadeCompleteAction = null) {
        InitiateFade(1f, 0f, fadeCompleteAction);
    }

    /// <summary>
    /// Fame from from value to to value
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    private void InitiateFade(float from, float to, Action fadeCompleteAction) {
        //stop current fade and go into the new one
        if (_fadeRoutine != null) StopCoroutine(_fadeRoutine);
        _fadeCanvas.gameObject.SetActive(true);
        _fadeRoutine = StartCoroutine(FadeFromTo(from, to, fadeCompleteAction));
    }

    /// <summary>
    /// Perform the fade over time
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    private IEnumerator FadeFromTo(float from, float to, Action onFadeComplete) {
        float timer = 0;
        float alphaValue = from;

        while (timer < _fadeDuration) {
            timer += Time.deltaTime;
            alphaValue = Mathf.Lerp(from, to, timer / _fadeDuration);
            _fadeCanvas.alpha = alphaValue;
            yield return null;
        }

        //if we are fully invisible, just turn it off
        if (alphaValue == 0f) {
            _fadeCanvas.gameObject.SetActive(false);
            onFadeComplete();
        }
    }
}
