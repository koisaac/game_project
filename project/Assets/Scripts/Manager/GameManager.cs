using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.Tilemaps;


public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;


    public List<BoxCollider2D> bounds;
    public int map_number;
    public GameObject button_script;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

    }


    void Start()
    {

    }


    public BoxCollider2D getbound()
    {
        return bounds[map_number];
    }

    public static GameManager Instance {get=> instance;}

}
