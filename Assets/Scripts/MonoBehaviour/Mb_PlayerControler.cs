using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
using UnityEngine.UI;

public class Mb_PlayerControler : MonoBehaviour
{
    [Header("Manette")]
    public PlayerIndex playerIndex;
    [HideInInspector] public GamePadState controlerUsedState;
    GamePadState controlerUsedOldState;

    [Header("Movement")]
    [SerializeField] Sc_PlayerCharact playerCharacts;
    PlayerMovementParameters liveParameters;
    Rigidbody body;
    Collider collider;
    Mb_Speedable moveInfluence;
    bool canMove = true;

    [Header("InteractionPart")]
    List<Mb_Trial> CurrentTrialsOverlaped = new List<Mb_Trial>();
    public Mb_Item itemHold;
    float throwTime;

    [Header("ThrowItemPart")]
    public Transform placeToThrow;

    [Header("GraphPart")]
    public Transform itemHandle;
    public Animator rAnimator;

    [Header("Ui")]
    public Image strengthBar;

    float floorAnim = 0.5f;

 /*   [Header("Input")]
    [SerializeField] KeyCode interactInput, throwInput, deposeInput;*/

    //A SUPPR APRES 
   // float throwTimePressed;


    void Start()
    {
        body = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        moveInfluence = GetComponent<Mb_Speedable>();
        // rAnimator = transform.GetChild(0).GetComponent<Animator>();
        liveParameters = playerCharacts.baseCharacterMovement;

        controlerUsedState = GamePad.GetState(playerIndex);
        UpdateThrowUI();
    }

    void Update()
    {
        controlerUsedOldState = controlerUsedState;
        controlerUsedState = GamePad.GetState(playerIndex);

        //recup des inputs a la frame
        if (canMove == true)
            Move();
        else
            body.velocity = moveInfluence.strengthApplied;
    }

    private void FixedUpdate()
    {
        APress();
        XPress();
        BPress();
      /*  InterractiveInput();
        DeposeInput();
        ThrowInput();*/
    }

    private void Move()
    {
        Vector3 moveDir = CurrentStickDirectionNormalized();
        Vector3 targetMovePosition = liveParameters.MoveSpeed * liveParameters.AccelerationRate.Evaluate(CurrentStickDirection().magnitude) * moveDir + moveInfluence.strengthApplied;

        RaycastHit hit;
        bool raycastHit = Physics.Raycast(new Vector3(transform.position.x, 0f, transform.position.z), moveDir, out hit,1 & (1 ^9));


        if (raycastHit && hit.collider.transform.tag != "TriggerZone")
        { 
            targetMovePosition = Vector3.ProjectOnPlane(targetMovePosition, hit.normal);
        }

        body.velocity = targetMovePosition;

        SetAnimFloat();

        if (CurrentStickDirection() != Vector3.zero)
            UpdateRotation();

    }

    void UpdateRotation()
    {
        transform.localRotation = Quaternion.Euler(new Vector3(0, RotY(), 0));
    }

    public float RotY()
    {
        return Mathf.Atan2(CurrentStickDirectionNormalized().x, CurrentStickDirectionNormalized().z)* Mathf.Rad2Deg;
    }

    //INPUT THINGY

    private void APress()
    {

        if (controlerUsedOldState.Buttons.A == ButtonState.Released && controlerUsedState.Buttons.A == ButtonState.Pressed
            && CurrentTrialsOverlaped.Count > 0  && usedTrial().CanInterract() == true)
        {
            Mb_Item isItem = usedTrial().GetComponent<Mb_Item>();
            if (isItem == null)
                SetCanMove(false);
            StartCoroutine(WaitAfterInteract());
            usedTrial().AddAvancement(usedTrial().trialRules.accomplishmentToAdd);
        }


       else if (controlerUsedOldState.Buttons.A == ButtonState.Pressed && controlerUsedState.Buttons.A == ButtonState.Released
       && CurrentTrialsOverlaped.Count > 0 && usedTrial().trialRules.trialType == TrialType.Mashing && usedTrial().CanInterract() == true)
        {
            //setup du trigger de l anim si tu porte un objet ou pas
            if (itemHold != null)
            {
                SetAnimTrigger(itemHold.itemType, usedTrial().animationType);
            }
            else
                SetAnimTrigger(ItemType.Null, usedTrial().animationType);


            usedTrial().AddAvancement(usedTrial().trialRules.accomplishmentToAdd);

            StartCoroutine(WaitAfterInteract());
        }


        else if (controlerUsedOldState.Buttons.A == ButtonState.Pressed && controlerUsedState.Buttons.A == ButtonState.Pressed
        && CurrentTrialsOverlaped.Count > 0 && usedTrial().trialRules.trialType == TrialType.Time && usedTrial().CanInterract() == true)
        {
            //setup du trigger de l anim si tu porte un objet ou pas
            if (itemHold != null)
                SetAnimTrigger(itemHold.itemType, usedTrial().animationType);
            else
                SetAnimTrigger(ItemType.Null, usedTrial().animationType);

            
            usedTrial().AddAvancement(usedTrial().trialRules.accomplishmentToAdd * Time.fixedDeltaTime);

            StartCoroutine(WaitAfterInteract());
        }
    }
    IEnumerator WaitAfterInteract()
    {
        yield return new WaitForSeconds(0.2f);
        canMove = true;
    }

    private void XPress()
    {
        if (itemHold != null && controlerUsedOldState.Buttons.X == ButtonState.Pressed
            && controlerUsedState.Buttons.X == ButtonState.Pressed && CurrentTrialsOverlaped.Count == 0)
        {
            PrepThrowItem();
        }
        else if (itemHold != null && controlerUsedOldState.Buttons.X == ButtonState.Pressed
            && controlerUsedState.Buttons.X == ButtonState.Released && CurrentTrialsOverlaped.Count == 0)
        {
            ThrowItem();
        }
    }

    private void BPress()
    {
        if (controlerUsedOldState.Buttons.B == ButtonState.Released && controlerUsedState.Buttons.B == ButtonState.Pressed)
        {
            // DO SHIT 
        }
    }

    /*
        void InterractiveInput()
        {
            if ( Input.GetKeyDown(interactInput) && CurrentTrialsOverlaped.Count > 0 && usedTrial().CanInterract() == true)
            {
                Mb_Item isItem = usedTrial().GetComponent<Mb_Item>();
                if (isItem == null)
                    SetCanMove(false);
                StartCoroutine(WaitAfterInteract());
                usedTrial().AddAvancement(usedTrial().trialRules.accomplishmentToAdd);
            }
            else if(Input.GetKeyUp(interactInput) && CurrentTrialsOverlaped.Count > 0 && usedTrial().trialRules.trialType == TrialType.Mashing && usedTrial().CanInterract() == true)
            {
                //setup du trigger de l anim si tu porte un objet ou pas
                if (itemHold != null)
                {
                    SetAnimTrigger(itemHold.itemType, usedTrial().animationType);
                }
                else
                    SetAnimTrigger(ItemType.Null, usedTrial().animationType);


                usedTrial().AddAvancement(usedTrial().trialRules.accomplishmentToAdd);

                StartCoroutine(WaitAfterInteract());
            }
            else if (Input.GetKeyDown(interactInput) && CurrentTrialsOverlaped.Count > 0 && usedTrial().trialRules.trialType == TrialType.Mashing && usedTrial().CanInterract() == true)
            { //setup du trigger de l anim si tu porte un objet ou pas
                if (itemHold != null)
                    SetAnimTrigger(itemHold.itemType, usedTrial().animationType);
                else
                    SetAnimTrigger(ItemType.Null, usedTrial().animationType);


                usedTrial().AddAvancement(usedTrial().trialRules.accomplishmentToAdd * Time.fixedDeltaTime);

                StartCoroutine(WaitAfterInteract());
            }
        }
        void DeposeInput()
        {
            if (Input.GetKeyDown(deposeInput))
                ThrowItem();
        }
        void ThrowInput()
        {
            if (Input.GetKey(throwInput))
            {
                throwTime += Time.fixedDeltaTime;
                UpdateThrowUI();
            }
            else if (Input.GetKeyUp(throwInput))
            {
                ThrowItem();
            }
        }

       
        */
    public void PrepThrowItem()
    {
        throwTime += Time.fixedDeltaTime;
        UpdateThrowUI();

    }

    private void ThrowItem()
    {
        itemHold.Throw(transform.forward, playerCharacts.throwGrowingStrengh.Evaluate(throwTime) * playerCharacts.throwMaxStrengh);
        itemHold = null;
        throwTime = 0;
        UpdateThrowUI();
    }

    //MOVINGWAIT
    public void SetCanMove(bool value)
    {
        canMove = value;
    }


    //INTERACTIONPART
    Mb_Trial usedTrial()
    {
        if (CurrentTrialsOverlaped.Count != 0)
        {
            Mb_Trial trial = CurrentTrialsOverlaped[0]; ;
            for (int i = 0; i < CurrentTrialsOverlaped.Count; i++) 
            {
                if (trial.trialRules.trialPriority <= CurrentTrialsOverlaped[i].trialRules.trialPriority)
                    trial = CurrentTrialsOverlaped[i];
            }
            return trial;
        }
        else
            return null;
    }

    public void AddOverlapedTrial(Mb_Trial trialToAdd)
    {
        CurrentTrialsOverlaped.Add(trialToAdd);
    }

    public void RemoveOverlapedTrial(Mb_Trial trialToRemove)
    {
        CurrentTrialsOverlaped.Remove(trialToRemove);
    }

    //UI UPDATE
    public void UpdateThrowUI()
    {
        strengthBar.fillAmount = playerCharacts.throwGrowingStrengh.Evaluate(throwTime);
    }

  
    //VECTOR MANNETTE REGION
    #region
    public Vector3 CurrentStickDirection()
    {
       /* if (Input.GetAxis("Horizontal")!=0 || Input.GetAxis("Vertical") != 0)
        {
            return new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }
        else*/
        return new Vector3(controlerUsedState.ThumbSticks.Left.X, 0, controlerUsedState.ThumbSticks.Left.Y);
    }

    public Vector3 CurrentStickDirectionNormalized()
    {
       /* if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            return Vector3.Normalize(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
        }/*
        else*/
            return Vector3.Normalize(new Vector3(controlerUsedState.ThumbSticks.Left.X, 0, controlerUsedState.ThumbSticks.Left.Y));
    }

    public Vector3 OldStickDirection()
    {
        return new Vector3(controlerUsedOldState.ThumbSticks.Left.X, 0, controlerUsedOldState.ThumbSticks.Left.Y);
    }
    public Vector3 OldStickDirectionNormalized()
    {
        return Vector3.Normalize(new Vector3(controlerUsedOldState.ThumbSticks.Left.X, 0, controlerUsedOldState.ThumbSticks.Left.Y));
    }
    #endregion

    //AnimFonction

    private void SetAnimFloat()
    {

        // anim
        rAnimator.SetFloat("Speed", animCourseValue());
        if (CurrentStickDirectionNormalized().magnitude > 0)
        {

        }
        else
        {
            if (rAnimator.GetFloat("Speed") > floorAnim)
                rAnimator.SetFloat("Speed", Mathf.Lerp(rAnimator.GetFloat("Speed"), 0, 0.3f));
        }
    }

    float animCourseValue()
    {
        return playerCharacts.baseCharacterMovement.AccelerationRate.Evaluate(CurrentStickDirection().magnitude - 0.04f); ;
    }

    // SET ICI LES TRIGGER D ANIMATIONS
    void SetAnimTrigger(ItemType toolAnimToProck, animationInteractionType animType)
    {
        if (usedTrial().trialRules.toolsNeeded.Length > 0)
        {
            for (int i = 0; i < usedTrial().trialRules.toolsNeeded.Length; i++)
                if (toolAnimToProck == usedTrial().trialRules.toolsNeeded[i])
                {
                    switch (toolAnimToProck)
                    {
                        case ItemType.Crowbar:
                            
                            rAnimator.SetTrigger("CrowbarTrialValidation");
                            break;
                        case ItemType.Drill:
                            rAnimator.SetTrigger("CrowbarTrialValidation");
                            break;
                        case ItemType.Tablet:
                            rAnimator.SetTrigger("HackingTrial");
                            break;

                    }
                }
        }
        else
        {
            switch (animType)
            {
                case animationInteractionType.Button:
                    rAnimator.SetTrigger("PushButton");
                    break;
                case animationInteractionType.PickUp:
                    rAnimator.SetTrigger("PushButton");
                    break;
                case animationInteractionType.Hacking:
                    rAnimator.SetTrigger("HackingTrial");
                    break;
                case animationInteractionType.InteractionClassic:
                    rAnimator.SetTrigger("PushButton");
                    break;
            }
           
        }
    }


}
