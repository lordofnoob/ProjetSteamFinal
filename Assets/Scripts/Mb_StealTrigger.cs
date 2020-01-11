using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mb_StealTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Mb_Item itemEntering = other.GetComponent<Mb_Item>();
        if (itemEntering != null)
            CheckItem(itemEntering);
    }

    void CheckItem(Mb_Item itemToCheck)
    {
        Gamemanager.instance.AddMoney(itemToCheck.itemValue);
        Gamemanager.instance.CheckItemToGet(itemToCheck);
    }
}
