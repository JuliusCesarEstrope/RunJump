using UnityEngine;
using System.Collections;

public class CharacterMove : MonoBehaviour {

	GameObject Character;
	GameObject special;

	public bool Jump;

	public float jumpSpeed;
	public float moveSpeed;
	public float maxYSpeed;

	public bool movable = true;
	public float xSpeed;
	public float ySpeed;

	public bool grounded = false;
	public float groundRadius = 0.2f;
	public Transform groundCheck;
	public LayerMask whatIsGround; 

	public bool playerWins = false;

	void Start () {

		jumpSpeed = 10f;
		moveSpeed = 3f;
		maxYSpeed = -15f;
			
	//	Physics2D.IgnoreLayerCollision (9, 9, true);

	}

	void Update () {

		//define needed GameObjects for the script to run
		Character = GameObject.Find ("Player");
		special = GameObject.Find ("special");

		//Define Win object (script) in order to call variables from it
		Win winScript = special.GetComponent<Win>();

		//Check if player has won
		playerWins = winScript.win;

		//user input to control character jumps
		if ((Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended) || Input.GetKeyDown ("space"))
			Jump = true;
		else
			Jump = false;

		//set X and Y speeds to be current speeds
		xSpeed = (float)Character.GetComponent<Rigidbody2D>().velocity.x;
		ySpeed = (float)Character.GetComponent<Rigidbody2D>().velocity.y;

		//controls grounded status based on Y velocity
		if (ySpeed < 0.01 && ySpeed > -0.1)
			grounded = true;
		else {

			ySpeed += Physics2D.gravity.y * Time.deltaTime;
			grounded = false;

		}

		//check if current Y velocity is greater than max Y velocity
		if (ySpeed < maxYSpeed) {

			ySpeed = maxYSpeed;

		}

		//check if player is not moving and has not won the game yet
		if (xSpeed < 0.1 && !playerWins) {

			//if so, teleport the player upwards (fixes issues getting stuck)
			Character.transform.position = new Vector2 ((float)Character.transform.position.x, (float)Character.transform.position.y + 0.01f);

		}
		
		if (grounded && Jump && !playerWins)
			moveJump ();
		else if (!playerWins)
			move ();
		else
			Stop ();

	}

	public void OnTriggerEnter2D(Collider2D other){

		if (other.tag == "Platform") {

			ySpeed = 0f;

			grounded = true;

		}

	}
	
	public void moveJump() {
		
		if (movable) {
			Character.GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveSpeed, jumpSpeed);
		}

	}

	public void move(){

		Character.GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveSpeed, ySpeed);

	}

	public void Stop(){

		Character.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, 0f);

	}

}