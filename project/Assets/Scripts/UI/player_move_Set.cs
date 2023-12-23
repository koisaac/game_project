using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_move_Set : MonoBehaviour
{
    public GameObject prefab;
    void OnMouseDown()
    {
        Debug.Log(transform.name);
        player player_instance=player.Instance();
        
        GameObject a = Instantiate(prefab);
        a.transform.position=transform.position;
        
        transform.parent.GetComponent<set_player_move_sets>().deactivate();


        player_instance.move(transform.position.x,transform.position.y);
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
