using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
	
	public GameController Controller;
	/// <summary>
	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player")){
			var item = gameObject.GetComponent<Item>();
			Controller.Pickup(item);
			Destroy(this.gameObject);
		}
		
	}
	// Use this for initialization
	void Awake () {
		Controller = GameObject.Find("GameController").GetComponent<GameController>();
	}
	
	// Update is called once per frame
}
