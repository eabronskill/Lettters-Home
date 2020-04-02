using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vaultable : MonoBehaviour
{
    public Transform VaultEndpoint;
    public string type;
    public float vaultTime = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Vaultable";
    }
}
