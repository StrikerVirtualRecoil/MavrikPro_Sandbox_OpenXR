using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using StrikerLink.Unity.Runtime.Core;
using UnityEngine.XR;

public class StrikerXRController : XRController
{
    public StrikerDevice strikerDevice;
    protected override void UpdateInput(XRControllerState controllerState)
    {
        base.UpdateInput(controllerState);
        if (controllerState == null)
            return;

        // Custom trigger check
        if (strikerDevice.GetTriggerDown())
        {
            controllerState.ResetFrameDependentStates();
            controllerState.selectInteractionState.SetFrameState(true, 1.0f);
            controllerState.activateInteractionState.SetFrameState(true, 1.0f);
            controllerState.uiPressInteractionState.SetFrameState(true, 1.0f);
        }
    }
}
