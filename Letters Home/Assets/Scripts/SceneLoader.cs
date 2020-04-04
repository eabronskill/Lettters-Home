using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public float delay = 2f;

    public void LoadLevel(string scene)
    {
        Time.timeScale = 1f;
            UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
            MySceneManager.LoadScene(scene);
    }

    public void LoadLevelWithDelay(string scene)
    {
        { StartCoroutine(sceneChange()); }

        IEnumerator sceneChange()
        {
            yield return new WaitForSeconds(delay);
            LoadLevel(scene);
        }
    }
}
