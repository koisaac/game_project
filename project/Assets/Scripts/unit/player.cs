using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class player : MonoBehaviour
{
    public int move_speed;

    const int move_range_size = 3;

    private static player Player = null;


    void Awake()
    {
        if (Player == null)
        {

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
    private bool is_using_skill;
    private bool check;
    private bool check_move;
    private bool is_panel_move_end;
    private bool is_player_move;


    private float time;

    private set_player_attack_sets attack_panels;
    private move_controller move_panels;

    private BoxCollider2D bound;
    private Vector3 max_bound, min_bound;
    private Vector3 Current_panel_position;

    public bool Is_moving { get => is_moving; }
    public bool Is_player_move { get => is_player_move; }
    public move_controller Move_panels { get => move_panels;}

    private Vector2 origin_postion;
    private void End()
    {
        is_moving = false;
        is_attack = false;
        is_defense = false;
        is_using_skill = false;

        check = true;
        check_move = true;
        is_panel_move_end = true;
        is_player_move = false;

        time = 0;
    }

    public void StartMove()
    {

        is_moving = true;
        is_attack = false;
        is_defense = false;
        is_using_skill = false;

        is_panel_move_end = true;
        is_player_move = false;

        origin_postion = transform.position;
        move_panels.MoveStart(move_speed);
    }
    public void EndMove()
    {
        End();

        move_panels.EndMove();

    }

    private bool check_can_move(Vector3 bound,int dic_x,int dic_y)
    {
        bool can_move = true;


        if(dic_x == 0)
        {
            if(Current_panel_position.y*dic_y + (14.52f ) >  dic_y* bound.y)
            {
                can_move = false;
            }
        }
        else if(dic_y == 0)
        {
            if (Current_panel_position.x * dic_x + (14.52f) > dic_x * bound.x)
            {
                can_move = false;
            }
        }

        return can_move;



    }

    private bool check_can_move_down()
    {
        return check_can_move(min_bound,0,-1);
    }


    private bool check_can_move_up()
    {
        return check_can_move(max_bound,0,1);
    }


    private bool check_can_move_right()
    {
        return check_can_move(max_bound,1,0);
    }

    private bool check_can_move_left()
    {
        return check_can_move(min_bound, -1, 0);
    }



    // Start is called before the first frame update
    void Start()
    {
        is_moving = false;
        is_attack = false;
        is_defense = false;
        check = true;
        check_move = true;
        is_panel_move_end = true;
        is_player_move = false;

        time = 0;

        attack_panels = transform.Find("attack_panels").GetComponent<set_player_attack_sets>();
        move_panels = transform.Find("move_panels").GetComponent<move_controller>();
    }



    // Update is called once per frame

    void FixedUpdate()
    {
        if (GameManager.Instance().Is_player_turn())
        {
            if (check)
            {
                Debug.Log("start");
                check_move = true;
                check = false;
            }


            if (is_moving)
            {
                if (!is_player_move)
                {
                    if (is_panel_move_end)
                    {
                        bound = GameManager.Instance().getbound();
                        max_bound = bound.bounds.max;
                        min_bound = bound.bounds.min;
                        if (move_panels.Panels.Count == 0)
                        {
                            Current_panel_position = transform.position;
                        }
                        else
                        {
                            Current_panel_position = move_panels.Panels[move_panels.Panels.Count - 1].transform.position;
                        }
                        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) //move down
                        {
                            Debug.Log(check_can_move_down());
                            if (check_can_move_down())
                            {

                                check_move = !move_panels.MoveDown(check_move);
                            }
                            else
                            {

                            }
                            is_panel_move_end = false;
                            time = 0;
                        }
                        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) //move up
                        {
                            Debug.Log(check_can_move_up());
                            if (check_can_move_up())
                            {

                                check_move = !move_panels.MoveUp(check_move);
                            }
                            else
                            {
                            }
                            is_panel_move_end = false;
                            time = 0;
                        }
                        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) //move_right
                        {
                            Debug.Log(check_can_move_right());
                            if (check_can_move_right())
                            {
                                check_move = !move_panels.MoveRight(check_move);
                            }
                            else
                            {

                            }

                            is_panel_move_end = false;
                            time = 0;
                        }
                        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) //move_left
                        {
                            Debug.Log(check_can_move_left());
                            if (check_can_move_left())
                            {
                                check_move = !move_panels.MoveLeft(check_move);
                            }
                            else
                            {
                                

                            }
                            is_panel_move_end = false;
                            time = 0;
                        }

                    }

                    if (time > 0.2)
                    {
                        time = 0;
                        is_panel_move_end = true;
                    }

                    if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow) ||
                       Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow) ||
                       Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow) ||
                       Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
                    {
                        time = 0;
                        is_panel_move_end = true;
                    }

                    time += Time.deltaTime;



                    if (Input.GetKey(KeyCode.F) && move_panels.Panels.Count > 0)
                    {
                        Debug.Log("a");
                        move_panels.ReplacePaneltoEndPoint();
                        is_player_move = true;
                    }

                }
                else
                {


                    Vector2 target = move_panels.Panels[0].transform.position;

                    transform.position = Vector3.MoveTowards(transform.position, target, 0.8f);


                    move_panels.transform.position = origin_postion;

                    if (target.x == transform.position.x && target.y == transform.position.y)
                    {
                        Destroy(move_panels.Panels[0].gameObject);
                        move_panels.Panels.Remove(move_panels.Panels[0]);
                    }
                    if (move_panels.Panels.Count == 0)
                    {
                        EndMove();
                    }
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
