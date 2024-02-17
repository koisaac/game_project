using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class turn_bar_manager : MonoBehaviour
{

    public List<GameObject> turn_bars;
    public GameObject prefab;
    public float panel_length;
    private List<int> unit_action_turn_bar_state;




    // Start is called before the first frame update
    public void init()
    {
        unit_action_turn_bar_state = Enumerable.Repeat(1,TurnManager.Instance.Unit_speed.Count).ToList();
        float ratio = panel_length/(float)TurnManager.Instance.Unit_speed_Total_product;
        Turn_Bar_Setting setting = SettiongManager.Instance.TurnBarSetting();

        int turn_bars_count = turn_bars.Count;
        if (turn_bars.Count < TurnManager.Instance.Show_action_number)
        {
            for(int i = 0;i< TurnManager.Instance.Show_action_number - turn_bars_count; i++)
            {

                turn_bars.Add(Instantiate(prefab));

                turn_bars[turn_bars.Count - 1].transform.SetParent(this.gameObject.transform,false);
            }
        }
        for(int i = 0;i< turn_bars.Count; i++)
        {
            int action_unit_number;
            if (i< TurnManager.Instance.Show_action_number)
            {
                action_unit_number = TurnManager.Instance.Unit_action_procedure[i];
                
                turn_bars[i].SetActive(true);
                turn_bars[i].GetComponent<turn_bar>().Unit_number = action_unit_number;
                turn_bars[i].GetComponent<turn_bar>().Unit_name = TurnManager.Instance.Unit[action_unit_number].name; 
                turn_bars[i].GetComponent<Image>().color = new Color(setting.type_of_color[action_unit_number][0], setting.type_of_color[action_unit_number][1], setting.type_of_color[action_unit_number][2]);
                turn_bars[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(-(panel_length / 2) + ratio*TurnManager.Instance.Unit_speed[action_unit_number] * unit_action_turn_bar_state[action_unit_number], 0, 0);
                turn_bars[i].GetComponent<turn_bar>().init();
                unit_action_turn_bar_state[action_unit_number]++;
            }
        }
    }


    void Start()
    {
        turn_bars = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
