using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public GameObject move_button;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void Move()
    {
        if (!player.Instance.Is_player_move)
        {
            if (!player.Instance.Is_moving)
            {
                player.                Instance.StartMove();
                move_button.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = "취소";
            }
            else
            {
                player.                Instance.EndMove();
                move_button.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = "이동";

            }

        }
    }
    // Update is called once per frame
    void Update()
    {
        if (player.Instance.Is_moving)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Move();
            }
            move_button.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = "취소";
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Move();
            }
            move_button.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = "이동";

        }
    }
}
