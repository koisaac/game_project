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
        
        if (other.CompareTag("Attack")) //Attack과 Skill 데미지는 태그로 따로 구분할 예정. Attack은 일반공격만.
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
