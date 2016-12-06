using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : Personaje {

	private IEstadoEnemigo estadoActual;
	public GameObject Objetivo { get; set; }
	[SerializeField] float rangoAtaque;
	[SerializeField] float rangoLanzar;
	public bool EnRangoAtaque {
		get{
			if (Objetivo != null){
				return Vector2.Distance (transform.position, Objetivo.transform.position) <= rangoAtaque;
			}
			return false;
		}
	}

	public bool EnRangoLanzar {
		get{
			if (Objetivo != null){
				return Vector2.Distance (transform.position, Objetivo.transform.position) <= rangoLanzar;
			}
			return false;
		}
	}

	// Use this for initialization
	public override void Start () {
		base.Start ();
		CambiarEstado (new EstadoIdle ());
	}
	
	// Update is called once per frame
	void Update () {
		estadoActual.Execute ();
		MirarObjetivo ();
	}

	void MirarObjetivo (){
		if (Objetivo != null){
			float posX = Objetivo.transform.position.x - transform.position.x;
			if (posX < 0 && mirandoDerecha || posX > 0 && !mirandoDerecha){
				CambiarDireccion ();
			}
		}
	}

	public void CambiarEstado (IEstadoEnemigo estadoNuevo) {
		if (estadoActual != null) {
			estadoActual.Exit ();
		}
		estadoActual = estadoNuevo;
		estadoActual.Enter (this);
	}

	public void Mover () {
		if (!Atacar) {
			MiAnimator.SetFloat ("velocidad", 1);
			transform.Translate (Direccion () * (velocidadMovimiento * Time.deltaTime));
		}
	}

	public Vector2 Direccion (){
		return mirandoDerecha ? Vector2.right : Vector2.left;
	}
		
	public void OnTriggerEnter2D (Collider2D otro){
		estadoActual.OnTriggerEnter (otro);
	}
}
