using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow_turn_bar : MonoBehaviour
{
    private bool is_move = false;
    private Vector2 target;
    // Start is called before the first frame update

    public void init()
    {
        turn_bar_manager turn_Bar = transform.parent.GetComponent<turn_bar_manager>();
        TurnManager turnManager = TurnManager.Instance;
        target = turn_Bar.turn_bars[turnManager.Number_of_turns_performed - 1].GetComponent<RectTransform>().anchoredPosition - new Vector2(0, 32);
        this.GetComponent<RectTransform>().anchoredPosition=target;
    }

    public void move_arrow()
    {
        turn_bar_manager turn_Bar = transform.parent.GetComponent<turn_bar_manager>();
        TurnManager turnManager = TurnManager.Instance;
        target = turn_Bar.turn_bars[turnManager.Number_of_turns_performed-1].GetComponent<RectTransform>().anchoredPosition - new Vector2(0,32);
        StartCoroutine("Moving");
    }

    IEnumerator Moving()
    {
       
        while (target != this.GetComponent<RectTransform>().anchoredPosition)
        {

            this.GetComponent<RectTransform>().anchoredPosition = Vector3.MoveTowards(this.GetComponent<RectTransform>().anchoredPosition, target, 0.8f);
            yield return new WaitForFixedUpdate();
        }
    }
    public bool is_moving()
    {
        return is_move;
    }
    void Start()
    {
    }
    void FixedUpdate()
    {

        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
