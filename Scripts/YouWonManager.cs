using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class YouWonManager : MonoBehaviour {

	Animator anim;
	float restartTimer; 
	public float restartDelay = 5f; 
	//ReplayController replaycontroller;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		//replaycontroller = GetComponent<ReplayController> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (ScoreManager.score >= 150) {
			anim.SetTrigger ("YouWon");
		
			restartTimer += Time.deltaTime;

			// .. if it reaches the restart delay...
			if (restartTimer >= restartDelay) {
				//replaycontroller.enabled = true;
				// .. then reload the currently loaded level.
				SceneManager.LoadScene("Menu");
			}
		}
	}
		

}
