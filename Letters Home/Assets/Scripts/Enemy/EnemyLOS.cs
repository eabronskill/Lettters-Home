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

            print("Player In Collider");
            Player check = other.GetComponent<Player>();
            Target = check.gameObject;
            if (!check.GetDead()) {
                
                Ray ray = new Ray(RayStart.position + Vector3.up*2, ((other.gameObject.transform.position + Vector3.up * .25f) - RayStart.position));
                Ray ray1 = new Ray(RayStart.position + Vector3.up * 2, ((other.gameObject.transform.position + Vector3.up * .5f) - RayStart.position));
                Ray ray2 = new Ray(RayStart.position + Vector3.up * 2, ((other.gameObject.transform.position - Vector3.up * 0.75f) - RayStart.position));
                
                Physics.Raycast(ray, out RaycastHit hit, maxDis, LayerMask.NameToLayer("EnemyRaycastIgnore"));
                Physics.Raycast(ray1, out RaycastHit hit1, maxDis, LayerMask.NameToLayer("EnemyRaycastIgnore"));
                Physics.Raycast(ray2, out RaycastHit hit2, maxDis, LayerMask.NameToLayer("EnemyRaycastIgnore"));

                // If it hits something...
                print(hit.collider.gameObject);
                Debug.DrawRay(ray1.origin, ray1.direction);
                Debug.DrawRay(ray2.origin, ray2.direction);
                Debug.DrawRay(ray.origin, ray.direction);
                if (hit.collider != null && hit.collider.gameObject.tag == "Player")
                {
                    LastSeen = hit.collider.gameObject.transform.position;
                    Debug.DrawRay(ray.origin, ray.direction, Color.magenta);
                    canSee = true;
                }
                else if(hit1.collider != null && hit1.collider.gameObject.tag == "Player")
                {
                    LastSeen = hit1.collider.gameObject.transform.position;
                    Debug.DrawRay(ray1.origin, ray1.direction, Color.magenta);
                    canSee = true;
                }
                else if(hit2.collider != null && hit2.collider.gameObject.tag == "Player")
                {
                    LastSeen = hit2.collider.gameObject.transform.position;
                    Debug.DrawRay(ray2.origin, ray2.direction, Color.magenta);
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
