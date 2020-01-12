using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mb_StealTrigger : MonoBehaviour
{
    [SerializeField] ParticleSystem particleToPop;

    private void OnTriggerEnter(Collider other)
    {
        Mb_Item itemEntering = other.GetComponent<Mb_Item>();
        if (itemEntering != null)
        {
            CheckItem(itemEntering);
            particleToPop.transform.position = other.transform.position;
            particleToPop.Play();
        }
    }

    void CheckItem(Mb_Item itemToCheck)
    {
        print(itemToCheck);
        Gamemanager.instance.AddMoney(itemToCheck.itemValue);
        Gamemanager.instance.CheckItemToGet(itemToCheck);
    }
}
