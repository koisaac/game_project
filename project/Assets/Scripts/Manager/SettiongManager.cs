using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.CompilerServices;

[System.Serializable]
public class Turn_Bar_Setting
{
    public int[][] type_of_color;
}

public class SettiongManager : MonoBehaviour
{
    private static SettiongManager instance;

    public string player_setting_path;
    public string turn_bar_setting_path;

    public static SettiongManager Instance { get => instance; }

    void Awake()
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

    public string GetFile_string(string path)
    {
        Debug.Log(path);
        return File.ReadAllText(path);
    }
    public Turn_Bar_Setting TurnBarSetting()
    {
        return JsonConvert.DeserializeObject<Turn_Bar_Setting>(GetFile_string(turn_bar_setting_path));
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
