using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    // Start is called before the first frame update
    private int unit_number;
    public void SetUnitNumber(int unit_number)
    {
        this.unit_number = unit_number;
    }
    public int unit_spped;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(TurnManager.Instance.Is_this_unit_turn(unit_number))
        {
            Debug.Log("enemy turn, turn :"+ TurnManager.Instance.Number_of_turns_performed.ToString());
            TurnManager.            Instance.turn_end();
        }
    }
}
