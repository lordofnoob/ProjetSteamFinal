using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mb_HoldingMasks : MonoBehaviour
{
    public List<GameObject> listOfAllMasks;
    private GameObject activeMask;

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
