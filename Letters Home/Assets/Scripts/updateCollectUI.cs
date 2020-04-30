using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class updateCollectUI : MonoBehaviour
{
    public int totalLetters;
    public Player player;
    public Text currentAmount;
    public Text collected;

    private void Start()
    {
        currentAmount.text = "0";
        collected.text = "/ " + totalLetters.ToString();

    }

    void Update()
    {
        currentAmount.text = player.collectedLetters.ToString();
    }
}
