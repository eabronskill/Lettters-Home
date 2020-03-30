using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZAxisOrderInLayer : MonoBehaviour
{

    private int BaseZ;
    private Transform ParentPos;
    private SpriteRenderer Renderer;
    // Start is called before the first frame update
    void Start()
    {
        Renderer = GetComponent<SpriteRenderer>();
        ParentPos = GetComponentInParent<Transform>();
        BaseZ = Renderer.sortingOrder;
    }

    // Update is called once per frame
    void Update()
    {
        Renderer.sortingOrder = Mathf.RoundToInt((BaseZ - (ParentPos.position.z*10.0f)));
    }
}
