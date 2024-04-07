using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Effect : MonoBehaviour
{
    // Start is called before the first frame update
    public int stack = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void Affect()
    {

    }
}
public class Poison : Effect
{
    
    public override void Affect()
    {

    }
    public Poison(int Stack)
    {
        stack += Stack;
    }
    void Update()
    {
        Debug.Log(stack);
    }
}
