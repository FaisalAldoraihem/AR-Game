using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenueManager : MonoBehaviour
{
   public void Play()
    {
        GameManager.Instance.LoadAR();
    }

    public void Quit()
    {
        GameManager.Instance.Quit();
    }
}
