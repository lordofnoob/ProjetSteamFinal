using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ma_GameManager : MonoBehaviour
{
    public static Ma_GameManager instance;
    public int redRessource, blueRessource, greenRessource;

    public Mb_Node3d currentNodeOverlaped;
    public LineRenderer currentLineTraced;
    public GameObject lineRendererPrefab; 

    private void Start()
    {
        instance = this;
        blueRessource = Ma_LevelManager.instance.levelParameters.baseBlueRessource;
        redRessource = Ma_LevelManager.instance.levelParameters.baseRedRessource;
        greenRessource = Ma_LevelManager.instance.levelParameters.baseGreenRessource;
    }

    private void Update()
    {
        Debug.DrawLine(Camera.main.transform.position, MousePosition());
        if (Input.GetMouseButtonDown(0) && currentNodeOverlaped!=null )
        {
            if (currentLineTraced == null)
                StartNewLine(currentNodeOverlaped);
            else
            {
                if (currentNodeOverlaped != null)
                {
                    currentLineTraced.SetPosition(1, currentNodeOverlaped.transform.position);
                    currentLineTraced = null;
                }     
            }
        }
        else if (currentLineTraced!=null)
            currentLineTraced.SetPosition(1, MousePosition());
    }

    void StartNewLine(Mb_Node3d beginingOfTheLine)
    {
        currentLineTraced = Instantiate(lineRendererPrefab, Vector3.zero, Quaternion.identity).GetComponent<LineRenderer>();
        currentLineTraced.SetPosition(0, beginingOfTheLine.transform.position);
    }

    public Vector3 MousePosition()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
            return hit.point;
        // Physics.Raycast(Camera.main.transform.position, Input.mousePosition, out hit, Mathf.Infinity, 0);
        else
            return Vector3.zero;

        
    }

}
