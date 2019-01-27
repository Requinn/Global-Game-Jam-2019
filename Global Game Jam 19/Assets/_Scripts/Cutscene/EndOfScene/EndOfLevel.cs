using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

/// <summary>
/// End of level sequence. 
/// </summary>
public class EndOfLevel : SequenceObject {

    [SerializeField]
    private SceneLoader _sceneLoader;

    public override IEnumerator DoSequenceAction() {
        float time = 0;
        while (time < 1f) {
            time += Time.deltaTime;
            _affectingPlayer.DoMovements(.75f, false);
            yield return null;
        }
        _sceneLoader.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        yield return null;
    }
}
