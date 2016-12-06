using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoIdle : IEstadoEnemigo {

	Enemigo enemigo;
	float tiempoIdle;
	float duracionIdle = 10;


	public void Enter (Enemigo enemigo) {
		this.enemigo = enemigo;
	}

	public void Execute () {
		Idle ();
		if (enemigo.Objetivo != null){
			enemigo.CambiarEstado (new EstadoPatrulla ());
		}
	}

	public void Exit () {
	}

	public void OnTriggerEnter (Collider2D otro) {
	}

	void Idle () {
		enemigo.MiAnimator.SetFloat ("velocidad", 0);
		tiempoIdle += Time.deltaTime;
		if (tiempoIdle >= duracionIdle) {
			enemigo.CambiarEstado (new EstadoPatrulla ());
		}
	}

}
