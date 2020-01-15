using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mb_HoldingMasks : MonoBehaviour
{
    [Header("List Of All Masks")]
    public List<GameObject> listOfAllMasks;
    private GameObject activeMask;

    [Header("Handler to put into Mb_PlayerController")]
    public Transform handler;
    [Header("Animator to put into Mb_PlayerController")]
    public Animator animator;


    public void SetActiveMask(GameObject newActiveMask)
    {
        if (activeMask != null)
            activeMask.SetActive(false);
        activeMask = newActiveMask;
        activeMask.SetActive(true);
        //Debug.Log("CHANGER DE MASK : activeMaskIndex : " + listOfAllMasks.IndexOf(activeMask));
    }

    public GameObject GetActiveMask()
    {
        return activeMask;
    }
}
