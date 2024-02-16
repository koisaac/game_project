using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemys_manager : MonoBehaviour
{
    private static enemys_manager instance = null;
    void Awake()
    {
        if (instance == null)
        {

            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);
    }
    public static enemys_manager Instance { get => instance; }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
