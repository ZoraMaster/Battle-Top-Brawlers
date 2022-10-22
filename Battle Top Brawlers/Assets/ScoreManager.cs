using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set;}
    public static int scoreValue = 0;
    Text score;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(this);
        }
        score = GetComponent<Text>();
    }


    void Update()
    {
        score.text = "Score: " + scoreValue;
    }

}
