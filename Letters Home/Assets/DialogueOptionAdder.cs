using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueOptionAdder : MonoBehaviour
{
    public Dialog_Basic dialog = new Dialog_Basic();
    public List<Choice> newChoices;
    private List<Choice> resetChoices;
    public bool shouldReset = true;

    void Start()
    {
    }

    public void Trigger()
    {
        resetChoices = dialog.choices;
        //Player player = SmoothCam2D.findCam.GetComponent<SmoothCam2D>().Target.gameObject.GetComponent<Player>();
        dialog.choices = newChoices;
    }

    public void TriggerReset()
    {
        if (shouldReset)
        {

            dialog.choices = resetChoices;
        }
    }

    public void setReset(bool reset)
    {
        shouldReset = reset;
    }
}
