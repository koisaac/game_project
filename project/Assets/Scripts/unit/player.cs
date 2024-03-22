using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class player : MonoBehaviour
{
    private int unit_number;
    public void SetUnitNumber(int unit_number)
    {
        this.unit_number = unit_number;
    }

    public int unit_spped;

    public int move_speed;

    int attack_range = 1;

    int attack_length = 3;

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


    public static player Instance { get => Player; }





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
    private bool check_attack;
    private bool is_panel_move_end;
    private bool is_player_move;
    private bool is_player_attack;


    private float time;

    private attack_controller attack_panels;
    private move_controller move_panels;

    private BoxCollider2D bound;
    private Vector3 max_bound, min_bound;
    private Vector3 Current_panel_position;

    public bool Is_moving { get => is_moving; }
    public bool Is_player_move { get => is_player_move; }
    public bool Is_player_attack { get => is_player_attack; }
    public bool Is_attack { get => is_attack; }
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
        check_attack = true;
        is_panel_move_end = true;
        is_player_move = false;
        is_player_attack = false;

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
        is_player_attack = false;

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

    public void StartAttack()
    {
        is_moving = false;
        is_attack = true;
        is_defense = false;
        is_using_skill = false;

        is_panel_move_end = true;
        is_player_move = false;
        is_player_attack = false;

        origin_postion = transform.position;
        attack_panels.AttackStart(attack_range, attack_length);
    }

    public void EndAttack()
    {
        End();

        attack_panels.EndAttack();
    }

    private bool check_can_attack(Vector3 bound, int dic_x, int dic_y)
    {
        bool can_attack = true;


        if (dic_x == 0)
        {
            if (Current_panel_position.y * dic_y + (14.52f) > dic_y * bound.y)
            {
                can_attack = false;
            }
        }
        else if (dic_y == 0)
        {
            if (Current_panel_position.x * dic_x + (14.52f) > dic_x * bound.x)
            {
                can_attack = false;
            }
        }

        return can_attack;
    }

    private bool check_can_attack_down()
    {
        return check_can_attack(min_bound, 0, -1);
    }


    private bool check_can_attack_up()
    {
        return check_can_attack(max_bound, 0, 1);
    }


    private bool check_can_attack_right()
    {
        return check_can_attack(max_bound, 1, 0);
    }

    private bool check_can_attack_left()
    {
        return check_can_attack(min_bound, -1, 0);
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
        is_player_attack = false;

        time = 0;

        attack_panels = transform.Find("attack_panels").GetComponent<attack_controller>();
        move_panels = transform.Find("move_panels").GetComponent<move_controller>();
    }



    // Update is called once per frame

    void FixedUpdate()
    {
        if (TurnManager.Instance.Is_player_turn())
        {
            if (check)
            {
                Debug.Log("player_turn, turn : "+TurnManager.Instance.Number_of_turns_performed.ToString());
                check_move = true;
                check = false;
                attack_panels.Remove_Player_Attack_set();
            }

            if (is_moving)
            {
                if (!is_player_move)
                {
                    attack_panels.Remove_Player_Attack_set();
                    if (is_panel_move_end)
                    {
                        bound = GameManager.Instance.getbound();
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
                                MainCameraManager.Instance.shake_down();
                            }
                            is_panel_move_end = false;
                            time = 0;
                        }
                        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) //move up
                        {
                            if (check_can_move_up())
                            {

                                check_move = !move_panels.MoveUp(check_move);
                            }
                            else
                            {
                                MainCameraManager.Instance.shake_up();
                            }
                            is_panel_move_end = false;
                            time = 0;
                        }
                        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) //move_right
                        {
                            if (check_can_move_right())
                            {
                                check_move = !move_panels.MoveRight(check_move);
                            }
                            else
                            {
                                MainCameraManager.Instance.shake_right();
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
                                MainCameraManager.Instance.shake_left();

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
                        TurnManager.Instance.turn_end();
                    }
                }





            }
            else if (is_attack)
            {
                if (!is_player_attack)
                {
                    bound = GameManager.Instance.getbound();
                    max_bound = bound.bounds.max;
                    min_bound = bound.bounds.min;

                    if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) //attack down
                    {
                        Debug.Log(check_can_attack_down());
                        if (check_can_attack_down())
                        {
                            check_attack = !attack_panels.AttackDown(check_attack);
                        }

                        time = 0;
                    }
                    else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) //attack up
                    {
                        if (check_can_attack_up())
                        {
                            check_attack = !attack_panels.AttackUp(check_attack);
                        }

                        time = 0;
                    }
                    else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) //attack_right
                    {
                        if (check_can_attack_right())
                        {
                            check_attack = !attack_panels.AttackRight(check_attack);
                        }

                        time = 0;
                    }
                    else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) //attack_left
                    {
                        Debug.Log(check_can_attack_left());
                        if (check_can_attack_left())
                        {
                            check_attack = !attack_panels.AttackLeft(check_attack);
                        }

                        time = 0;
                    }

                    if (Input.GetKey(KeyCode.F))
                    {
                        attack_panels.EndAttack();
                        is_player_attack = true;
                    }
                }

                else
                {
                    EndAttack();
                    TurnManager.Instance.turn_end();
                }
            }
            else if (is_defense)
            {

            }
        }
    }
}
