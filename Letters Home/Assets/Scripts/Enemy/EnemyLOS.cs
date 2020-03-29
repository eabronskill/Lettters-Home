using UnityEngine;

public class EnemyLOS : MonoBehaviour
{
    public Transform RayStart;
    public float maxDis;

    [HideInInspector]
    public GameObject Target;
    [HideInInspector]
    public Vector3 LastSeen;
    [HideInInspector]
    public bool canSee = false;


    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            

            Player check = other.GetComponent<Player>();
            Target = check.gameObject;
            if (!check.GetDead()) {
                
                Ray ray = new Ray(RayStart.position, (other.gameObject.transform.position - RayStart.position));
                Ray ray1 = new Ray(RayStart.position, ((other.gameObject.transform.position + Vector3.up) - RayStart.position));
                Ray ray2 = new Ray(RayStart.position, ((other.gameObject.transform.position - Vector3.up) - RayStart.position));
                
                Physics.Raycast(ray, out RaycastHit hit, maxDis);
                Physics.Raycast(ray1, out RaycastHit hit1, maxDis);
                Physics.Raycast(ray2, out RaycastHit hit2, maxDis);

                // If it hits something...
                if (hit.collider != null && hit.collider.gameObject.tag == "Player")
                {
                    LastSeen = hit.collider.gameObject.transform.position;
                    Debug.DrawRay(RayStart.position, (other.gameObject.transform.position - RayStart.position), Color.magenta);
                    canSee = true;
                }
                else if(hit1.collider != null && hit1.collider.gameObject.tag == "Player")
                {
                    LastSeen = hit1.collider.gameObject.transform.position;
                    Debug.DrawRay(RayStart.position, ((other.gameObject.transform.position + Vector3.up) - RayStart.position), Color.magenta);
                    canSee = true;
                }
                else if(hit2.collider != null && hit2.collider.gameObject.tag == "Player")
                {
                    LastSeen = hit2.collider.gameObject.transform.position;
                    Debug.DrawRay(RayStart.position, ((other.gameObject.transform.position - Vector3.up) - RayStart.position), Color.magenta);
                    canSee = true;
                }
                else
                {
                    canSee = false;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Target = null;
            canSee = false;
        }
    }
}
