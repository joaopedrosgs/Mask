using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

	public List<string> Falas;
	public string Nome;
	public GameController controller;

	public bool LookAt;
	// Use this for initialization
	void Start () {
		controller = GameObject.Find("GameController").GetComponent<GameController>();
		
	}
	

	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player")) {
			controller.CriarDialogo(Falas, Nome);
			if(LookAt)
				transform.LookAt(other.transform);
		}
	}
}
