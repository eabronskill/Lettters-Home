using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public int Number = 0;
    public string Prefix = "";
    // Start is called before the first frame update
    public void Start()
    {
        if(PlayerPrefs.HasKey("C" + Prefix + Number) && PlayerPrefs.GetInt("C" + Prefix + Number) == 1)
        {
            Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            UI_InvFinder.me.nearItem = true;
            UI_InvFinder.me.messageText.text = "press e to pickup Letter " + Number;
        }
    }
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Save();
                UI_InvFinder.me.nearItem = false;
                Destroy(this.gameObject);
            }
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            UI_InvFinder.me.nearItem = false;
        }
    }
    // Update is called once per frame
    void Save()
    {
        PlayerPrefs.SetInt("C" + Prefix + Number, 1);
    }
    
    void UnSave()
    {
        PlayerPrefs.SetInt("C" + Prefix + Number, 0);
    }
}
