using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraManager : MonoBehaviour
{
    public GameObject player;

    private Vector3 max_bound;
    private Vector3 min_bound;
    private float camera_width;
    private float camera_height;

    private BoxCollider2D bound;
    private float move_x;
    private float move_y;

    public void shake_left()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        bound = GameManager.Instance().getbound();
        camera_height = Camera.main.orthographicSize;
        camera_width = camera_height*Screen.width/Screen.height;

    }


    // Update is called once per frame
    void Update()
    {
        bound = GameManager.Instance().getbound();


        max_bound = bound.bounds.max;
        min_bound = bound.bounds.min;

       

        move_x = Mathf.Clamp(player.transform.position.x, min_bound.x+camera_width-44, max_bound.x-camera_width+44);
        move_y = Mathf.Clamp(player.transform.position.y, min_bound.y+camera_height-47, max_bound.y-camera_height);


        Vector3 target_postion=new Vector3(move_x,move_y,this.transform.position.z) ;

        transform.position = Vector3.MoveTowards(transform.position, target_postion, 0.3f);
    }  
}
