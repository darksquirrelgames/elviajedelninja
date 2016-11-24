using UnityEngine;
using System.Collections;

public class PlataformaControl : MonoBehaviour {

	private BoxCollider2D jugadorCollider;
	[SerializeField] private BoxCollider2D plataformaTriggerCollider;
	[SerializeField] private BoxCollider2D plataformaCollider;

	void Start () {
		jugadorCollider = GameObject.Find ("Jugador").GetComponent<BoxCollider2D> ();
		Physics2D.IgnoreCollision (plataformaTriggerCollider, plataformaCollider, false);
	}

	void OnTriggerEnter2D (Collider2D otro){
		if (otro.gameObject.name == "Jugador"){
			Physics2D.IgnoreCollision (jugadorCollider, plataformaCollider, true);
		}
	}

	void OnTriggerExit2D (Collider2D otro){
		if (otro.gameObject.name == "Jugador"){
			Physics2D.IgnoreCollision (jugadorCollider, plataformaCollider, false);
		}
	}
}
