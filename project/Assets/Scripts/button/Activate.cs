using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activate : MonoBehaviour
{
    // Start is called before the first frame update
    private bool click_move;


    public void click_move_false()
    {
        this.click_move = false;
    }
    public void move()
    {
        if (!click_move)
        {
            player.Instance().transform.Find("player_move_sets").GetComponent<set_player_move_sets>().activate();
            click_move = true;
        }
    }

    void Start()
    {
        click_move = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
