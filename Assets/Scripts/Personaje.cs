using UnityEngine;
using System.Collections;

public abstract class Personaje : MonoBehaviour {

	public Animator MiAnimator { get; private set; }
	[SerializeField] protected Transform arrojablePosicion;
	[SerializeField] protected GameObject arrojablePrefab;

	[SerializeField] protected float velocidadMovimiento = 10f;

	protected bool mirandoDerecha;
	public bool Atacar { get; set; }

	public virtual void Start (){
		mirandoDerecha = true;
		MiAnimator = GetComponent<Animator> ();
	}

	public void CambiarDireccion (){
		mirandoDerecha = !mirandoDerecha;
		transform.localScale = new Vector3 (transform.localScale.x*-1, transform.localScale.y, transform.localScale.z);
	}

	public virtual void LanzarObjeto (int capa){
		Vector3 rotacion;
		Vector2 direccion;
		if (mirandoDerecha) {
			rotacion = new Vector3 (0, 0, -90);
			direccion = Vector2.right;
		} else {
			rotacion = new Vector3 (0, 0, 90);
			direccion = Vector2.left;
		}
		GameObject tmp = (GameObject)Instantiate (arrojablePrefab, arrojablePosicion.position, Quaternion.Euler (rotacion));
		tmp.GetComponent<ObjetoArrojable> ().Iniciar (direccion);
	}

}
