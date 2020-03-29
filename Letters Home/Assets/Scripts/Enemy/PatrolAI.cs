using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Requires that a Nav Mesh is already baked.
/// </summary>
public class PatrolAI : MonoBehaviour
{
    [HideInInspector]
    public Enemy me;

    public Transform pos1;
    public Transform pos2;

    private NavMeshAgent agent;
    [HideInInspector]
    public bool reset = true;
    private float goingTo = 1;
    [HideInInspector]
    public bool stopMoving = false;
    [HideInInspector]
    public bool isPatroling = true;
    [HideInInspector]
    public bool isFollowing = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        me = gameObject.GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPatroling && !me.Dead)
        {
            if (reset)
            {
                agent.SetDestination(pos1.position);
                goingTo = 1;
                reset = false;
                spriteDir(pos1);
            }
            else if (goingTo == 1 && agent.remainingDistance > 0)
            {
                agent.SetDestination(pos1.position);
                spriteDir(pos1);
            }
            else if (goingTo == 1 && agent.remainingDistance <= 0)
            {   
                goingTo = 2;
                agent.SetDestination(pos2.position);
                me.enemySprite.gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (goingTo == 2 && agent.remainingDistance > 0)
            {
                agent.SetDestination(pos2.position);
                spriteDir(pos2);
            }
            else if (goingTo == 2 && agent.remainingDistance <= 0)
            {
                goingTo = 1;
                agent.SetDestination(pos1.position);
                me.enemySprite.gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        else if (!isPatroling && stopMoving)
        {
            agent.ResetPath();
            stopMoving = false;
        }
        
    }

    /// <summary>
    /// Used to properly orient the direction of the Enemy sprite.
    /// </summary>
    /// <param name="pos"></param>
    private void spriteDir(Transform pos)
    {
        if (this.transform.position.x > pos.transform.position.x)
        {
            me.enemySprite.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            me.enemySprite.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
    
}
