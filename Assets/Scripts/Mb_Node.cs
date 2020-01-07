using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeFunction
{
    Input,
    Output
}

public enum NodeType
{
    Red,
    Green,
    Blue
}

public class Mb_Node : MonoBehaviour
{
    public NodeFunction function;
    public NodeType type;

    private bool drawLine = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (drawLine)
        {
            //DRAW LINE
        }
    }

    public void BeginDrawLine()
    {
        drawLine = true;
    }

    public void SnapLine()
    {

    }

    public void BuildConnection()
    {
        drawLine = false;
    }
}
