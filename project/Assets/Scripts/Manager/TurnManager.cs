using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private static TurnManager instance = null;

    // Start is called before the first frame update
    private List<int> unit_speed;
    private List<int> unit_action_state;
    private int unit_speed_Total_product;
    private List<int> unit_action_procedure;
    private List<GameObject> unit;
    private player Player;
    private GameObject Enemys;
    private int this_turn;
    private int number_of_turns_performed = 0;
    private int show_action_number;

    private const int error_number = -2;
    private const int round_end = -1;
    private const int player_turn = 0;


    public int Number_of_turns_performed { get => number_of_turns_performed; }
    public int Show_action_number { get => show_action_number;}
    public static TurnManager Instance { get => instance;}
    public List<int> Unit_speed { get => unit_speed;}
    public int Unit_speed_Total_product { get => unit_speed_Total_product;}
    public List<GameObject> Unit { get => unit;}
    public List<int> Unit_action_procedure { get => unit_action_procedure;}


    public GameObject turn_bar;

    void Awake()
    {
        if (instance == null)
        {

            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);
    }
    private int GetFirstActionUnitCheckPriority(List<int> action_unit)
    {
        //구현 필요

        if(action_unit.Count == unit_action_state.Count)
        {
            return round_end;
        }
        else if(action_unit.Count > 1)
        {
            //구현 필요;
        }
        else if(action_unit.Count == 1)
        {
            return action_unit[0];
        }
        else if(action_unit.Count == 0)
        {
            Debug.LogError("action_unit이 빈 배열임.");
            return error_number;
        }
            Debug.LogError($"GetFirstActionUnitCheckPriority함수에서 오류 {string.Join(",", action_unit)}");
            return error_number; 
    }


    private List<int> GetFirstActionUnits()
    {
        int minValue = unit_action_state.Min();
        List<int> min_list = new List<int>();
        for (int i = 0; i < unit_action_state.Count; i++)
        {
            if (minValue == unit_action_state[i])
            {
                min_list.Add(i);
            }
        }
        return min_list;
    }


    private void InitUnitSpeed()
    {
        unit = new List<GameObject>();
        unit_speed = new List<int>();
        unit_action_state = new List<int>();
        unit_speed_Total_product = 1;
        show_action_number = 0;

        Player.SetUnitNumber(0);

        unit_speed.Add(Player.unit_spped);
        unit.Add(Player.gameObject);

        unit_speed_Total_product *= Player.unit_spped;
        show_action_number += Player.unit_spped - 1;
        for (int i = 0; i < Enemys.transform.childCount; i++)
        {
            enemy Enemy = Enemys.transform.GetChild(i).GetComponent<enemy>();
            Enemy.SetUnitNumber(1 + i);

            unit_speed.Add(Enemy.unit_spped);
            unit.Add(Enemy.gameObject);

            unit_speed_Total_product *= Enemy.unit_spped;
            show_action_number += Enemy.unit_spped - 1;
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
        if(action_unit >=0)
            unit_action_state[action_unit] += unit_speed[action_unit];
        unit_action_procedure.Add(action_unit);
    }

    public void InitTurnSysterm()
    {



        unit_action_procedure = new List<int>();
        InitUnitSpeed();
        do
        {
            Add_unit_action_procedure();
        } while (unit_action_procedure[unit_action_procedure.Count-1] != round_end);

        number_of_turns_performed++;
        this_turn = unit_action_procedure[0];

        string s = "";
        foreach (var i in unit_action_procedure)
        {
            s += i.ToString() + ",";
        }
  


        if (unit_action_procedure[0] == player_turn) s += "player turn";
        else if (unit_action_procedure[0] == round_end) s += "turn end";
        else if (unit_action_procedure[0] == error_number) s += "erro";
        else s += "enemy turn";
        Debug.Log(s);     
        turn_bar.GetComponent<turn_bar_manager>().init();
        turn_bar.transform.Find("화살표").GetComponent<arrow_turn_bar>().init();
    }



    void Start()
    {
        this.Player = player.Instance;
        this.Enemys = enemys_manager.Instance.gameObject;
        number_of_turns_performed = 0;
        InitTurnSysterm();

    }

    public bool Is_this_unit_turn(int unit_number)
    {
        return (this_turn == unit_number);
    }
    public bool Is_player_turn()
    {
        return (this_turn == player_turn);
    }
    public void turn_end()
    {
        unit_action_procedure.RemoveAt(0);
        this_turn = unit_action_procedure[0];
        string s = "";
        foreach (var i in unit_action_procedure)
        {
            s += i.ToString() + ",";
        }
        if (unit_action_procedure[0] == player_turn) s += "player turn";
        else if (unit_action_procedure[0] == round_end) s += "turn end";
        else if (unit_action_procedure[0] == error_number) s += "erro";
        else s += "enemy turn";
        Debug.Log(s);
        number_of_turns_performed++;
        if(unit_action_procedure[0] != round_end && unit_action_procedure[0] != error_number)
            turn_bar.transform.Find("화살표").GetComponent<arrow_turn_bar>().move_arrow();

    }

    void Update()
    {
        if(this_turn == round_end)
        {
            Debug.Log("라운드 종료");
            number_of_turns_performed = 0;
            InitTurnSysterm();
        }
    }

}
