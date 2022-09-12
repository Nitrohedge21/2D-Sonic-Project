using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RingCounter : MonoBehaviour
{
    public static RingCounter instance;
    public TextMeshProUGUI text;
    int score;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void ChangeScore(int RingValue)
    {
        score += RingValue;
        text.text = "x " + score.ToString();
    }
}
