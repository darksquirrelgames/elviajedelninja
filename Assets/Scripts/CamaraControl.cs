using UnityEngine;
using System.Collections;

public class CamaraControl : MonoBehaviour {

	[SerializeField] private float xMin;
	[SerializeField] private float xMax;
	[SerializeField] private float yMin;
	[SerializeField] private float yMax;

	private Transform objetivo;

	void Start () {
		objetivo = GameObject.Find ("Jugador").transform;
	}

	void LateUpdate () {
		Vector3 posicion = new Vector3 ();
		posicion.x = Mathf.Clamp (objetivo.position.x, xMin, xMax);
		posicion.y = Mathf.Clamp (objetivo.position.y, yMin, yMax);
		posicion.z = transform.position.z;
		transform.position = posicion;
	}

}
