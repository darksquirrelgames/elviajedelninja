using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoPatrulla : IEstadoEnemigo {

	Enemigo enemigo;
	float tiempoPatrulla;
	float duracionPatrulla = 10;

	public void Enter (Enemigo enemigo) {
		this.enemigo = enemigo;
	}

	public void Execute () {
		Patrullar ();
		enemigo.Mover ();
		if (enemigo.Objetivo != null && enemigo.EnRangoLanzar){
			enemigo.CambiarEstado (new EstadoRango ());
		}
	}
		
	public void Exit () {
	}

	public void OnTriggerEnter (Collider2D otro) {
		if (otro.CompareTag ("Borde")){
			enemigo.CambiarDireccion ();
		}
	}

	void Patrullar () {
		tiempoPatrulla += Time.deltaTime;
		if (tiempoPatrulla >= duracionPatrulla) {
			enemigo.CambiarEstado (new EstadoIdle ());
		}
	}
}
