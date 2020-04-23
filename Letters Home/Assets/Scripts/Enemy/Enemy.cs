﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    EnemyLOS los;
    PatrolAI patrol;
    
    [HideInInspector]
    public GameObject Target;
    public GameObject enemySprite;
    private NavMeshAgent agent;

    public float speed = 3f;
    public float ganderSpeed = 0.75f;
    [HideInInspector]
    public bool isLooking = false;
    [HideInInspector]
    public bool isSearching = false;

    public float domeTimer = 1.0f;
    public float lookTime = 2.0f;
    private float ltimer = 0.0f;
    public bool Dead = false;
    private bool HasDied = false;
    public List<bool> ShotSpots = new List<bool>();
    [SerializeField]
    private Animator Anim;
    private float ct = 0;
    private float ctimer;


    public bool MaybeSmoke = false;
    public float SmokeTimer = 10f;
    public GameObject Cigarette;
    public GameObject Gun;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<EnemyLOS>() != null)
        {
            los = GetComponent<EnemyLOS>();
        }
        if (GetComponent<PatrolAI>() != null)
        {
            patrol = GetComponent<PatrolAI>();
        }
        if (GetComponent<NavMeshAgent>() != null)
        {
            agent = GetComponent<NavMeshAgent>();
            agent.speed = speed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SmokeTimer < Time.time && Target == null && !Dead)
        {
            int c = UnityEngine.Random.Range(0, 4);
            c = 3;
            if (c == 3)
            {
                MaybeSmoke = true;
                Anim.SetBool("Smoking", true);
                patrol.isPatroling = false;
                patrol.stopMoving = true;
                Anim.SetBool("Walkin", false);
                if (Cigarette != null)
                {
                    Cigarette.SetActive(true);
                }
            }
            else
            {
                MaybeSmoke = false;
                patrol.isPatroling = true;
                patrol.reset = true;
                Anim.SetBool("Smoking", false);
                if (Cigarette != null)
                {
                    Cigarette.SetActive(false);
                }
            }
            SmokeTimer = Time.time + 10.0f;
        }
        else if(Target != null || Dead)
        {
            MaybeSmoke = false;
            Anim.SetBool("Smoking", false);
            if (Cigarette != null)
            {
                Cigarette.SetActive(false);
            }
            SmokeTimer = Time.time + 10.0f;
        }

        if (!Dead)
        {


            if (los && patrol && agent)
            {
                patrollingLogic();
            }

            if (ctimer < Time.time && !MaybeSmoke && !los.canSee)
            {
                if (Mathf.Abs(transform.position.x - ct) > 0.1f)
                    Anim.SetBool("Walkin", true);
                else
                    Anim.SetBool("Walkin", false);
                if (ct < transform.position.x)
                {
                    Vector3 temp = new Vector3(0.0f - Mathf.Abs(enemySprite.transform.localScale.x), enemySprite.transform.localScale.y, enemySprite.transform.localScale.z);
                    enemySprite.transform.localScale = temp;
                    
                }
                if (ct > transform.position.x)
                {
                    print(Mathf.Abs(enemySprite.transform.localScale.x));
                    Vector3 temp = new Vector3(Mathf.Abs(enemySprite.transform.localScale.x), enemySprite.transform.localScale.y, enemySprite.transform.localScale.z);
                    enemySprite.transform.localScale = temp;
                }
                ctimer = Time.time + 0.2f;
                ct = transform.position.x;
            }
            enemySprite.transform.LookAt(enemySprite.transform.position + new Vector3(0, 0, 10000));
            
        }
        else if (!HasDied)
        {
            HasDied = true;
            Die();
        }
    }


    private void Die()
    {
        Destroy(this.gameObject, 10f);
        agent.enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().isKinematic = true;
        //Do Stuff with anims later.
        Anim.SetBool("Dead", true);
        if(ShotSpots[2] == true)
        {
            print("Headshot");
        }else if(ShotSpots[1] == true)
        {
            print("LegShot");
        }
        else
        {
            print("BodyShot");
        }
        Destroy(patrol);
        agent.ResetPath();
        Destroy(this);
    }
    /// <summary>
    /// Uses information from the PatrolAI script and EnemyLOS script to control the behavior of this Enemy.
    /// </summary>
    private void patrollingLogic()
    {
        // If this enemy can see the Player, stop patrolling and get ready to shoot.
        if (los.canSee && los.Target != null && !los.Target.GetComponent<Player>().GetDead())
        {
            print("Shooting Dude");
            Target = los.Target;
            patrol.isPatroling = false;
            patrol.stopMoving = true;
            if (!Anim.GetBool("Aiming"))
                Invoke("ShootTarget", domeTimer);
            Anim.SetBool("Walkin", false);
            agent.speed = 1;
            agent.stoppingDistance = 8;
            agent.SetDestination(Target.transform.position);
            if(Gun != null)
            {
                Gun.SetActive(true);
                Anim.SetBool("Aiming", true);
            }
        }
        // If the enemy has reached the last seen location of the Player, wait for the look timer to run out, then go back to patrolling.
        else if (!los.canSee && isSearching && Target != null && agent.remainingDistance <= 0)
        {
            isSearching = false;
            isLooking = true;
            ltimer = Time.time + lookTime;
            agent.ResetPath();
            agent.stoppingDistance = 0;
            Anim.SetBool("Walkin", true);
        }
        // This enemy is at the last seen location of the Player, and is looking around for them.
        else if (!los.canSee && isLooking && Target != null && ltimer > Time.time)
        {
            // TODO
            // Looking Animation
            Anim.SetBool("Walkin", false);
        }
        // This enemy is at the last seen location of the Player, and has finished looking for them.
        else if (!los.canSee && isLooking && Target != null && Time.time > ltimer)
        {
            patrol.reset = true;
            patrol.isPatroling = true;
            isLooking = false;
            Target = null;
            agent.speed = speed;
            Anim.SetBool("Walkin", true);
        }
    }

    /// <summary>
    /// Helper method used for killing the Player.
    /// </summary>
    void ShootTarget()
    {
        if (los.canSee && !Dead)
        {
            // Kill the Player
            Target.GetComponent<Player>().SetDead();
            Debug.Log("KiLL!");

            // Reset patrol
            patrol.isPatroling = true;
            Anim.SetBool("Walkin", true);
            patrol.reset = true;
            agent.speed = speed;
            if (Gun != null)
            {
                Gun.SetActive(false);
                Anim.SetBool("Aiming", false);
            }
        }
        else
        {
            isSearching = true;
            agent.speed = ganderSpeed;
            // Go to the last seen location of the player.
            agent.SetDestination(los.LastSeen);
            if (Gun != null)
            {
                Gun.SetActive(false);
                Anim.SetBool("Aiming", false);
            }
        }
    }

}
