using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerupController : MonoBehaviour {

    public GameObject powerupPrefab;

    public GameObject Player;

    public List<powerup> powerups;

    public Dictionary<powerup, float> activePowerups = new Dictionary<powerup, float>();

    private List<powerup> keys = new List<powerup>();

    void Start()
    {

    }

    void Update ()
    {
        handleActivePowerups();
    }

    public void handleActivePowerups()
    {
        bool changed = false;

        if (activePowerups.Count > 0)
        {
            foreach(powerup powerup in keys)
            {
                if (activePowerups[powerup] > 0)
                {
                    activePowerups[powerup] -= 0.0009f;
                    Player.GetComponent<playerStats>().energy -= 0.0009f;
                }
                else
                {
                    changed = true;

                    activePowerups.Remove(powerup);

                    powerup.end();
                }
                    
            }
        }
        if (changed)
        {
            keys = new List<powerup>(activePowerups.Keys);
        }
    }

    public void activatePowerup(powerup powerup)
    {
        if (!activePowerups.ContainsKey(powerup))
        {
            powerup.start();
            activePowerups.Add(powerup, Player.GetComponent<playerStats>().energy);
        }
        else
        {
            activePowerups[powerup] += Player.GetComponent<playerStats>().energy;
        }

        keys = new List<powerup>(activePowerups.Keys);
    }

    public GameObject spawnPowerup(powerup powerup, Vector3 position)
    {
        GameObject powerupGameObject = Instantiate(powerupPrefab);

        var powerupBehavior = powerupGameObject.GetComponent<powerupBehavior>();

        powerupBehavior.controller = this;

        powerupBehavior.setPowerup(powerup);

        powerupGameObject.transform.position = position;

        return powerupGameObject;

    }
}
