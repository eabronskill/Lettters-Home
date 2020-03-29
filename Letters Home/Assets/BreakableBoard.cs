using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBoard : MonoBehaviour
{
    [SerializeField]
    GameObject Board;
    [SerializeField]
    List<GameObject> BrokenBoards = new List<GameObject>();
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Board.SetActive(false);
            foreach (GameObject g in BrokenBoards)
                g.SetActive(true);

            other.GetComponent<PlayerMovement>().crawl = true;
            other.GetComponent<PlayerMovement>().crouch = false;

        }
    }
}
