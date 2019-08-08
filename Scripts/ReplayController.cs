using UnityEngine;
using UnityEngine.UI;// we need this namespace in order to access UI elements within our script
using System.Collections;
using UnityEngine.SceneManagement; // neded in order to load scenes

public class ReplayController : MonoBehaviour {

	public Canvas replayMenu;
	public Button replayText;
	public Button exitText;
	Animator anim;

	// Use this for initialization
	void Start () {
		replayMenu = replayMenu.GetComponent<Canvas>();
		replayText = replayText.GetComponent<Button> ();
		exitText = exitText.GetComponent<Button> ();
		replayMenu.enabled = false;
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		anim.SetTrigger ("Replay");
	}

	public void ExitPress() //this function will be used on our Exit button

	{
		replayMenu.enabled = true; //enable the Quit menu when we click the Exit button
		replayText.enabled = false; //then disable the Play and Exit buttons so they cannot be clicked
		exitText.enabled = false;

	}

	public void NoPress() //this function will be used for our "NO" button in our Quit Menu

	{
		replayMenu.enabled = false; //we'll disable the quit menu, meaning it won't be visible anymore
		replayText.enabled = true; //enable the Play and Exit buttons again so they can be clicked
		exitText.enabled = true;

	}

	public void StartLevel () //this function will be used on our Play button

	{
		SceneManager.LoadScene (1); //this will load our first level from our build settings. "1" is the second scene in our game

	}

	public void ExitGame () //This function will be used on our "Yes" button in our Quit menu

	{
		Application.Quit(); //this will quit our game. Note this will only work after building the game

	}

}
	