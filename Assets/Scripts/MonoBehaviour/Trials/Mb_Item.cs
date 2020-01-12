using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mb_Item : Mb_Trial
{

    Collider coll;
    Rigidbody body;
    [Header("ItemPart")]
    [SerializeField] Collider triggerCollider;
    Mb_PlayerControler user;
    public ItemType itemType;
    public int itemValue;
   //[HideInInspector]
    public bool thrown = false;
    Mb_Speedable speedInfluencer;

    protected virtual void Awake()
    {
        coll = GetComponent<Collider>();
        body = GetComponent<Rigidbody>();
        speedInfluencer = GetComponent<Mb_Speedable>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        SetThrown(false);

    }

    public override void DoThings()
    {
        //On recupere l objet, on le set comme objet pour joueur et on lui d arreter d objet
        user = listOfUser[0];
        speedInfluencer.ResetStrenghApplied();
        SetThrown(false);

        user.itemHold = this;
        user.RemoveOverlapedTrial(this); //virer le trial de la liste des trials overlap pour qu'il puisse interagir avec autre chose

        //desactiver le coll physiquede l objet
        coll.enabled = false;
        body.isKinematic = true;
        //desactiver les composents de trial de l objet a recup
        triggerCollider.enabled = false;
        base.DoThings();

        //Set sa position sur le handle du joueur et le parent
        transform.SetParent(user.itemHandle);
        transform.localPosition = new Vector3(0, 0, 0);
        transform.localRotation = Quaternion.identity;



        
    }

    public void ResetInteraction()
    {
        SetThrown(true);
        transform.position = user.placeToThrow.position;
        transform.rotation = user.placeToThrow.rotation;
        transform.SetParent(null);
        coll.enabled = true;
        body.isKinematic =false;
        triggerCollider.enabled = true;
    }

    public void SetThrown(bool throwValue)
    {
        thrown = throwValue;
    }

    public void Throw(Vector3 direction, float strength)
    {
        print(direction * strength);
        ResetInteraction();
        body.AddForce(direction * strength, ForceMode.Impulse);
    }  
}

public enum ItemType
{
    Loot, Drill, Crowbar, Pass1, Pass2, Pass3, Tablet, Null
}


