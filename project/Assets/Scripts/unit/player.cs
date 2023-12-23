using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class player : MonoBehaviour
{
    public float move_speed;

    const int move_range_size = 3;

    private static player Player = null;
    

    void Awake()
    {
        if (Player == null) {
      
            Player = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);
    }


    public static player Instance()
    {
        return Player;
    }
    

    
    

    float HP;
    int Level;
    float EXP;
    Weapon weapon;


    private bool is_moving;
    private bool is_attack;
    private bool is_defense;
    private bool check;
    private bool check_move;
    private Vector2 target;
    private Vector2 target_;
    public void move(float x, float y)
    {
        this.target.x = x;
        this.target.y = y;
        is_moving = true;
        is_attack = false; 
        is_defense =false;

        
    }



    private void move_set()
    {
        transform.Find("player_move_sets").GetComponent<set_player_move_sets>().set_player_move_set(move_range_size);
    }











    // Start is called before the first frame update
    void Start()
    {
        is_moving = false;
        is_attack = false; 
        is_defense = false;
        check = true;
        check_move = true;
        target = new Vector2();
        move_set();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance().Is_player_turn())
        {
            if (check)
            {
                Debug.Log("start");
                check = false;
            }


            if (is_moving)
            {


                if(check_move)
                {
                    target_ = target;
                    check_move = false;
                }
                transform.position = Vector3.MoveTowards(transform.position, target_, 0.1f);
                if(target_.x == transform.position.x && target_.x == transform.position.x)
                {
                    Debug.Log("end");
                    check_move = true;
                    check = true;

                    Destroy(GameObject.Find("µµÂøÁöÁ¡(Clone)"));

                    GameManager.Instance().turn_end();
                }



            }
            else if (is_attack)
            {

            }
            else if (is_defense)
            {

            }
        }
    }
}
