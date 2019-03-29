using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveRustle : MonoBehaviour {

    Animator myAnimator;
	BoxCollider2D myBodyCollider;
	// Use this for initialization
	void Start () {
		myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		CheckLeaves();		
	}


	private void CheckLeaves(){
	 if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
			myAnimator.SetTrigger("Rustle");
            // isAlive = false;
            // myAnimator.SetTrigger("Dying");
            // velocity = deathKick;
            // FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
	}

}
