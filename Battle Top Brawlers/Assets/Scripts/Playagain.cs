using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Playagain : MonoBehaviour
{
   public void Replay()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
