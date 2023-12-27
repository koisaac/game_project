using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private bool player_turn = true;
    public List<BoxCollider2D> bounds;
    public int map_number;
    public GameObject button_script;

    private void Awake()
    {   
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
  
    }
    public BoxCollider2D getbound()
    {
        return bounds[map_number];
    }

    public static GameManager Instance()
    {
        return instance;
    }
    
    public bool Is_player_turn()
    {
        return player_turn;
    }
    public void turn_end() {
        button_script.GetComponent<Activate>().click_attack_false();
   
        player_turn = !player_turn;
    }

}
