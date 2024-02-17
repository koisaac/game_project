using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;


public class turn_bar : MonoBehaviour
    , IPointerEnterHandler
    , IPointerExitHandler
{
    private int unit_number;
    private string unit_name;
    private Transform turn_bar_info;
    public int Unit_number {set => unit_number = value;}
    public string Unit_name { set => unit_name = value; }


    public TMP_Text tmp;
    public void init()
    {        turn_bar_info = transform.Find("turn_bar_info");

        turn_bar_info.gameObject.SetActive(true);
        tmp.text = unit_name;

        Transform image = turn_bar_info.Find("Image");

        turn_bar_info.gameObject.SetActive(false);


    }


    public void OnPointerEnter(PointerEventData eventData)
    {   
        Debug.Log("a"); 
        turn_bar_info.gameObject.SetActive(true);

        StartCoroutine("delay");       

    }
    IEnumerator delay()
    {
        Transform image = turn_bar_info.Find("Image");
        while (image.GetComponent<RectTransform>().rect.width == 0)
        {
            yield return null;
        }
        turn_bar_info.GetComponent<RectTransform>().anchoredPosition = new Vector2(-(15 + image.GetComponent<RectTransform>().rect.width / 2f), -28);

    }


    public void OnPointerExit(PointerEventData eventData)
    {
        turn_bar_info.gameObject.SetActive(false);
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
