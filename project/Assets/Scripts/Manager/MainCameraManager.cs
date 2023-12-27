using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraManager : MonoBehaviour
{

    private static MainCameraManager instance;

    private Vector3 max_bound;
    private Vector3 min_bound;
    private float camera_width;
    private float camera_height;

    private BoxCollider2D bound;
    private float move_x;
    private float move_y;

    public float shake_degree;

    public static MainCameraManager Instance { get => instance;  }

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void shake_left()
    {
        transform.Translate(new Vector3(-shake_degree, 0, 0));
    }
    public void shake_right()
    {
        transform.Translate(new Vector3(shake_degree, 0, 0));
    }
    public void shake_up()
    {
        transform.Translate(new Vector3(0, shake_degree, 0));
    }
    public void shake_down()
    {
        transform.Translate(new Vector3(0, -shake_degree, 0));
    }
    // Start is called before the first frame update
    void Start()
    {
        bound = GameManager.Instance().getbound();
        camera_height = Camera.main.orthographicSize;
        camera_width = camera_height * Screen.width / Screen.height;

    }


    // Update is called once per frame
    void Update()
    {
        Vector3 target = player.Instance().transform.position;
        if (player.Instance().Is_moving && !player.Instance().Is_player_move)
        {
            move_controller Move_Controller = player.Instance().Move_panels;
            if (Move_Controller.Panels.Count != 0)
            {
                target = Move_Controller.Panels[Move_Controller.Panels.Count - 1].transform.position;

            }
        }

        bound = GameManager.Instance().getbound();


        max_bound = bound.bounds.max;
        min_bound = bound.bounds.min;

        move_x = Mathf.Clamp(target.x, min_bound.x + camera_width - 43, max_bound.x - camera_width + 43);
        move_y = Mathf.Clamp(target.y, min_bound.y + camera_height - 34.2f, max_bound.y - camera_height);
        Vector3 target_postion = new Vector3(move_x, move_y, this.transform.position.z);


        transform.position = Vector3.Lerp(transform.position, target_postion, 0.03f);



    }
}
