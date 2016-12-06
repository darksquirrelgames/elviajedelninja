using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoRango : IEstadoEnemigo {

	Enemigo enemigo;
	float arrojarTiempo;
	float arrojarEspera = 3;
	bool puedeArrojar = true;

	public void Enter (Enemigo enemigo) {
		this.enemigo = enemigo;
	}

	public void Execute () {
		ArrojarObjeto ();
		if (enemigo.EnRangoAtaque){
			enemigo.CambiarEstado (new EstadoAtaque ());
		}else if (enemigo.Objetivo != null){
			enemigo.Mover ();
		}else{
			enemigo.CambiarEstado (new EstadoIdle ());
		}
		
	}

	public void Exit () {
	}

	public void OnTriggerEnter (Collider2D otro) {
	}

	void ArrojarObjeto (){
		arrojarTiempo += Time.deltaTime;
		if (arrojarTiempo >= arrojarEspera){
			puedeArrojar = true;
			arrojarTiempo = 0;
		}
		if (puedeArrojar){
			puedeArrojar = false;
			enemigo.MiAnimator.SetTrigger ("lanzar");
		}
	}

}
