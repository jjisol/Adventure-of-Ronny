using UnityEngine;

using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	public static int score;

	Text text;

	void Awake()
	{
		text = transform.GetComponent<Text> ();
		score = 0;
	}

	void Update()
	{
		text.text = "Score: " + score;

	}

}
