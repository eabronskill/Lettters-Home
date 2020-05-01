using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerExplosionOnCollision : MonoBehaviour
{
    public GameObject Explosion;
    public Light MyLight;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Untagged")
        {
            Explosion.SetActive(true);
            MyLight.enabled = true;
            Destroy(this.gameObject, 0.2f);
        }
    }
}
