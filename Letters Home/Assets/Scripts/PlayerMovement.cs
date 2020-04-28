using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool m_dead;
    Rigidbody me;
    public float MoveSpeed = 1.0f;
    Vector3 MoveVector = new Vector3();
    public bool attached;
    public bool crouch;
    public bool crawl;
    public bool Up;
    public bool isVaulting;
    public float totalVaultTime;
    float vaulttimer;
    public List<Vector2> CapsuleSizes;
    public CapsuleCollider mine;
    Animator Anim;

    private bool canVault;
    private Transform VaultPos;
    private Vector3 StartPos;
    private float timerS;
    private float timerW;
    private float doubleTapTimer = 0.35f;
    public static int Lane = 0;
    public static bool CanChangeLanes = true;

    private bool swapped = false;
    private float spriteHeight;
    public float crouchHeight;
    public Transform spriteT;
    private float ctimer;


    [HideInInspector]
    public float climbSpeed = 1;
    private bool precrawl;

    private bool lb;
    private bool crb;

    private bool forcedCrawl = false;

    private bool left, right, up, down = false;
    public float cinematicTimer = 0f;
    public bool begCinematicStart = false;
    [SerializeField]
    private List<CinematicEvent> cinEvents = new List<CinematicEvent>();
    public int curEvent = 0;
    private float ccts = 0f;

    private bool falling;

    // Start is called before the first frame update
    void Start()
    {
        me = GetComponent<Rigidbody>();
        Anim = GetComponentInChildren<Animator>();
        mine = GetComponent<CapsuleCollider>();
        //spriteT= GetComponentInChildren<Transform>();
        spriteHeight = 0 + spriteT.localPosition.y;
        crouchHeight = 0 + spriteHeight - crouchHeight;
        if (begCinematicStart)
        {
            cinematicTimer = Time.time + cinematicTimer;
            ccts = Time.time + cinEvents[curEvent].timers[cinEvents[curEvent].curMovement];
        }
    }

    public void SetFall(float time)
    {
        Anim.SetBool("Falling", true);
        falling = true;
    }
    public void ResetFall()
    {
        Anim.SetBool("Falling", false);
        falling = false;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (attached)
        {
            Player.me.Lantern = false;

            crawl = false;
            crouch = false;
            transform.Translate(new Vector3(0, Input.GetAxis("Vertical"), 0) * Time.deltaTime * climbSpeed);
            Anim.SetFloat("ClimbDirection", Input.GetAxis("Vertical"));
            //spriteT.transform.rotation = Quaternion.Euler(0,0,Mathf.Lerp(90, spriteT.transform.rotation.z, 0.1f));
            //spriteT.transform.localPosition = new Vector3(1.4f, 0, 0);
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            Anim.SetBool("Climbing", true);
        }
        else
        {
            //spriteT.transform.rotation = Quaternion.identity;
            //spriteT.transform.localPosition = new Vector3(0, 0, 0);
            Anim.SetBool("Climbing", false);
        }

        if (!m_dead && !UI_InvFinder.me.Dialogue)
        {


            MoveVector = Vector3.zero;

           

            

            if (cinematicTimer < Time.time)
            {
                MoveVector += new Vector3(Input.GetAxis("Horizontal") * MoveSpeed, 0, Input.GetAxis("Vertical") * MoveSpeed);
                me.transform.position += new Vector3(MoveVector.x, MoveVector.y, MoveVector.z) * Time.deltaTime;

                if (Input.GetButton("Crawl") && !crb && !forcedCrawl)
                {
                    timerS = Time.time;
                    ToggleCrawl();
                    crb = true;
                }
                if (Input.GetButtonUp("Crawl"))
                    crb = false;

                if (Input.GetButton("Crouch") && !lb && !forcedCrawl)
                {
                    timerW = Time.time;
                    ToggleCrouch();
                    lb = true;
                }
                if (Input.GetButtonUp("Crouch"))
                    lb = false;


                if (canVault && (Input.GetButtonDown("Vault")) && !isVaulting && !crawl && !crouch)
                {

                    StartPos = transform.position;
                    vaulttimer = Time.time + totalVaultTime;
                    isVaulting = true;
                    canVault = false;
                }
                if (isVaulting && vaulttimer > Time.time)
                {
                    Anim.SetBool("Vaulting", true);
                    transform.position = Vector3.Slerp(VaultPos.position, StartPos, (vaulttimer - Time.time) / totalVaultTime);
                    me.isKinematic = true;
                    me.useGravity = false;
                    mine.enabled = false;
                }
                else if (isVaulting && vaulttimer < Time.time)
                {
                    print("ended vault!");
                    Anim.SetBool("Vaulting", false);
                    isVaulting = false;
                    me.isKinematic = false;
                    me.useGravity = true;
                    mine.enabled = true;
                }


                if (Input.GetAxis("Horizontal") < 0)
                {
                    if (Player.me.CanShoot == false && !attached)
                        transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);

                    Anim.SetBool("Walkin", true);
                }
                else if ((Input.GetAxis("Horizontal") > 0))
                {
                    if (Player.me.CanShoot == false && !attached)
                        transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
                    //else if(transform.localScale.x == -1)
                    //{
                    //Anim.SetFloat("WalkMod", -1);
                    //}
                    Anim.SetBool("Walkin", true);
                }
                else if ((Input.GetAxis("Vertical") != 0))
                {
                    Anim.SetBool("Walkin", true);
                }
                else
                    Anim.SetBool("Walkin", false);
            }
            else if (cinematicTimer > Time.time)
            {
                if (ccts > Time.time)
                {
                    // Left
                    if (cinEvents[curEvent].movement[cinEvents[curEvent].curMovement].left) //Can you read?
                    {
                        MoveVector = new Vector3(MoveVector.x - MoveSpeed, MoveVector.y, MoveVector.z);
                        if (Player.me.CanShoot == false && !attached)
                            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);

                        Anim.SetBool("Walkin", true);
                    }

                    // Right
                    if (cinEvents[curEvent].movement[cinEvents[curEvent].curMovement].right)
                    {
                        MoveVector = new Vector3(MoveVector.x + MoveSpeed, MoveVector.y, MoveVector.z);
                        if (Player.me.CanShoot == false && !attached)
                            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);

                        Anim.SetBool("Walkin", true);
                    }

                    me.transform.position += new Vector3(MoveVector.x, MoveVector.y, MoveVector.z) * Time.deltaTime;
                }
                else
                {
                    if (cinEvents[curEvent].movement.Count > cinEvents[curEvent].curMovement+1)
                    {
                        cinEvents[curEvent].curMovement++;
                    }
                    ccts = Time.time + cinEvents[curEvent].timers[cinEvents[curEvent].curMovement];
                }
            }
            if (crawl)
            {

                spriteT.localPosition = Vector3.Lerp(new Vector3(0, crouchHeight, 0), new Vector3(0, spriteHeight, 0), (ctimer - Time.time)/.5f);
                mine.center = new Vector3(0, CapsuleSizes[2][0], 0);
                mine.height = CapsuleSizes[2][1];
                mine.direction = 0;


                Anim.SetBool("Crawlin", true);
                Anim.SetBool("Crouch", false);
                MoveSpeed = 1f;
            }
            else if (crouch)
            {
                if (precrawl && ctimer > Time.time)
                {
                    spriteT.localPosition = Vector3.Lerp(new Vector3(0, spriteHeight, 0), new Vector3(0, crouchHeight, 0), (ctimer - Time.time) / .25f);
                }
                else if (precrawl)
                {
                    precrawl = false;
                }
                mine.center = new Vector3(0, CapsuleSizes[1][0], 0);
                mine.height = CapsuleSizes[1][1];
                mine.direction = 1;
                Anim.SetBool("Crawlin", false);
                Anim.SetBool("Crouch", true);
                MoveSpeed = 1.5f;
                //print("Crouching!!");

            }
            else if (!crouch && !crawl)
            {
                if (precrawl && ctimer > Time.time)
                {
                    spriteT.localPosition = Vector3.Lerp(new Vector3(0, spriteHeight, 0), new Vector3(0, crouchHeight, 0), (ctimer - Time.time) / .25f);
                }
                else if (precrawl)
                {
                    precrawl = false;
                }
                mine.height = CapsuleSizes[0][1];
                mine.center = new Vector3(0, CapsuleSizes[0][0], 0);
                Anim.SetBool("Crawlin", false);
                Anim.SetBool("Crouch", false);
                //print("Not Crouching!!");
                mine.direction = 1;
                MoveSpeed = 2.25f;
            }
            //Condition for the crawling
        }
        else
        {
            if (m_dead == true)
            {
                if (precrawl)
                {
                    spriteT.localPosition = new Vector3(0, spriteHeight, 0);
                    precrawl = false;
                }
                Anim.SetBool("Dead", true);
            }
            Anim.SetBool("Crawlin", false);
            Anim.SetBool("Walkin", false);
            Anim.SetBool("Crouch", false);
        }
    }

    public void SetSpeed(int nes)
    {
        MoveSpeed = nes;
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Vaultable")
        {
            canVault = true;
            VaultPos = col.gameObject.GetComponent<Vaultable>().VaultEndpoint;
            totalVaultTime = col.gameObject.GetComponent<Vaultable>().vaultTime;
            //if (transform.localScale.x != 1)
                //VaultPos.position = new Vector3(VaultPos.position.x - 5, VaultPos.position.y, VaultPos.position.z);
        }
    }

     void OnTriggerStay(Collider other)
     {
        if(other.gameObject.tag == "Vaultable")
        {
            bool t = other.gameObject.GetComponent<Vaultable>().type == "Side";
            if (t)
            {
                print("asdfasdfasdfadf");
                canVault = true;
                VaultPos = other.gameObject.GetComponent<Vaultable>().VaultEndpoint;
                VaultPos.position = new Vector3(this.transform.position.x, VaultPos.position.y, VaultPos.position.z);
            }
        }
        
        if (other.gameObject.tag == "Crouch")
        {
            if (!crouch && !crawl)
            {
                crouch = true;
            }
        }
        if (other.gameObject.tag == "Crawl")
        {
            crouch = false;
            crawl = true;
            forcedCrawl = true;
            precrawl = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Vaultable")
        {
            canVault = false;
        }
        if (col.gameObject.tag == "Crawl")
        {
            forcedCrawl = false;
        }
    }


    void ToggleCrawl()
    {
        crawl = !crawl;
        crouch = false;
        if(crawl)
            ctimer = Time.time + .5f;
        else
            ctimer = Time.time + 0.25f;
        if (crawl == true)
        {
            precrawl = true;
        }
    }

    void ToggleCrouch()
    {
        crouch = !crouch;
        crawl = false;
        ctimer = Time.time + 0.25f;
    }


}


[System.Serializable]
public class CinematicEvent
{
    public int curMovement = 0;
    public List<MovementArray> movement = new List<MovementArray>();
    public List<float> timers = new List<float>();

}

[System.Serializable]
public class MovementArray
{
    public bool right, left, up, down = false;
}

