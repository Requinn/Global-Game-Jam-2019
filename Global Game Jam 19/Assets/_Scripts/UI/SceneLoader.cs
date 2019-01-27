using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    int sceneIndexToLoad;
    public void LoadScene(int index)
    {
        sceneIndexToLoad = index;
        SceneFader.Instance.FadeIn(DoSceneChange);
    }

    private void DoSceneChange() {
        SceneManager.LoadScene(sceneIndexToLoad);
    }

}
