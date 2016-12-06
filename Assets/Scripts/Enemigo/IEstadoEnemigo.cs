using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEstadoEnemigo {

	void Enter (Enemigo enemigo);
	void Execute ();
	void Exit ();
	void OnTriggerEnter (Collider2D otro);
}
