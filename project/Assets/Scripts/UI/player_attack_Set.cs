using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_attack_Set : MonoBehaviour
{
    public GameObject prefab;
    void OnMouseDown()
    {
        Debug.Log(transform.name);
        player player_instance=player.Instance();
        
        GameObject a = Instantiate(prefab);
        a.transform.position=transform.position;
        
        transform.parent.GetComponent<set_player_attack_sets>().deactivate_attack_set();


    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
