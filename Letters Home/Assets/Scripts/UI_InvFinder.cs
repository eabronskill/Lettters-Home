using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InvFinder : MonoBehaviour
{
    public static UI_InvFinder me;
    public Image ItemImage;
    public Text ItemText;
    public GameObject messageObject;
    public Text messageText;
    public bool nearItem;
    public bool Dialogue;

    public GameObject DialogueHolder;
    public Text FirstMessage;
    public Button[] Dialogues;
    public List<Choice> curChoices;

    private Sprite normImage;


    private void Start()
    {
        normImage = ItemImage.sprite;
        if(me == null)
        {
            me = this;
        }
        else
        {
            Destroy(this);
        }
    }


    public void SetChoices(List<Choice> cs)
    {
        curChoices = cs;
        if (curChoices.Count == 0)
        {
            Dialogue = false;
            return;
        }
        FirstMessage.text = curChoices[0].Message;
        for (int i = 1; i < curChoices.Count; i++)
        {
            Dialogues[i-1].GetComponentInChildren<Text>().text = curChoices[i].Message;
        }
        Dialogue = true;
    }

    public void SelectChoice(int selection)
    {
        if(curChoices != null)
        {
            SetChoices(curChoices[selection].Select());
        }
    }

    public void Update()
    {
        messageObject.SetActive(nearItem);
        if (Dialogue)
        {
            DialogueHolder.SetActive(true);
            for (int i = 0; i < curChoices.Count-1; i++)
            {
                Dialogues[i].gameObject.SetActive(true);
            }
            for(int j = curChoices.Count-1; j < Dialogues.Length; j++)
            {
                Dialogues[j].gameObject.SetActive(false);
            }
        }else if(!Dialogue && DialogueHolder.activeInHierarchy)
        {
            DialogueHolder.SetActive(false);
        }
    }

    public void EquipItem(Item item)
    {
        ItemImage.sprite = item.myUI;
        ItemText.text = item.Name;
    }

    public void DequipItem()
    {
        ItemImage.sprite = normImage;
        ItemText.text = "";
    }

}
