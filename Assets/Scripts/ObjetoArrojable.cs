using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
public class ObjetoArrojable : MonoBehaviour {

	Rigidbody2D miRigidbody;
	float velocidad = 10f;
	Vector2 direccion;
	float tiempoVida = 5;

	public void Start () {
		miRigidbody = GetComponent<Rigidbody2D> ();
		Destroy (gameObject, tiempoVida);
	}

	public void FixedUpdate (){
		miRigidbody.velocity = direccion * velocidad;
	}

	public void Iniciar(Vector2 direccion){
		this.direccion = direccion;
	}

}
