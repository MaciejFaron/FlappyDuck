using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ExitGame : MonoBehaviour
{
    public void ExitGameOnButton()
    {
        Debug.Log("exitgame");
        Application.Quit();
    }
}