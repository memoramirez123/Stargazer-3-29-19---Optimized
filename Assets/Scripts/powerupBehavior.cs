using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerupBehavior : MonoBehaviour {

    public powerupController controller;

    public GameObject Player;

    [SerializeField]
    private powerup powerup;

    private Transform transform_;

    private void Awake()
    {
        transform_ = transform;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GainEnergy();
        activatePowerup();
        gameObject.SetActive(false);
    }

    private void activatePowerup()
    {
        controller.activatePowerup(powerup);
    }

    public void setPowerup(powerup powerup)
    {
        this.powerup = powerup;
        gameObject.name = powerup.name;
    }

    public void GainEnergy()
    {
        playerStats stats = Player.GetComponent<playerStats>();
        stats.energy += 0.25f;
    }
}
