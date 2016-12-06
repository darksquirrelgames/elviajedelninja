using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour {

	[SerializeField] Enemigo enemigo;

	void OnTriggerEnter2D (Collider2D otro){
		if (otro.CompareTag ("Player")){
			enemigo.Objetivo = otro.gameObject;
		}
	}

	void OnTriggerExit2D (Collider2D otro){
		if (otro.CompareTag ("Player")){
			enemigo.Objetivo = null;
		}
	}
}
