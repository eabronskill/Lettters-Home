using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBoard : MonoBehaviour
{
    [SerializeField]
    GameObject Board;
    [SerializeField]
    List<GameObject> BrokenBoards = new List<GameObject>();
    public AudioSource aud;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (aud)
            {
                aud.Play();
            }
            Board.SetActive(false);
            foreach (GameObject g in BrokenBoards)
                g.SetActive(true);

            other.GetComponent<PlayerMovement>().crawl = true;
            other.GetComponent<PlayerMovement>().precrawl = true;
            other.GetComponent<PlayerMovement>().crouch = false;

        }
    }
}
