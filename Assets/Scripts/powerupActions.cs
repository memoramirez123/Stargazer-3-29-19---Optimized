using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerupActions : MonoBehaviour
{
    [SerializeField]
    private player playerController;

    public void startPowerupTest()
    {
        playerController.jumpSpeed *= 1.5f;
        Debug.Log("Powerup Start");
    }

    public void endPowerupTest()
    {
        playerController.jumpSpeed /= 1.5f;
        Debug.Log("Powerup End");
    }
}
