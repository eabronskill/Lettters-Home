using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    public static List<Item> allItems;
    public Sprite myUI;
    public string Name;
    private bool canGrab = false;

    public UnityEvent OnUse;
    public UnityEvent OnEquip;
    public UnityEvent OnDequip;
    private GameObject Player;
    public void Start()
    {
        if(PlayerPrefs.HasKey("SavedItem") && PlayerPrefs.GetString("SavedItem") == Name)
        {
            SmoothCam2D.findCam.GetComponent<SmoothCam2D>().Target.GetComponent<Player>().EquipItemPlayer(this);
            canGrab = false;
            UI_InvFinder.me.EquipItem(this);
            UI_InvFinder.me.nearItem = false;
            this.gameObject.SetActive(false);
        }

        allItems.Add(this);
    }

    private void Update()
    {
        if (canGrab && Input.GetButtonDown("Interact"))
        {
            Player.GetComponent<Player>().EquipItemPlayer(this);
            canGrab = false;
            UI_InvFinder.me.EquipItem(this);
            UI_InvFinder.me.nearItem = false;
            this.gameObject.SetActive(false);
            
        }
    }

    public void UseItem()
    {
        OnUse.Invoke();
    }

    public void Equip()
    {
        OnEquip.Invoke();
    }
    public void Dequip()
    {
        OnDequip.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Player = other.gameObject;
            UI_InvFinder.me.messageText.text = "PRESS E TO PICKUP:" + Name;
            UI_InvFinder.me.nearItem = true;
            canGrab = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            UI_InvFinder.me.nearItem = false;
            canGrab = false;
            Player = null;
        }
    }


    public void SaveItem()
    {
        if(Player != null)
        {
            PlayerPrefs.SetString("SavedItem", Name);
        }
    }

    public static void ResetSavedItem()
    {
        PlayerPrefs.SetString("SavedItem", "");
    }

    public static void SaveAllItems()
    {
        foreach (Item i in allItems)
        {
            if (i.Player)
            {
                PlayerPrefs.SetString("SavedItem", i.Name);
            }
        
        }
    }

}
