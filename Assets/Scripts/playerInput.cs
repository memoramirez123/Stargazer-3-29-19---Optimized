using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(playerMovement))]
public class playerInput : MonoBehaviour
{

    playerMovement player;

    void Start()
    {
        player = GetComponent<playerMovement>();
    }

    void Update()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float controlThrowJump = CrossPlatformInputManager.GetAxis("Jump");
        Vector2 directionalInput = new Vector2(controlThrow, controlThrowJump);
        player.SetDirectionalInput(directionalInput);

        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            player.OnJumpInputDown();
        }
        if (CrossPlatformInputManager.GetButtonUp("Jump"))
        {
            player.OnJumpInputUp();
        }
    }
}
