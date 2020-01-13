using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mb_StealTrigger : MonoBehaviour
{
    [SerializeField] Transform repulsionSpot;
    [SerializeField] ParticleSystem particleToPop;

    private void OnTriggerEnter(Collider other)
    {
        Mb_Item itemEntering = other.GetComponent<Mb_Item>();
        if (itemEntering != null && itemEntering.itemType == ItemType.Loot)
        {
            CheckItem(itemEntering);
            particleToPop.transform.position = other.transform.position;
            particleToPop.Play();
        }
        else if (itemEntering != null)
        {
            other.transform.DOMove(repulsionSpot.position, 0.5f);
        }
    }

    void CheckItem(Mb_Item itemToCheck)
    {
        Gamemanager.instance.AddMoney(itemToCheck.itemValue);
        Gamemanager.instance.CheckItemToGet(itemToCheck);
        Destroy(itemToCheck.uiToTrigger.transform.parent.gameObject);
        Destroy(itemToCheck.gameObject);
    }
}
