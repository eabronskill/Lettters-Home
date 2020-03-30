using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform cameraTransform;
    public float parallaxAmount;
    public float offset;
    private float lastPosition;
    private RectTransform flatTrans;

    private void Start()
    {
        lastPosition = cameraTransform.position.x;
        flatTrans = GetComponent<RectTransform>();
    }

    void Update()
    {
        transform.Translate((cameraTransform.position.x - lastPosition) * parallaxAmount, 0, 0);
        lastPosition = cameraTransform.position.x;
        
        if (transform.position.x < cameraTransform.position.x - offset)
        {
            transform.Translate(flatTrans.rect.width * 2 * transform.localScale.x, 0, 0);
        } 
        else if (transform.position.x > cameraTransform.position.x + offset) {
            transform.Translate(- flatTrans.rect.width * 2 * transform.localScale.x, 0, 0);
        }
    }
}
