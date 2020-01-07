using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mb_Node3d : MonoBehaviour
{
    private void OnMouseOver()
    {
        Ma_GameManager.instance.currentNodeOverlaped = this;
    }
    private void OnMouseExit()
    {
        Ma_GameManager.instance.currentNodeOverlaped = null;
    }
  
}
