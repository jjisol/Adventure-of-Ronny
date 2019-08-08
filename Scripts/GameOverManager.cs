using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
	public PlayerHealth playerHealth;       // Reference to the player's health.
	public float restartDelay = 5f;         // Time to wait before restarting the level
	public int timeLeft = 100;
	public Text countdownText;
	Animator anim;                          // Reference to the animator component.
	float restartTimer;                     // Timer to count up to restarting the level


	void Awake ()
	{
		// Set up the reference.
		anim = GetComponent <Animator> ();
	}


	void Start()
	{
		StartCoroutine("LoseTime");
	}

	void Update ()
	{
		// If the player has run out of health...
		if(playerHealth.currentHealth <= 0 && ScoreManager.score < 150)
		{
			// ... tell the animator the game is over.
			anim.SetTrigger ("GameOver");

			// .. increment a timer to count up to restarting.
			restartTimer += Time.deltaTime;

			// .. if it reaches the restart delay...
			if(restartTimer >= restartDelay)
			{
				// .. then reload the currently loaded level.
				SceneManager.LoadScene("1");
			}
		}

		countdownText.text = ("Time Left: " + timeLeft);
			
		if (timeLeft <= 0 && ScoreManager.score < 150)
		{
			StopCoroutine("LoseTime");
			countdownText.text = "Times Up!";
			anim.SetTrigger ("GameOver");
			//Application.LoadLevel(Application.loadedLevel);
		}
	}

	IEnumerator LoseTime()
	{
		while (true)
		{
			yield return new WaitForSeconds(1);
			timeLeft--;
		}
	}
}