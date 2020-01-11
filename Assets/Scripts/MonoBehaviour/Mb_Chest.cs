using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mb_Chest : Mb_Trial
{
    [Header("ItemToDrop")]
    [SerializeField] Mb_Item[] allItemToDrop;

    [Header("ResolutionPart Travel")]
    [SerializeField] Transform itemCreationSpot;
    [SerializeField] Transform dropSpot;
    [SerializeField] float randomRangePosition;
    [SerializeField] float timeToGetToSpot;
    bool used = false;

    public override bool CanInterract()
    {
        if (base.CanInterract() == true && used == false)
            return true;
        else
            return false;
    }


    public override void DoThings()
    {   
        for (int i = 0; i < allItemToDrop.Length; i++)
        {
            Mb_Item newItem = Instantiate(allItemToDrop[i], itemCreationSpot.position, Quaternion.identity, null);
            Vector3 positionToLerpItem = dropSpot.position + new Vector3(Random.Range(1, randomRangePosition),0, Random.Range(1, randomRangePosition));
            LerpItem(positionToLerpItem, newItem); 
        }
        used = true;
        base.DoThings();
    }

    void LerpItem(Vector3 PositionToLerp, Mb_Item itemToLerp)
    {
        print(PositionToLerp);
      //  itemToLerp.transform.DOMoveX(PositionToLerp.x, timeToGetToSpot);
        itemToLerp.transform.DOMove(new Vector3(PositionToLerp.x,transform.position.y, PositionToLerp.z), timeToGetToSpot);
    }
}