using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public bool isPaused = false;
    public GameObject Player;
    public GameObject pauseUI;
    private static Vector3 playerPosition;
    private static PlayerMovement pm;
    private static bool firstLoad = true;

    private void Awake()
    {
        pm = Player.GetComponent<PlayerMovement>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (firstLoad)
        {
            Time.timeScale = 1f;
            firstLoad = false;
        }
        else
        {
            Resume();
        }
        Debug.Log("pausemenu");
        pauseUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Pause()
    {
        Time.timeScale = 0.1f;
        isPaused = true;
        //playerPosition = pm.gameObject.transform.position;
        pauseUI.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        isPaused = false;
        //pm.transform.position = playerPosition;
        pauseUI.SetActive(false);
    }

    public void onDestroy()
    {
        Time.timeScale = 1f;
    }
}
