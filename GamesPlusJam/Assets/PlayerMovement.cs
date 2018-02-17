using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
	public Rigidbody rb;

	public float forwardForce = 2000;

	void FixedUpdate()
	{
		//rb.AddForce(0,0, forwardForce * Time.deltaTime);

		if (Input.GetKey("w"))
		{
			rb.AddForce(0,0, forwardForce * Time.deltaTime);
		}

		if (Input.GetKey("a"))
		{
			rb.AddForce(-forwardForce * Time.deltaTime, 0, 0);
		}

		if (Input.GetKey("s"))
		{
			rb.AddForce(0,0, -forwardForce * Time.deltaTime);
		}

		if (Input.GetKey("d"))
		{
			rb.AddForce(forwardForce * Time.deltaTime, 0, 0);
		}


	}
}
