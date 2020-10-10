using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Dialog_Basic : MonoBehaviour
{
    public string NPCName;
    public bool TriggerOnEnter = false;
    private bool canTalk = false;
    public List<Choice> choices = new List<Choice>();
    [HideInInspector] public bool social = true;

    public void SetSocial(bool soc)
    {
        social = soc;
        if(!social && canTalk)
        {
            canTalk = false;
            UI_InvFinder.me.nearItem = false;
        }
    }

    public void EndDialogue()
    {
        UI_InvFinder.me.Dialogue = false;
    }
    // Update is called once per frame
    void Update()
    {

        if (canTalk && Input.GetButton("Interact") && !TriggerOnEnter && UI_InvFinder.me.Dialogue == false)
        {
            UI_InvFinder.me.SetChoices(choices);
            
        }
        else if (canTalk && TriggerOnEnter && UI_InvFinder.me.Dialogue == false)
        {
            UI_InvFinder.me.SetChoices(choices);
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && social)
        {
            UI_InvFinder.me.messageText.text = "Press E to talk to:" + NPCName;
            UI_InvFinder.me.nearItem = true;
            canTalk = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && social)
        {
            UI_InvFinder.me.nearItem = false;
            canTalk = false;
            UI_InvFinder.me.Dialogue = false;
        }
    }

}

[System.Serializable]
public class Choice{
    public Dialog_Basic ToReturn;
    public UnityEvent OnSelect;
    public string Message;
    public List<Choice> Select()
    {
        OnSelect.Invoke();
        if (ToReturn != null)
            return ToReturn.choices;
        return nextSet;
    }

    public List<Choice> nextSet;
}
