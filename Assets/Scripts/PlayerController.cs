using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public Transform target;
	//Player Variables
	public float moveSpeed;
	public float jumpForce;

	private Rigidbody2D triRb;

	//Aim Line Variables
	public Transform aimLine;
	private float MinZRot, MaxZRot;
	private float ZRot;

	//Jump Variables
	public float Vi = 75;
	float angle;

	void Start () {
		MinZRot = -180.0f;
		MaxZRot = 180.0f;
		ZRot = 0;

		triRb = GetComponent<Rigidbody2D> ();
	}

	void Update(){
		//ZRot = -Mathf.Atan2(Input.mousePosition.x - aimLine.position.x, Input.mousePosition.y - aimLine.position.y) * (180 / Mathf.PI);
		//ZRot = Mathf.Clamp(ZRot, MinZRot, MaxZRot);
		//aimLine.eulerAngles = new Vector3(aimLine.eulerAngles.x, aimLine.eulerAngles.y, ZRot);

		Vector2 targetDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - target.position;
		angle = Vector2.Angle( targetDir, transform.right );
		Debug.Log (angle);
		//aimLine.LookAt (target);
	}
		
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxisRaw ("Horizontal");
		triRb.angularVelocity = (moveHorizontal * -1) * moveSpeed;

		if (Input.GetKeyDown (KeyCode.Space)) {
			//triRb.AddForce (new Vector2(0, 1 * jumpForce),ForceMode2D.Impulse);
			transform.rotation = Quaternion.identity;
		}

		if(Input.GetKeyUp(KeyCode.Space)){
			Jump();
		}
	}

	void Jump(){
		float Vy, Vx;   // y,z components of the initial velocity

		Vy = Vi * Mathf.Sin(Mathf.Deg2Rad * angle);
		Vx = Vi * Mathf.Cos(Mathf.Deg2Rad * angle);

		// create the velocity vector in local space
		Vector2 localVelocity = new Vector2(Vx, Vy);

		// transform it to global vector
		Vector3 globalVelocity = transform.TransformVector(localVelocity);

		// launch the triangle by setting its initial velocity
		triRb.velocity = globalVelocity;
	}

	void OnCollisionEnter2D(Collision2D coll) 
	{
		
		if(coll.transform.tag == "platform")
		{
			Debug.Log("HIT!");

			transform.position = coll.contacts[0].point;
		}
	}

	}