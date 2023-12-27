using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activate : MonoBehaviour
{
    // Start is called before the first frame update
    private bool click_attack;

    public void click_attack_false()
    {
        this.click_attack = false;
    }

    public void attack()
    {
        if (!click_attack)
        {
            player.Instance().transform.Find("attack_panels").GetComponent<set_player_attack_sets>().activate_attack_set();
            click_attack = true;
        }

    }
    public void move()
    {
        if(!player.Instance().Is_moving)
        {
            player.Instance().StartMove();
            gameObject.transform.Find("��ư").transform.Find("�̵� ��ư").transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = "���";
        }
        else
        {
            player.Instance().EndMove();
            gameObject.transform.Find("��ư").transform.Find("�̵� ��ư").transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = "�̵�";

        }
    }

    void Start()
    {
        click_attack = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (player.Instance().Is_moving)
        {
            gameObject.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = "���";
        }
        else
        {
            gameObject.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = "�̵�";
        }
    }
}
