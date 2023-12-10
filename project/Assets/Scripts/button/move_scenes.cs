using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class move_scenes : MonoBehaviour
{
    public void move_scene_start()
    {
        SceneManager.LoadScene("start_game");
    }
    public void move_scene_option()
    {
        SceneManager.LoadScene("option");
    }
}

