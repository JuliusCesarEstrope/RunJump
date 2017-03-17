using UnityEngine;
using System.Collections;

public class Win : MonoBehaviour {

	public bool win = false;

	void OnTriggerEnter2D(Collider2D other){

		Debug.Log ("Win Collision");

		if (other.tag == "Player")

			win = true;

	}

	void OnGUI(){

		if (win) {
			
			Debug.Log ("You win!");

			if(GUI.Button(new Rect (Screen.width/2-50, Screen.height/2-50, 100, 50), "You Win!\nPlay Again"))
				Application.LoadLevel (0);
			
		}

	}

}
