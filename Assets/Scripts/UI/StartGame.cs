using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void Press()
    {
        SceneManager.LoadScene("CurrentScene");
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Cinema Test");
    }
}
