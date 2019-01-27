using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    int sceneIndexToLoad;
    public void LoadScene(int index, bool useFader = true)
    {
        sceneIndexToLoad = index;
        if(useFader) SceneFader.Instance.FadeIn(DoSceneChange);
        else { DoSceneChange(); }
    }

    private void DoSceneChange() {
        SceneManager.LoadScene(sceneIndexToLoad);
    }
}
