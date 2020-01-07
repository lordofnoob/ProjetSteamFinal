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

    private Color nodeColor;
    private bool drawLine = false;
    private bool snaping = false;
    public Mb_Connection connection;

    void Start()
    {
        switch (type)
        {
            case NodeType.Red:
                nodeColor = Color.red;
                break;

            case NodeType.Green:
                nodeColor = Color.green;
                break;

            case NodeType.Blue:
                nodeColor = Color.blue;
                break;

        }
    }

    void Update()
    {
        if (drawLine)
        {
            connection.SetEndLinePosition(Ma_PlayerManager.instance.GetMouseCameraPoint());
        }

        Debug.Log(Vector3.Distance(Ma_PlayerManager.instance.GetMouseCameraPoint(), transform.position));
        if(Ma_PlayerManager.instance.activeNode != null)
        {
            if (function == NodeFunction.Input && Ma_PlayerManager.instance.activeNode.type == type && Vector3.Distance(Ma_PlayerManager.instance.GetMouseCameraPoint(), transform.position) < 1)
            {
                SnapLine();
            }
            else if (Ma_PlayerManager.instance.activeNode.snaping && Vector3.Distance(Ma_PlayerManager.instance.GetMouseCameraPoint(), transform.position) >= 1)
            {
                Ma_PlayerManager.instance.activeNode.snaping = false;
            }
        }
    }

    public void BeginDrawLine()
    {
        connection = UniversalPool.GetItem("Connection").GetComponent<Mb_Connection>();
        //Debug.Log(connection);
        connection.SetStartLinePosition(transform.position);
        connection.SetLineColor(nodeColor, 0.5f);
        connection.StartDrawing();
        Ma_PlayerManager.instance.activeNode = this;
        drawLine = true;
    }

    public void SnapLine()
    {
        Ma_PlayerManager.instance.activeNode.snaping = true;
        Ma_PlayerManager.instance.activeNode.connection.SetEndLinePosition(transform.position);
    }

    public bool IsSnaping()
    {
        return snaping;
    }

    public void CancelDrawing()
    {
        UniversalPool.ReturnItem(connection.gameObject, "Connection");
        drawLine = false;
    }

    public void BuildConnection()
    {
        Debug.Log("BUILD");
        drawLine = false;
        connection.SetLineColor(nodeColor, 1f);
    }
}
