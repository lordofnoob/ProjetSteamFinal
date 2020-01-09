using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class Mb_PlayerControler : MonoBehaviour
{
    [Header("Manette")]
    public PlayerIndex playerIndex;
    GamePadState controlerUsedState;
    GamePadState controlerUsedOldState;

    [Header("Movement")]
    [SerializeField] Sc_PlayerCharact playerCharacts;
    PlayerMovementParameters liveParameters;
    Rigidbody body;

    [Header("InteractionPart")]
    List<Mb_Trial> CurrentTrialsOverlaped = new List<Mb_Trial>();
    public Mb_Item itemHold;
    float throwTime;


    [Header("GraphPart")]
    public Transform rightHandHandle, leftHandHandle, backHandle;

    Animator rAnimator;
    float floorAnim = 0.5f;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        rAnimator = transform.GetChild(0).GetComponent<Animator>();
        liveParameters = playerCharacts.baseCharacterMovement;

        controlerUsedState = GamePad.GetState(playerIndex);
    }

    void Update()
    {
        controlerUsedOldState = controlerUsedState;
        controlerUsedState = GamePad.GetState(playerIndex);
        
        //recup des inputs a la frame
        Move();

     
    }

    private void FixedUpdate()
    {
        APress();
        XPress();
        BPress();
    }

    private void Move()
    {
        body.velocity = liveParameters.MoveSpeed * liveParameters.AccelerationRate.Evaluate(CurrentStickDirection().magnitude) * CurrentStickDirectionNormalized();

        SetAnimFloat();

        if (CurrentStickDirection() != Vector3.zero)
            UpdateRotation();

    }

    private void SetAnimFloat()
    {
        rAnimator.SetFloat("Speed", body.velocity.magnitude / playerCharacts.baseCharacterMovement.MoveSpeed);

        // anim
        if (CurrentStickDirectionNormalized().magnitude > 0)
        {
            // if(ne porte rien)
            //anim
            rAnimator.SetBool("Idle00_To_Move", true);
            ;
        }
        else
        {
            /*
                 if (rAnimator.GetFloat("Speed") > floorAnim)
                 rAnimator.SetFloat("Speed", Mathf.Lerp(rAnimator.GetFloat("Speed"), 0, 0.3f));*/
        }
    }

    void UpdateRotation()
    {
        transform.localRotation = Quaternion.Euler(new Vector3(0, RotY(), 0));
    }

    float RotY()
    {
        return Mathf.Atan2(CurrentStickDirectionNormalized().x, CurrentStickDirectionNormalized().z)* Mathf.Rad2Deg - 90;
    }

    //INPUT THINGY

    private void APress()
    {
        if (controlerUsedOldState.Buttons.A == ButtonState.Released && controlerUsedState.Buttons.A == ButtonState.Pressed && CurrentTrialsOverlaped.Count >0 && usedTrial().trialRules.trialType == TrialType.Mashing&& usedTrial().canInteract() == true)
        {
            // DO SHIT 
            usedTrial().AddAvancement(usedTrial().trialRules.accomplishmentToAdd);
        }
        else if (controlerUsedOldState.Buttons.A == ButtonState.Pressed && controlerUsedState.Buttons.A == ButtonState.Pressed && CurrentTrialsOverlaped.Count > 0 && usedTrial().trialRules.trialType == TrialType.Time && usedTrial().canInteract() ==true  )
        {
            // DO SHIT 
            //A corriger ca marche pas // il faut caller ça par seconde
            usedTrial().AddAvancement(usedTrial().trialRules.accomplishmentToAdd * Time.fixedDeltaTime);
        }
        else if(itemHold != null && controlerUsedOldState.Buttons.A == ButtonState.Pressed && controlerUsedState.Buttons.A == ButtonState.Pressed)
        {
            PrepThrowItem();
        }
       
    }

    private void XPress()
    {
        if (controlerUsedOldState.Buttons.X == ButtonState.Released && controlerUsedState.Buttons.X == ButtonState.Pressed)
        {
            // DO SHIT 
        }
    }

    private void BPress()
    {
        if (controlerUsedOldState.Buttons.B == ButtonState.Released && controlerUsedState.Buttons.B == ButtonState.Pressed)
        {
            // DO SHIT 
        }
    }

    public void PrepThrowItem()
    {
       // throwTime += 
    }

    private void ThrowItem()
    {
        //itemHold.GetComponent<Rigidbody>().velocity = transform.localRotation.eulerAngles * throwGrowingStrengh.Evaluate(throwTime);
        itemHold = null;
        throwTime = 0;
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
        print(trialToAdd);
        CurrentTrialsOverlaped.Add(trialToAdd);
    }

    public void RemoveOverlapedTrial(Mb_Trial trialToRemove)
    {
        CurrentTrialsOverlaped.Remove(trialToRemove);
    }
    //VECTOR MANNETTE REGION
    #region
    public Vector3 CurrentStickDirection()
    {
        return new Vector3(controlerUsedState.ThumbSticks.Left.X, 0, controlerUsedState.ThumbSticks.Left.Y);
    }

    public Vector3 CurrentStickDirectionNormalized()
    {
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
}
