using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
	public float speed;
	private Rigidbody rb;
	private int count;
	public Text counttext;
	public Text wintext;
	private IEnumerator coroutine;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		count = 0;
		counttext.text = "Count: " + count.ToString ();
		wintext.text = "Avoid touching inner walls of maze";
		coroutine = startingtext (3.0f);
		StartCoroutine (coroutine);


	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.AddForce (movement*speed);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Pick Up")) {
			other.gameObject.SetActive (false);
			count = count + 1;
			counttext.text = "Count: " + count.ToString ();
		}
		if (count >= 11) {
			
			coroutine=waiting(3.0f);
			StartCoroutine (coroutine);
		}
	}
	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag ("InWalls")) {
			wintext.text = "You Lose :(";
			//yield return new WaitForSeconds (4);
			coroutine=WaitandPrint(3.0f);
			StartCoroutine (coroutine);
			//SceneManager.SetActiveScene (SceneManager.GetActiveScene ().name);
			//SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}
	}
	private IEnumerator startingtext(float waiting)
	{
		yield return new WaitForSecondsRealtime (waiting);
		wintext.text = "";
	}
	private IEnumerator WaitandPrint(float waitTime)
	{
		
			yield return new WaitForSecondsRealtime (waitTime);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		
	}
	private IEnumerator waiting(float waitTime)
	{
		wintext.text = "You Win!!";
		yield return new WaitForSecondsRealtime (waitTime);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);

	}
}
