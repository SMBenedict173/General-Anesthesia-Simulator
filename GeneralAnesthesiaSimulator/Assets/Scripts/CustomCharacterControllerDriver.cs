using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Unity;

public class CustomCharacterControllerDriver : CharacterControllerDriver
{
    void Update()
    {
        this.UpdateCharacterController();
    }

    /// <summary>
    /// Update the <see cref="CharacterController.height"/> and <see cref="CharacterController.center"/>
    /// based on the camera's position.
    /// </summary>
    protected override void UpdateCharacterController()
    {
        if (xrRig == null || characterController == null)
            return;

        var height = Mathf.Clamp(xrRig.cameraInRigSpaceHeight, minHeight, maxHeight);

        Vector3 center = xrRig.cameraInRigSpacePos;
        center.y = height / 2f + characterController.skinWidth;

        characterController.height = height;
        characterController.center = center;
    }
}
