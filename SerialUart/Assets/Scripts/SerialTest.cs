using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerialTest : MonoBehaviour {

	SerialHandler serial;

	// Use this for initialization
	void Start () {
		
		serial = GetComponent<SerialHandler> ();
		serial.OnReceivedLine += ReceivedLine;
		serial.OnReceivedByte += ReceivedByte;
		serial.Connect ("/dev/cu.usbmodem1421");
	
		StartCoroutine (Write ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void ReceivedByte(byte[] data){
		for (int i = 0; i < data.Length; i++) {
			Debug.Log (data [i]);
		}
	}

	void ReceivedLine(string data){
		Debug.Log (data);
	}


	IEnumerator Write(){

//		while (true){
//			yield return new WaitForSeconds (5f);
//			serial.WriteLine ("ON");
//
//			yield return new WaitForSeconds (5f);
//			serial.WriteLine ("OFF");
//		}
	}
}
