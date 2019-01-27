using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    int sceneIndexToLoad;

    /// <summary>
    /// this one is for he UI calls
    /// </summary>
    /// <param name="index"></param>
    public void UILoadScene(int index) {
        LoadScene(index);
    }

    /// <summary>
    /// Load scene wih the choice to use the screen fader
    /// </summary>
    /// <param name="index"></param>
    /// <param name="useFader"></param>
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
