using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemy;
    public void Button()
    {
        if (TurnManager.Instance.Is_this_unit_turn(enemy.GetComponent<enemy>().GetUnitNumber()))
        {
            TurnManager.Instance.turn_end();
        }
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
