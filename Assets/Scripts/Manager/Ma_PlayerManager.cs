using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ma_PlayerManager : MonoBehaviour
{
    public Camera cam;
    public static Ma_PlayerManager instance;
    public float depth = 5;

    public Mb_Node activeNode = null;

    void Start()
    {
        instance = this;

        cam = FindObjectOfType<Camera>();
    }

    void Update()
    {
        if(activeNode != null)
        {
            if (Input.GetMouseButtonDown(1))
            {
                activeNode.CancelDrawing();
                activeNode = null;
                return;
            }

            Debug.Log(activeNode.IsSnaping());
            if(activeNode.IsSnaping() && Input.GetMouseButtonDown(0))
            {
                activeNode.BuildConnection();
                return;
            }
        }
    }

    public Vector3 GetMouseCameraPoint()
    {
        var ray = cam.ScreenPointToRay(Input.mousePosition);
        return ray.origin + ray.direction * depth;
    }
}
