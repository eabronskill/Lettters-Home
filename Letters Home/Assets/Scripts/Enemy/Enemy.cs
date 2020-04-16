using System;
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
        if (!Dead)
        {
            enemySprite.transform.position = this.transform.position;

            if (los && patrol && agent)
            {
                patrollingLogic();
            }
        }
        else if(!HasDied)
        {
            HasDied = true;
            Die();
        }
    }


    private void Die()
    {
        Destroy(enemySprite.gameObject, 0.5f);
        Destroy(this.gameObject, 0.5f);
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
    }
    /// <summary>
    /// Uses information from the PatrolAI script and EnemyLOS script to control the behavior of this Enemy.
    /// </summary>
    private void patrollingLogic()
    {
        // If this enemy can see the Player, stop patrolling and get ready to shoot.
        if (los.canSee && los.Target != null && !los.Target.GetComponent<Player>().GetDead())
        {
            Target = los.Target;
            patrol.isPatroling = false;
            patrol.stopMoving = true;
            Invoke("ShootTarget", domeTimer);
            Anim.SetBool("Walkin", false);
        }
        // If the enemy has reached the last seen location of the Player, wait for the look timer to run out, then go back to patrolling.
        else if (!los.canSee && isSearching && Target != null && agent.remainingDistance <= 0)
        {
            isSearching = false;
            isLooking = true;
            ltimer = Time.time + lookTime;
            agent.ResetPath();
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
        if (los.canSee)
        {
            // Kill the Player
            Target.GetComponent<Player>().SetDead();
            Debug.Log("KiLL!");

            // Reset patrol
            patrol.isPatroling = true;
            Anim.SetBool("Walkin", true);
            patrol.reset = true;
            agent.speed = speed;
        }
        else
        {
            isSearching = true;
            agent.speed = ganderSpeed;
            // Go to the last seen location of the player.
            agent.SetDestination(los.LastSeen);
        }
    }

}
