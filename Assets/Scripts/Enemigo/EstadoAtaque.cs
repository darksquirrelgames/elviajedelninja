using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoAtaque : IEstadoEnemigo {

	float atacarTiempo;
	float atacarEspera = 3;
	bool puedeAtacar = true;
	Enemigo enemigo;
	
	public void Enter (Enemigo enemigo) {
		this.enemigo = enemigo;
	}

	public void Execute () {
		Atacar ();
		if (enemigo.EnRangoLanzar && !enemigo.EnRangoAtaque){
			enemigo.CambiarEstado (new EstadoRango ());
		}else if (enemigo.Objetivo == null){
			enemigo.CambiarEstado (new EstadoIdle ());
		}
	}

	public void Exit () {
	}

	public void OnTriggerEnter (Collider2D otro) {
	}

	void Atacar (){
		atacarTiempo += Time.deltaTime;
		if (atacarTiempo >= atacarEspera){
			puedeAtacar = true;
			atacarTiempo = 0;
		}
		if (puedeAtacar){
			puedeAtacar = false;
			enemigo.MiAnimator.SetTrigger ("atacar");
		}
	}



}
