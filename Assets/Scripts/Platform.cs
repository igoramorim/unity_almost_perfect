using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Platform : MonoBehaviour {

	public enum Direction {
		Up,
		Down,
		Left,
		Right
	};

	public bool isActive;
	public Direction direction;
	public float moveSpeed;
	public float duration;
	public float movementCycles;
	public Image cycleTimer;
    public GameObject player;

	private float currentSwitchCount;
	private float totalSwitchCount;

	void Start() {
		currentSwitchCount = 0;
		totalSwitchCount = movementCycles * 2;
		cycleTimer.fillAmount = 0;
	}
	
	void Update() {
		if (Input.GetKeyDown(KeyCode.E)) {
			if (!isActive) {
				Debug.Log("ATIVOU!");				
				Activate();	
			}
		}

		if (isActive) {
			Move();
		}
	}

	void Move() {
		if (direction == Direction.Up) {
			transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
		} else if (direction == Direction.Down) {
			transform.Translate(Vector3.down * Time.deltaTime * moveSpeed);
		} else if (direction == Direction.Left) {
			transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
		} else if (direction == Direction.Right) {
			transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
		}
	}

	void SwitchDirection() {
		currentSwitchCount++;
		UpdateUI();		
		Debug.Log(currentSwitchCount);

		if (IsCyclesDone()) {
			Deactivate();
		}

		if (direction == Direction.Up) {
			direction = Direction.Down;
		} else if (direction == Direction.Down) {
			direction = Direction.Up;
		} else if (direction == Direction.Left) {
			direction = Direction.Right;
		} else if (direction == Direction.Right) {
			direction = Direction.Left;
		}
	}

	IEnumerator SwitchDirectionCoroutine() {
		// Debug.Log("entrou coroutine");
		while (isActive) {
			// Debug.Log("entrou coroutine INSIDE LOOP");			
			yield return new WaitForSeconds(duration);
			SwitchDirection();
		}
	}

	bool IsCyclesDone() {
		bool result =  currentSwitchCount >= totalSwitchCount ? true : false;
		Debug.Log(result);
		return result;
	}

	void Deactivate() {
		isActive = false;
	}

	void Activate() {
		isActive = true;
		currentSwitchCount = 0;
		cycleTimer.fillAmount = 0;
		StartCoroutine(SwitchDirectionCoroutine());		
	}

	void UpdateUI() {
		cycleTimer.fillAmount = currentSwitchCount/totalSwitchCount;
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Player") {
			Debug.Log("Colision");
		}
	}

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            other.transform.parent = transform;
            Debug.Log("OPA!");
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player") {
            other.transform.parent = null;
            Debug.Log("OPA SAIU!");
        }
    }

}
