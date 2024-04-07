using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

struct attack_panel_info
{
    public int direction_x;
    public int direction_y;


    public attack_panel_info(int direction_x, int direction_y)
    {
        this.direction_x = direction_x;
        this.direction_y = direction_y;
    }

    public void AttackUp()
    {
        direction_x = 0;
        direction_y = 1;
    }

    public void AttackDown()
    {
        direction_x = 0;
        direction_y = -1;
    }

    public void AttackLeft()
    {
        direction_x = -1;
        direction_y = 0;
    }

    public void AttackRight()
    {
        direction_x = 1;
        direction_y = 0;
    }

    public attack_panel_info Get_Panel_Info()
    {
        attack_panel_info info = new attack_panel_info(this.direction_x, this.direction_y);
        return info;
    }
}
public class attack_controller : MonoBehaviour
{
    public GameObject prefab;

    private attack_panel_info panel_info;
    private List<attack_panel_info> attack_panel_infos;
    private List<GameObject> panels;
    private int r_count;
    private int l_count;

    //디버그용 변수였지만 이제 지우면 안됨
    private bool did_start_attack = false;

    private bool is_confirmed = false;
    public List<GameObject> Panels { get => panels; }
    internal List<attack_panel_info> Attack_panel_infos { get => attack_panel_infos; }

    public Damage attack;

    public void EndAttack()
    {
        did_start_attack = false;
    }

    public void Remove_Player_Attack_set()
    {
        foreach (GameObject g in panels)
        {
            if(!did_start_attack) //endattack 시행, 즉 공격 확정 여부를 나타냄
            {
                g.GetComponent<Attack_damager>().tile_attack = attack;
            }
            Destroy(g);

            attack = null;
        }
        panels.Clear();
        attack_panel_infos.Clear();
    }
    public void AttackStart(int range, int length)
    {
        this.transform.localPosition = new Vector3(0, 0, 0);
        did_start_attack = true;
        Vector2 player_site = transform.parent.position;
        panel_info.direction_x = 0;
        panel_info.direction_y = 0;
        r_count = range;
        l_count = length;
        attack_panel_infos.Add(panel_info);
    }

    private bool Attack(string attack_type, bool check_attack)
    {

        bool is_end = false;
        bool check = true;

        if (!did_start_attack)
        {
            Debug.LogError($"Attack_start 시행되지 않았는데 공격 코드 실행됨(attack_controller.Attack.{attack_type})");
            return false;
        }

        GameObject @object = null;

        switch (attack_type)
        {
            case ("Left"):
                if (panel_info.direction_x == -1)
                {
                    check = false;
                }
                else
                {
                    panel_info.AttackLeft();
                }
                break;
            case ("Right"):
                if (panel_info.direction_x == 1)
                {
                    check = false;
                }
                else
                {
                    panel_info.AttackRight();
                }
                break;
            case ("Up"):
                if (panel_info.direction_y == 1)
                {
                    check = false;
                }
                else
                {
                    panel_info.AttackUp();
                }
                break;
            case ("Down"):
                if (panel_info.direction_y == -1)
                {
                    check = false;
                }
                else
                {
                    panel_info.AttackDown();
                }
                break;
            default:
                Debug.LogError($"올바르지 않은 attack_type attack_type = {attack_type}(attack_controller.Attack.{attack_type})");
                break;
        }

        if(check && check_attack)
        {
            Remove_Player_Attack_set();

            if(r_count > 1)
            {
                if (panel_info.direction_y != 0)
                {
                    for (int i = 0; i < r_count; i++)
                    {
                        for (int j = 0; j < l_count; j++)
                        {
                            @object = Instantiate(prefab);
                            @object.transform.parent = this.gameObject.transform;
                            @object.transform.localPosition = new Vector3(panel_info.direction_x * (i + 1) * 14.52f, panel_info.direction_y * (j + 1) * 14.52f, this.transform.position.z);
                            attack_panel_infos.Add(panel_info.Get_Panel_Info());
                            panels.Add(@object);
                        }
                    }
                }

                else if (panel_info.direction_x != 0)
                {
                    for (int i = 0; i < l_count; i++)
                    {
                        for (int j = 0; j < r_count; j++)
                        {
                            @object = Instantiate(prefab);
                            @object.transform.parent = this.gameObject.transform;
                            @object.transform.localPosition = new Vector3(panel_info.direction_x * (i + 1) * 14.52f, panel_info.direction_y * (j + 1) * 14.52f, this.transform.position.z);
                            attack_panel_infos.Add(panel_info.Get_Panel_Info());
                            panels.Add(@object);
                        }
                    }
                }
            }

            else if(r_count == 1)
            {
                if (panel_info.direction_y != 0)
                {
                    for (int i = 0; i < r_count; i++)
                    {
                        for (int j = 0; j < l_count; j++)
                        {
                            @object = Instantiate(prefab);
                            @object.transform.parent = this.gameObject.transform;
                            @object.transform.localPosition = new Vector3(panel_info.direction_x * (i + 1) * 14.52f, panel_info.direction_y * (j + 1) * 14.52f, this.transform.position.z);
                            attack_panel_infos.Add(panel_info.Get_Panel_Info());
                            panels.Add(@object);
                        }
                    }
                }

                else if (panel_info.direction_x != 0)
                {
                    for (int i = 0; i < l_count; i++)
                    {
                        for (int j = 0; j < r_count; j++)
                        {
                            @object = Instantiate(prefab);
                            @object.transform.parent = this.gameObject.transform;
                            @object.transform.localPosition = new Vector3(panel_info.direction_x * (i + 1) * 14.52f, panel_info.direction_y * (j + 1) * 14.52f, this.transform.position.z);
                            attack_panel_infos.Add(panel_info.Get_Panel_Info());
                            panels.Add(@object);
                        }
                    }
                }
            }

            else
            {
                Debug.LogError($"올바르지 않은 range range = {r_count}(attack_controller.Attack.{r_count})");
            }
            is_end = true;
        }


        return is_end;
    }

    public bool AttackLeft(bool check_attack)
    {
        return Attack("Left", check_attack);
    }
    public bool AttackRight(bool check_attack)
    {
        return Attack("Right", check_attack);
    }
    public bool AttackUp(bool check_attack)
    {
        return Attack("Up", check_attack);
    }
    public bool AttackDown(bool check_attack)
    {
        return Attack("Down", check_attack);
    }
    // Start is called before the first frame updatez
    void Start()
    {
        attack_panel_infos = new List<attack_panel_info>();
        panels = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}