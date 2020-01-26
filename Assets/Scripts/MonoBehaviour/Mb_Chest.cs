using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mb_Chest : Mb_Trial
{
    [Header("ItemToDrop")]
    [SerializeField] Loot[] allItemToDrop;

    [Header("ResolutionPart Travel")]
    [SerializeField] Transform itemCreationSpot;
    [SerializeField] Transform dropSpot;
    [SerializeField] bool isDiamondChest = false;
    [SerializeField] ParticleSystem particleSystemToSpawn;
    [SerializeField] GameObject[] objectsToDisableOnTrialDone;
    [SerializeField] float randomRangePosition;
    [SerializeField] float timeToGetToSpot;
    bool used = false;
    List<GameObject> gameObjectToGive= new List<GameObject>();
    int currentInteractionIndex = 0;
    int currentItemToGiveIndex= 0;
    int currentItemIndex = 0;

    public override bool CanInterract()
    {
       
        if (base.CanInterract() == true && currentInteractionIndex < allItemToDrop.Length)
            return true;
        else
        {
            return false;
        }
    }

    public override void Awake()
    {
        for (int i = 0; i < allItemToDrop.Length; i++)
        {
            for (int y = 0; y < allItemToDrop[i].itemToDrop.Length; y++)
            {
                GameObject newItem = Instantiate(allItemToDrop[i].itemToDrop[y], itemCreationSpot.position, Quaternion.identity, null);
                gameObjectToGive.Add(newItem);
                newItem.gameObject.SetActive(false);
            }
        }
        base.Awake();
    }

    public override void DoThings()
    {
        currentItemToGiveIndex += allItemToDrop[currentInteractionIndex].itemToDrop.Length;
        for (int i= currentItemIndex; i < currentItemToGiveIndex; i++)
        {
           
            gameObjectToGive[i].SetActive(true);
            Vector3 positionToLerpItem = dropSpot.position + new Vector3(Random.Range(1, randomRangePosition), 0, Random.Range(1, randomRangePosition));
            LerpItem(positionToLerpItem, gameObjectToGive[i]);
        }


        currentItemIndex = currentItemToGiveIndex;
        currentInteractionIndex++;
        if (currentInteractionIndex < allItemToDrop.Length)
            if(animatorAssociated != null)
                animatorAssociated.SetBool("StillFull", true);
        else
        {
            if (animatorAssociated != null)
                animatorAssociated.SetBool("StillFull", false);
            UiDisactivate();
            Destroy(uiToTrigger);
        }

        if (isDiamondChest)
        {
            foreach(GameObject obj in objectsToDisableOnTrialDone)
            {
                obj.SetActive(false);
            }
            particleSystemToSpawn.Play();
        }
    

        base.DoThings();
    }

    void LerpItem(Vector3 PositionToLerp, GameObject itemToLerp)
    {
        itemToLerp.transform.DOMove(new Vector3(PositionToLerp.x,transform.position.y, PositionToLerp.z), timeToGetToSpot);
    }
}

[System.Serializable]
public struct Loot
{
    public GameObject[] itemToDrop;
}