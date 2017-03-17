using UnityEngine;
using System.Collections;

public class Handler : MonoBehaviour {

	GameObject start;

	public float deaths = 0f;

	void Start (){

		start = GameObject.Find("StartPosition");

	}

	void OnTriggerEnter2D(Collider2D other){

		Debug.Log ("Collision");

		if (other.tag == "Player") {

			deaths += 1;

			other.transform.position = new Vector2(start.transform.position.x, start.transform.position.y);

		}

	}


	void OnGUI(){

		string message = "Deaths: " + deaths;

		Debug.Log (message);

		GUI.Label (new Rect(0,0,100,30), message, "box");

	}

}
