using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mb_Item : Mb_Trial
{
    Collider coll;
    Rigidbody body;
    [SerializeField] Mb_TrialCollider triggerCollider;
    Mb_PlayerControler user;
    public ItemType itemType;

    protected virtual void Awake()
    {
        coll = GetComponent<Collider>();
        body = GetComponent<Rigidbody>();
    }

    public override void DoThings()
    {
        user = listOfUser[0];

        user.itemHold = this;
        user.RemoveOverlapedTrial(this);

        //desactiver les composents de trial de l objet a recup

        //Set sa position sur le handle du joueur et le parent
        transform.SetParent(user.itemHandle);
        transform.localPosition = new Vector3(0, 0, 0);
        transform.localRotation = Quaternion.identity;

        //desactiver le coll physiquede l objet
        coll.enabled = false;
        body.isKinematic =true;
        triggerCollider.enabled = false;
        base.DoThings();
    }

    public void ResetInteraction()
    {
   
        transform.position = user.placeToThrow.position;
        transform.rotation = user.placeToThrow.rotation;
        transform.SetParent(null);
        coll.enabled = true;
        body.isKinematic =false;
        triggerCollider.enabled = true;
    }

    public void Throw(Vector3 direction, float strength)
    {
        ResetInteraction();
        body.AddForce(direction * strength, ForceMode.Impulse);
    }  
}

public enum ItemType
{
    Loot, Drill, Crowbar,
}


