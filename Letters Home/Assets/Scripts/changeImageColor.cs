using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class changeImageColor : MonoBehaviour
{
    [Header("Attach to Image")]
    public float FadeDuration = 1f;
    public Color startColor;
    public Color endColor;

    [SerializeField] private UnityEngine.UI.Image source;

    private void Start()
    {
        source = GetComponentInParent<UnityEngine.UI.Image>();
    }

    public void Update()
    {
        source.color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time, FadeDuration));

    }
}
