using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Get_damage : MonoBehaviour
{
    bool attackHit = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerStay2D(Collider2D other)
    {
        
        if (other.CompareTag("Attack")) //Attack�� Skill �������� �±׷� ���� ������ ����. Attack�� �Ϲݰ��ݸ�.
        {
            Attack_damager gottenattack = other.gameObject.GetComponent<Attack_damager>();
            if (gottenattack.Hit)
            {
                Debug.Log("Targeted");
                if(!attackHit)
                {
                    Damage Comeattack;
                    Comeattack = gottenattack.tile_attack;
                    attackHit = true;
                    
                }
            }
            
        }
    }
    void OntriggerExit2D(Collider2D other)
    {
        Debug.Log(other.gameObject.tag);
    }
}
