using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    bool isDead = false;
    PlayerMovement moveyBoi;
    public bool dead() { return isDead; }

    private RaycastHit hit;
    public Item myItem;
    public bool CanShoot = false;
    public bool Lantern = false;
    public float reloadTime = 1f;
    public float shotTime = 0.5f;
    public bool CanReload = false;
    public AudioSource Aud;
    public AudioClip Shot;
    public AudioClip ReloadS;
    public bool Reloading = false;
    private float shottimer;
    private float reloadtimer;
    public int maxAmmo = 60;
    public int MagAmmo = 0;
    public int MagSize = 1;
    public int ammo = 0;
    public int Letters = 0;

    static bool killCurrentPlayer = false;
    static Player me;

    [SerializeField]
    private Sprite baseItem;

    //Debug only
    Ray newRay;

    // Start is called before the first frame update
    void Start()
    {
        if(me == null)
        {
            me = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        moveyBoi = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(myItem == null && UI_InvFinder.me.ItemImage.sprite != baseItem)
        {
            UI_InvFinder.me.ItemImage.sprite = baseItem;
            UI_InvFinder.me.ItemText.text = "";
        }

        Debug.DrawRay(newRay.origin, newRay.direction);
        if (isDead && moveyBoi.m_dead != true)
        {
            moveyBoi.m_dead = true;
        }


        if (moveyBoi.crawl && CanShoot == true)
        {
            CanShoot = false;
        }


        if (Input.GetButtonDown("UseItem"))
        {
            print("Using Item!");
            InvokeItem();
        }

        if (ammo < 0)
        {
            ammo = 0;
        }
        else if (ammo > maxAmmo)
        {
            ammo = maxAmmo;
        }
        if (MagAmmo > MagSize)
        {
            MagAmmo = MagSize;
        }
        else if (MagAmmo < 0)
        {
            MagAmmo = 0;
        }
        if (CanReload && reloadtimer < Time.time)
        {
            Aud.volume = 0.75f;
            Aud.PlayOneShot(ReloadS);
            CanReload = false;
            reloadtimer = Time.time + reloadTime;
            Reloading = true;
            Invoke("Reload", reloadTime);
        }
        if (CanShoot && shottimer < Time.time)
        {
            Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            float intersectionDistance = 0f;


            if (Input.GetMouseButtonDown(0) && MagAmmo > 0)
            {
                MagAmmo -= 1;
                shottimer = Time.time + shotTime;
                if (Physics.Raycast(cameraRay, out hit, 10000.0f))
                {
                    //print("hit at:" + hit.point);
                    if (hit.collider.tag == "Enemy")
                    {
                        Enemy toHit = hit.collider.GetComponentInParent<Enemy>();
                        RaycastHit hit2;
                        newRay = new Ray((this.transform.position), (toHit.transform.position - this.transform.position));
                        if(Physics.Raycast(newRay, out hit2, 50.0f));
                        {
                            if(hit2.collider.tag == "Enemy")
                            {
                                print(hit.collider.name);
                                if (hit.collider.name == "Head")
                                {
                                    toHit.ShotSpots[2] = true;
                                }
                                else if(hit.collider.name == "Legs")
                                {
                                    toHit.ShotSpots[1] = true;
                                }
                                else
                                {
                                    toHit.ShotSpots[0] = true;
                                }
                                toHit.Dead = true;
                            }
                        }
                    }


                }
                Aud.volume = 1f;
                Aud.PlayOneShot(Shot);
                print("Took a shot");

            }
            if (Input.GetButtonDown("Reload") && CanReload == false)
            {
                CanReload = true;
                print("Reloading!");
            }
        }
        if (isDead)
        {
            CanShoot = false;
        }
        if (killCurrentPlayer)
        {
            killCurrentPlayer = false;
            KillPlayer();
        }
    }

    public static void KillPlayer()
    {
        Destroy(me.gameObject);
        me = null;
    }

    public void Reload()
    {
        Reloading = false;
        if (MagSize <= ammo)
        {
            MagAmmo = MagSize;
            ammo -= MagSize;
        }
        else
        {
            MagAmmo = ammo;
            ammo = 0;
        }
        print("Finished Reloading");
    }


    public void EquipItemPlayer(Item toQuip)
    {
        if(myItem != null)
        {
            myItem.Dequip();
            myItem.gameObject.transform.position = transform.position + Vector3.down;
            myItem.gameObject.SetActive(true);
        }
        myItem = toQuip;
        myItem.Equip();
    }

    public void DequipCurrentItem()
    {
        myItem.Dequip();
    }

    public void Reset()
    {
        isDead = false;
    }

    public void SetDead()
    {
        isDead = true;
    }

    public bool GetDead()
    {
        return isDead;
    }

    public void InvokeItem()
    {
        if(myItem != null)
        {
            myItem.UseItem();
        }
    }

}
