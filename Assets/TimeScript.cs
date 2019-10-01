using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeScript : MonoBehaviour {
	private float time = 60;
	public ballScript BallScript;

	public GameObject exchangeButton;
	public GameObject gameOverText;

	
	void Start () {
 
		gameOverText.SetActive(false);

		GetComponent<Text>().text = ((int)time).ToString();
	}
	
	void Update ()
	{
		time -= Time.deltaTime;

		if (time < 0) {
			StartCoroutine("GameOver");
		}

		if (time < 0) time = 0;
		GetComponent<Text> ().text = ((int)time).ToString ();
	}
 
	IEnumerator GameOver () {
		gameOverText.SetActive(true);
		exchangeButton.GetComponent<Button>().interactable = false;
		BallScript.isPlaying = false;
		yield return new WaitForSeconds(2.0f);
		if (Input.GetMouseButtonDown (0)) {
			Application.LoadLevel ("title");
		}
	}
 
}