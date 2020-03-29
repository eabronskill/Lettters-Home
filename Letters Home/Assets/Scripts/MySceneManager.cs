using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MySceneManager : MonoBehaviour
{
    private static bool destroyed = false;
    private static object lockObject = new object();
    private static MySceneManager instance;
    private Stack<int> loadedLevels;
    private static PauseMenu _PauseMenu = new PauseMenu();

    public static MySceneManager Instance
    {
        get
        {
            if (destroyed)
            {
                Debug.LogWarning("[Singleton] Instance '" + typeof(MySceneManager) +
                    "' already destroyed. Returning null.");
                return null;
            }

            lock (lockObject)
            {
                if (instance == null)
                {
                    // Search for existing instance.
                    instance = (MySceneManager)FindObjectOfType(typeof(MySceneManager));

                    // Create new instance if one doesn't already exist.
                    if (instance == null)
                    {
                        // Need to create a new GameObject to attach the singleton to.
                        var singletonObject = new GameObject();
                        instance = singletonObject.AddComponent<MySceneManager>();
                        singletonObject.name = typeof(MySceneManager).ToString() + " (Singleton)";

                        // Make instance persistent.
                        DontDestroyOnLoad(singletonObject);
                    }
                }

                return instance;
            }
        }
    }

    public static UnityEngine.SceneManagement.Scene GetActiveScene()
    {
        return UnityEngine.SceneManagement.SceneManager.GetActiveScene();
    }

    public static void LoadScene(int buildIndex)
    {
        Instance.loadedLevels.Push(GetActiveScene().buildIndex);
        UnityEngine.SceneManagement.SceneManager.LoadScene(buildIndex);
    }

    public static void LoadScene(string sceneName)
    {
        Instance.loadedLevels.Push(GetActiveScene().buildIndex);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public static void LoadPreviousScene()
    {
        if (Instance.loadedLevels.Count > 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(Instance.loadedLevels.Pop());
        }
        else
        {
            Debug.LogError("No previous scene loaded");
            // If you want, you can call `Application.Quit()` instead
        }
            _PauseMenu.Resume();
    }

    private void Awake()
    {
        loadedLevels = new Stack<int>();
    }

    private void OnApplicationQuit()
    {
        destroyed = true;
    }

    private void OnDestroy()
    {
        destroyed = true;
    }
}