using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Sc_DistanceMarkers : MonoBehaviour
{

    public LineRenderer lRenderer;
    public Material lMaterial;
    public float tileModifier;
    public float currentTiling;


    void Start()
    {
        //runtime only
       /* if(GetComponent<LineRenderer>() != null)
        {
            lRenderer = GetComponent<LineRenderer>();
            lMaterial = lRenderer.material;
        }*/

    }

    void Update()
    {

        lMaterial.SetFloat("_DistanceTiling", currentTiling);

        currentTiling = lRenderer.GetPosition(lRenderer.positionCount-1).z * tileModifier;

    }
}
