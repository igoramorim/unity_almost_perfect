using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forward, Color.green);
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Platform") {
			Debug.Log("Colision");
		}
	}

	
}
