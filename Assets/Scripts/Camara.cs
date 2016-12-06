using UnityEngine;
using System.Collections;

public class Camara : MonoBehaviour {

	public Vector3 posicionMinima;
	public Vector3 posicionMaxima;

	private Transform objetivo;

	void Start () {
		objetivo = GameObject.Find ("Jugador").transform;
	}

	void LateUpdate () {
		Vector3 posicion = new Vector3 ();
		posicion.x = Mathf.Clamp (objetivo.position.x, posicionMinima.x, posicionMaxima.x);
		posicion.y = Mathf.Clamp (objetivo.position.y, posicionMinima.y, posicionMaxima.y);
		posicion.z = Mathf.Clamp (objetivo.position.z, posicionMinima.z, posicionMaxima.z);
		transform.position = posicion;
	}

	public void SetPosicionMinima () {
		posicionMinima = gameObject.transform.position;
	}

	public void SetPosicionMaxima () {
		posicionMaxima = gameObject.transform.position;
	}

}
