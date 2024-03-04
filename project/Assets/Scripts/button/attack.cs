using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
    public GameObject attack_button;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void Attack()
    {
        if (!player.Instance.Is_player_attack)
        {
            if (!player.Instance.Is_attack)
            {
                player.Instance.StartAttack();
                attack_button.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = "취소";
            }
            else
            {
                player.Instance.EndAttack();
                attack_button.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = "공격";

            }

        }
    }
    // Update is called once per frame
    void Update()
    {
        if (player.Instance.Is_attack)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Attack();
            }
            attack_button.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = "취소";
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                Attack();
            }
            attack_button.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = "공격";

        }
    }
}
