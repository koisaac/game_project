using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.Tilemaps;


public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public List<int> unit_speed;
    public List<int> unit_action_state;
    public int unit_speed_Total_product;
    public List<int> unit_action_procedure;
    private player Player;
    private GameObject Enemys;
    public int this_turn;
    public int number_of_turns_performed;

    public List<BoxCollider2D> bounds;
    public int map_number;
    public GameObject button_script;
    public int show_action_number;

    public int Number_of_turns_performed { get => number_of_turns_performed;}

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
    private int GetFirstActionUnitCheckPriority(List<int> action_unit)
    {
        //구현 필요
        return action_unit[0];
    }


    private List<int> GetFirstActionUnits()
    {
        int minValue = unit_action_state.Min();
        List<int> min_list = new List<int>();
        for(int i = 0;i< unit_action_state.Count; i++)
        {
            if(minValue == unit_action_state[i])
            {
                min_list.Add(i);
            }
        }
        return min_list;
    }


    private void InitUnitSpeed()
    {
        unit_speed = new List<int>();
        unit_action_state = new List<int>();
        unit_speed_Total_product = 1;


        Player.SetUnitNumber(0);
        unit_speed.Add(Player.unit_spped);
        unit_speed_Total_product *= Player.unit_spped;
        for (int i = 0; i < Enemys.transform.childCount; i++)
        {
            enemy Enemy = Enemys.transform.GetChild(i).GetComponent<enemy>();
            Enemy.SetUnitNumber(1 + i);
            unit_speed.Add(Enemy.unit_spped);
            unit_speed_Total_product *= Enemy.unit_spped;
        }

        for (int i = 0; i < unit_speed.Count; i++)
        {
            unit_speed[i] = unit_speed_Total_product / unit_speed[i];
            unit_action_state.Add(unit_speed[i]);
        }

    }
    public void Add_unit_action_procedure()
    {
        List<int> action_units = GetFirstActionUnits();
        int action_unit = GetFirstActionUnitCheckPriority(action_units);
        unit_action_state[action_unit] += unit_speed[action_unit];
        unit_action_procedure.Add(action_unit);
    }

    public void InitTurnSysterm()
    {
        unit_action_procedure = new List<int>();
        InitUnitSpeed();
        for(int i = 0;i<show_action_number;i++)
        {
            Add_unit_action_procedure();
        }
        number_of_turns_performed++;
        this_turn = unit_action_procedure[0];
    }


    void Start()
    {
        this.Player = player.Instance();
        this.Enemys = enemys_manager.Instance().gameObject;

        InitTurnSysterm();
        string s = "";
        foreach (var i in unit_action_procedure)
        {
            s += i.ToString() + ",";
        }
        if (unit_action_procedure[0] == 0) s += "player turn";
        else s += "enemy turn";
        Debug.Log(s);
 
    }


    public BoxCollider2D getbound()
    {
        return bounds[map_number];
    }

    public static GameManager Instance()
    {
        return instance;
    }
    public bool Is_this_unit_turn(int unit_number)
    {
        return(this_turn == unit_number);
    }
    public bool Is_player_turn()
    {
        return(this_turn == 0);  
    }
    public void turn_end() {
        unit_action_procedure.RemoveAt(0);
        this_turn = unit_action_procedure[0];
        Add_unit_action_procedure();
        string s = "";
        foreach (var i in unit_action_procedure)
        {
            s += i.ToString() + ",";
        }
        if (unit_action_procedure[0] == 0) s += "player turn";
        else s += "enemy turn";
        Debug.Log(s);
        
        number_of_turns_performed++;
    }

}
