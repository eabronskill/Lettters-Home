using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinamaticMovement : MonoBehaviour
{
    public List<Railpoint> PosList = new List<Railpoint>();
    public bool anim;
    public string wTag;
    public string dTag;
    public Animator link;
    private int Journey = -1;
    private float timer;

    // Update is called once per frame
    void Update()
    {
        if(timer > Time.time)
        {
            if(anim)
                link.SetBool(wTag, true);
            transform.position = Vector3.Lerp(PosList[Journey].EndPoint.position, PosList[Journey].StartPoint.position, (timer - Time.time) / PosList[Journey].jTime);

            transform.localScale = new Vector3((PosList[Journey].StartPoint.position.x < PosList[Journey].EndPoint.position.x) ? 1 : -1,1,1);
        }
        else
        {
            if(anim)
                link.SetBool(wTag, false);
        }
    }


    public void CallNext()
    {
        Journey += 1;
        if (Journey < PosList.Count) {
            timer = Time.time + PosList[Journey].jTime;
        }
    }   

    public void SetJourney(int point)
    {
        Journey = point;
    }

    public void SetDead()
    {
        if(anim)
            link.SetBool(dTag, true);
    }

    public void SetSmoking()
    {
        if (anim)
            link.SetBool("Smoking", true);
    }

}
[System.Serializable]
public class Railpoint
{
    public float jTime = 1f;
    public Transform StartPoint;
    public Transform EndPoint;
}
