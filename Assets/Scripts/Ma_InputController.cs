using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class Ma_InputController : MonoBehaviour
{

    [Header("GamePadInput")]
    public PlayerIndex playerIndex;
    GamePadState controlerUsedOldState;
    GamePadState controlerUsedState;
    [HideInInspector]public ButtonState AButton, BButton, XButton, YButton, StartButton, DPadUp, DpadRight, DpadDown, DpadLeft;
    [HideInInspector]public ButtonState OldAButton, OldBButton, OldXButton, OldYButton, OldStartButton, OldDPadUp, OldDpadRight, OldDpadDown, OldDpadLeft;
    [HideInInspector]public Vector3 LeftThumbStick, RightThumbStick;
    [HideInInspector]public Vector3 OldLeftThumbStick, OldRightThumbStick;

    private void Awake()
    {
        AButton = ButtonState.Released;
        BButton = ButtonState.Released;
        XButton = ButtonState.Released;
        YButton = ButtonState.Released;
        StartButton = ButtonState.Released;
        DPadUp = ButtonState.Released;
        DpadRight = ButtonState.Released;
        DpadDown = ButtonState.Released;
        DpadLeft = ButtonState.Released;

        OldAButton = ButtonState.Released;
        OldBButton = ButtonState.Released;
        OldXButton = ButtonState.Released;
        OldYButton = ButtonState.Released;
        OldStartButton = ButtonState.Released;
        OldDPadUp = ButtonState.Released;
        OldDpadRight = ButtonState.Released;
        OldDpadDown = ButtonState.Released;
        OldDpadLeft = ButtonState.Released;

        LeftThumbStick = Vector3.zero;
        RightThumbStick = Vector3.zero;
        OldLeftThumbStick = Vector3.zero;
        OldRightThumbStick = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        controlerUsedOldState = controlerUsedState;
        controlerUsedState = GamePad.GetState(playerIndex);

        OldAButton = controlerUsedOldState.Buttons.A;
        OldBButton = controlerUsedOldState.Buttons.B;
        OldXButton = controlerUsedOldState.Buttons.X;
        OldYButton = controlerUsedOldState.Buttons.Y;
        OldStartButton = controlerUsedOldState.Buttons.Start;
        OldDPadUp = controlerUsedOldState.DPad.Up;
        OldDpadRight = controlerUsedOldState.DPad.Right;
        OldDpadDown = controlerUsedOldState.DPad.Down;
        OldDpadLeft = controlerUsedOldState.DPad.Left;

        OldLeftThumbStick = new Vector3(controlerUsedOldState.ThumbSticks.Left.X, 0f, controlerUsedOldState.ThumbSticks.Left.Y);
        OldRightThumbStick = new Vector3(controlerUsedOldState.ThumbSticks.Right.X, 0f, controlerUsedOldState.ThumbSticks.Right.Y);

        AButton = controlerUsedState.Buttons.A;
        BButton = controlerUsedState.Buttons.B;
        XButton = controlerUsedState.Buttons.X;
        YButton = controlerUsedState.Buttons.Y;
        StartButton = controlerUsedState.Buttons.Start;
        DPadUp = controlerUsedState.DPad.Up;
        DpadRight = controlerUsedState.DPad.Right;
        DpadDown = controlerUsedState.DPad.Down;
        DpadLeft = controlerUsedState.DPad.Left;

        LeftThumbStick = new Vector3(controlerUsedState.ThumbSticks.Left.X, 0f, controlerUsedState.ThumbSticks.Left.Y);
        RightThumbStick = new Vector3(controlerUsedState.ThumbSticks.Right.X, 0f, controlerUsedState.ThumbSticks.Right.Y);

    }
}
