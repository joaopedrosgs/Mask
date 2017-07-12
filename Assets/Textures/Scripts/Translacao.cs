using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translacao : MonoBehaviour {
	
	public GameObject Player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Player)
		     transform.RotateAround (Player.transform.position,Vector3.up, 30f * Time.deltaTime);
			 transform.LookAt(Camera.main.transform);
		

		
	}
}
