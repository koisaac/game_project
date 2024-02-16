using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct attack_panel_info
{
    public int x;
    public int y;
    public int direction_x;
    public int direction_y;


    public attack_panel_info(int x, int y, int direction_x, int direction_y)
    {
        this.x = x;
        this.y = y;
        this.direction_x = direction_x;
        this.direction_y = direction_y;
    }
    
    public void AttackLeft()
    {
        Quaternion angleQ = Quaternion.Euler(0, 90, 0);
    }

    public void AttackRight() 
    {
        Quaternion angleQ = Quaternion.Euler(0, -90, 0);
    }
}
public class attact_controller : MonoBehaviour
{
    public GameObject prefab;
    public GameObject player_attack_set;
    private attack_panel_info panel_info;
    private List<attack_panel_info> attack_panel_infos;
    private List<GameObject> panels;

    private bool did_start_attack = false;

    public List<GameObject> Panels { get => panels; }
    internal List<attack_panel_info> Attack_panel_infos { get => attack_panel_infos; }

    public void MoveStart()
    {
        this.transform.localPosition = new Vector3(0, 0, 0);
        did_start_attack = true;
        Vector2 player_site = transform.parent.position;
        panel_info.x = 0;
        panel_info.y = 0;
        panel_info.direction_x = 0;
        panel_info.direction_y = 0;
        attack_panel_infos.Add(panel_info);
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
