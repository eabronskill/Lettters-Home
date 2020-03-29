using UnityEngine;
public class previousScene : MonoBehaviour
{
    public void back()
    {
        // reload the current scene
        MySceneManager.LoadPreviousScene();
    }
}