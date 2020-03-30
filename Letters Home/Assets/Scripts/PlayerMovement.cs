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
    public float climbSpeed = 3;
    private bool precrawl;

    private bool lb;
    private bool crb;

    // Start is called before the first frame update
    void Start()
    {
        me = GetComponent<Rigidbody>();
        Anim = GetComponentInChildren<Animator>();
        mine = GetComponent<CapsuleCollider>();
        //spriteT= GetComponentInChildren<Transform>();
        spriteHeight = 0 + spriteT.localPosition.y;
        crouchHeight = 0 + spriteHeight - crouchHeight;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (attached)
        {
            crawl = false;
            crouch = false;
            transform.Translate(new Vector3(0, Input.GetAxis("Vertical"), 0) * Time.deltaTime * climbSpeed);
        }

        if (!m_dead && !UI_InvFinder.me.Dialogue)
        {


            MoveVector = Vector3.zero;

            MoveVector += new Vector3(Input.GetAxis("Horizontal") * MoveSpeed, 0, Input.GetAxis("Vertical") * MoveSpeed);

            me.transform.position += new Vector3(MoveVector.x, MoveVector.y, MoveVector.z) * Time.deltaTime;

            if (Input.GetButton("Crawl") && !crb)
            {
                timerS = Time.time;
                ToggleCrawl();
                crb = true;
            }
            if (Input.GetButtonUp("Crawl"))
                crb = false;

            if (Input.GetButton("Crouch") && !lb)
            {
                timerW = Time.time;
                ToggleCrouch();
                lb = true;
            }
            if (Input.GetButtonUp("Crouch"))
                lb = false;
            

            if (canVault && (Input.GetButtonDown("Vault")))
            {
                if (transform.localScale.x == 1)
                    transform.position = VaultPos.position;
                else
                    transform.position = new Vector3(VaultPos.position.x - 5, VaultPos.position.y, VaultPos.position.z);
                canVault = false;
            }



            if (Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Vertical") > 0)
            {
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                Anim.SetBool("Walkin", true);
            }
            else if ((Input.GetAxis("Horizontal") > 0) || Input.GetAxis("Vertical") < 0)
            {
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
                Anim.SetBool("Walkin", true);
            }
            else
                Anim.SetBool("Walkin", false);


            if (crawl)
            {
                
                spriteT.localPosition = Vector3.Lerp(new Vector3(0, crouchHeight, 0), new Vector3(0, spriteHeight, 0), ctimer - Time.time);
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
                    spriteT.localPosition = Vector3.Lerp(new Vector3(0, spriteHeight, 0), new Vector3(0, crouchHeight, 0), (ctimer - Time.time)/.25f);
                }
                else if(precrawl)
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
                    spriteT.localPosition = Vector3.Lerp(new Vector3(0, spriteHeight, 0), new Vector3(0, crouchHeight, 0), (ctimer - Time.time)/.25f);
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
            print("found1");
            VaultPos = col.gameObject.GetComponent<Vaultable>().VaultEndpoint;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Vaultable")
        {
            print("found1");
            canVault = false;
        }
    }


    void ToggleCrawl()
    {
        crawl = !crawl;
        crouch = false;
        if(crawl)
            ctimer = Time.time + 1f;
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
