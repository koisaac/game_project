using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


struct move_panel_info
{
    public float x;
    public float y;
    public int direction_x;
    public int direction_y;

    public move_panel_info(float x, float y, int direction_x, int direction_y)
    {
        this.x = x;
        this.y = y;
        this.direction_x = direction_x;
        this.direction_y = direction_y;
    }
    public void MoveLeft()
    {
        this.x -= 1;
        direction_x = -1;
        direction_y = 0;
    }
    public void MoveRight()
    {
        this.x += 1;
        direction_x = 1;
        direction_y = 0;
    }
    public void MoveUp()
    {
        this.y += 1;
        direction_x = 0;
        direction_y = 1;
    }
    public void MoveDown()
    {
        this.y -= 1;
        direction_x = 0;
        direction_y = -1;
    }
    public move_panel_info Get_Panel_Info()
    {
        move_panel_info info = new move_panel_info(this.x, this.y, this.direction_x, this.direction_y);
        return info;
    }
}




public class move_controller : MonoBehaviour
{
    public GameObject prefab;
    public GameObject end_point;

    private move_panel_info panel_info;
    private List<move_panel_info> move_panel_infos;
    private List<GameObject> panels;
    private int move_conut;

    //디버그용 변수
    private bool did_start_moving = false;

    public List<GameObject> Panels { get => panels; }
    internal List<move_panel_info> Move_panel_infos { get => move_panel_infos;}


    public void ReplacePaneltoEndPoint()
    {
        GameObject g = Instantiate(end_point); 
        g.transform.position = panels[panels.Count - 1].transform.position;
        Destroy(panels[panels.Count - 1].gameObject);
        panels[panels.Count - 1] = g;
    }




    public void EndMove()
    {
        foreach(GameObject g in panels)
        {
            Destroy(g);
        }
        panels.Clear();
        move_panel_infos.Clear();
        did_start_moving = false;
    }

    public void MoveStart(int speed)
    {
        this.transform.localPosition =new Vector3(0,0,0);
        did_start_moving = true;
        Vector2 player_site = transform.parent.position;
        panel_info.x = 0;
        panel_info.y = 0;
        panel_info.direction_x = 0;
        panel_info.direction_y = 0;
        move_conut = speed;
        move_panel_infos.Add(panel_info);
    }

    private bool Move(string move_type, bool check_move)
    {
        bool is_end = false;
        bool check = true;
        if (!did_start_moving)
        {
            Debug.LogError($"Move_start 시행되지 않았는데 움직이기 코드 실행됨(move_controller.Move.{move_type})");
            return false;
        }

        GameObject @object = null;


        switch (move_type)
        {
            case ("Left"):
                if (panel_info.direction_x == 1)
                {
                    check = false;
                }
                else
                {
                    panel_info.MoveLeft();
                }
                break;
            case ("Right"):
                if (panel_info.direction_x == -1)
                {
                    check = false;
                }
                else
                {
                    panel_info.MoveRight();
                }
                break;
            case ("Up"):
                if (panel_info.direction_y == -1)
                {
                    check = false;
                }
                else
                {
                    panel_info.MoveUp();
                }
                break;
            case ("Down"):
                if (panel_info.direction_y == 1)
                {
                    check = false;
                }
                else
                {
                    panel_info.MoveDown();
                }
                break;
            default:
                Debug.LogError($"올바르지 않은 move_type move_type = {move_type}(move_controller.Move.{move_type}, move_cont = {move_conut})");
                break;
        }




        if (check && check_move)
        {
            move_conut -= 1;

            if (move_conut > 0)
            {
                @object = Instantiate(prefab);
                is_end = false;

            }
            else if (move_conut == 0)
            {
                @object = Instantiate(end_point);
                is_end = true;
            }
            else
            {
                Debug.LogError($"move_count 값 음수(move_controller.Move.{move_type}, move_cont = {move_conut})");
                return false;
            }

            @object.transform.parent = this.gameObject.transform;
            @object.transform.localPosition = new Vector3(panel_info.x * 14.52f, panel_info.y * 14.52f, this.transform.position.z);
            move_panel_infos.Add(panel_info.Get_Panel_Info());
            panels.Add(@object);


        }
        else if(!check)
        {
            move_conut++;
            is_end = false;

            panel_info = move_panel_infos[move_panel_infos.Count - 2].Get_Panel_Info();
            move_panel_infos.Remove(move_panel_infos[move_panel_infos.Count - 1]);

            Destroy(panels[panels.Count - 1].gameObject);
            panels.Remove(panels[panels.Count - 1]);

            if (move_panel_infos.Count == 1)
            {
                player.                Instance.EndMove();
                return false;
            }
        }
        else
        {
            panel_info = move_panel_infos[move_panel_infos.Count - 1].Get_Panel_Info();
            is_end = true;
        }
        return is_end;
    }




    public bool MoveLeft(bool check_move)
    {
        return Move("Left", check_move);
    }
    public bool MoveRight(bool check_move)
    {
        return Move("Right", check_move);
    }
    public bool MoveUp(bool check_move)
    {
        return Move("Up", check_move);
    }
    public bool MoveDown(bool check_move)
    {
        return Move("Down", check_move);
    }




    // Start is called before the first frame update
    void Start()
    {
        move_panel_infos = new List<move_panel_info>();
        panels = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
