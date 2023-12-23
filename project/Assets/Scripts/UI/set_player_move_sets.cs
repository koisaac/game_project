using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class set_player_move_sets : MonoBehaviour
{
    public GameObject prefab;
    private int childcount;
    public void deactivate()
    {
        for(int i = 0; i < childcount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    public void activate()
    {
        for (int i = 0; i < childcount; i++)
        {
            Transform @object = transform.GetChild(i);
            if (Are_the_coordinates_within_range(@object.position, GameManager.Instance().getbound()))
            {
                @object.gameObject.SetActive(true);
            }
        }
    }
    private bool Is_the_variable_in_scope(float value,float min,float max)
    {
        if(value>min && value<max) return true;
        else return false;
    }
    private bool Are_the_coordinates_within_range(Vector2 valaue,BoxCollider2D bound)
    {
        if(Is_the_variable_in_scope(valaue.x,bound.bounds.min.x,bound.bounds.max.x) && Is_the_variable_in_scope(valaue.y, bound.bounds.min.y, bound.bounds.max.y))
        {
            return true;
        }
        else return false;
    }
    private void generate(float x,float y)
    {
        GameObject object_ = Instantiate(prefab, gameObject.transform);
        object_.transform.localPosition = new Vector3(x, y, transform.position.z);
    }

    public void set_player_move_set(int move_range)
    {
        BoxCollider2D bound = GameManager.Instance().getbound();
        for (int i = 0; i < move_range; i++)
        {
            for (int j = -i;j<=i;j++)
            {
    
                generate(j * 14.52f, (move_range - i) * 14.52f);
                generate(j * 14.52f, -(move_range - i) * 14.52f);
            }
            generate((i + 1) * 14.52f, 0);
            generate(-(i + 1) * 14.52f, 0);
            
        }
        childcount = transform.childCount;
        deactivate();

        

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
