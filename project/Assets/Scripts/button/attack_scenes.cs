using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class attack_scene : MonoBehaviour
{
    public void attack_scene_start()
    {
        SceneManager.LoadScene("start_game");
    }
    public void attack_scene_option()
    {
        SceneManager.LoadScene("option");
    }
}
