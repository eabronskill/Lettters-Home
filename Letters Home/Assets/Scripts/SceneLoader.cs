using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadLevel(string scene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
        MySceneManager.LoadScene(scene);
    }
}
