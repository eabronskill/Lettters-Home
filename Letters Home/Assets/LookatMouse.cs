using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookatMouse : MonoBehaviour
{
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(cameraRay, out hit, 1000f))
            if (hit.collider != null)
            {
                transform.LookAt(new Vector3(hit.point.x, hit.point.y, Player.me.transform.position.z));
                if(hit.point.x < Player.me.gameObject.transform.position.x && Player.me.gameObject.transform.localScale.x == 1)
                {
                    Player.me.gameObject.transform.localScale = new Vector3(-1,1,1);
                }
                else if (hit.point.x > Player.me.gameObject.transform.position.x && Player.me.gameObject.transform.localScale.x == -1)
                {
                    Player.me.gameObject.transform.localScale = new Vector3(1, 1, 1);
                }
            }
    }
}
