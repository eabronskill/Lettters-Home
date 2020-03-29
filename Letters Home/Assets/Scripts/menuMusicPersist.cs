using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuMusicPersist : MonoBehaviour
{
    private static menuMusicPersist _instance;
    public string gameScene;

    public static menuMusicPersist instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<menuMusicPersist>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (this != _instance)
            _instance = null;
        else if (SceneManager.GetActiveScene().name == gameScene)
        {
            Stop();
            this.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            Debug.Log("Null");
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else if (this != _instance)
        {
            //Play();
            Debug.Log("Is Not Null");
            Destroy(this.gameObject);
        }
        else
        {
            Stop();
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    public void Play()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }

    public void Stop()
    {
        gameObject.GetComponent<AudioSource>().Stop();
    }
}
