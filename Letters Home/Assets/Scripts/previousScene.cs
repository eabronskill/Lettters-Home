using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class previousScene : MonoBehaviour
{
    public void back(float delay)
    {
        { StartCoroutine(sceneChange()); }

        IEnumerator sceneChange()
        {
            yield return new WaitForSeconds(delay);
            // reload the current scene
            MySceneManager.LoadPreviousScene();
        }
    }
}