using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnorarColisiones : MonoBehaviour {

	[SerializeField] Collider2D miCollider;

	void Awake (){
		Physics2D.IgnoreCollision (GetComponent<Collider2D> (), miCollider, true);
	}
}
