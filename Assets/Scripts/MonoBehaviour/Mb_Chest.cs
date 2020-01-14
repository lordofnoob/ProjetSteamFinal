using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mb_Chest : Mb_Trial
{
    [Header("ItemToDrop")]
    [SerializeField] GameObject[] allItemToDrop;

    [Header("ResolutionPart Travel")]
    [SerializeField] Transform itemCreationSpot;
    [SerializeField] Transform dropSpot;
    [SerializeField] float randomRangePosition;
    [SerializeField] float timeToGetToSpot;
    bool used = false;
    List<GameObject> gameObjectToGive= new List<GameObject>();


    public override bool CanInterract()
    {
       
        if (base.CanInterract() == true && used == false)
            return true;
        else
        {
            return false;
        }
    }

    private void Awake()
    {
        for (int i = 0; i < allItemToDrop.Length; i++)
        {
            GameObject newItem = Instantiate(allItemToDrop[i], itemCreationSpot.position, Quaternion.identity, null);
            gameObjectToGive.Add(newItem);
            
            newItem.gameObject.SetActive(false);
        }
    }

    public override void DoThings()
    {   
        for (int i = 0; i < allItemToDrop.Length; i++)
        {
            gameObjectToGive[i].SetActive(true);
            Vector3 positionToLerpItem = dropSpot.position + new Vector3(Random.Range(1, randomRangePosition),0, Random.Range(1, randomRangePosition));
            LerpItem(positionToLerpItem, gameObjectToGive[i]); 
        }
        used = true;
        base.DoThings();
    }

    void LerpItem(Vector3 PositionToLerp, GameObject itemToLerp)
    {
        itemToLerp.transform.DOMove(new Vector3(PositionToLerp.x,transform.position.y, PositionToLerp.z), timeToGetToSpot);
    }
}