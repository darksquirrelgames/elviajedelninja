using UnityEngine;
using System.Collections;

public class JugadorControl : MonoBehaviour {

	Rigidbody2D miRigidbody;
	Animator miAnimator;

	bool mirandoDerecha;
	bool enTierra;
	bool saltar = false;
	bool atacar = false;
	bool atacarAire = false;
	bool lanzar = false;

	public float radioTierra = 0.2f;
	public float velocidadMovimiento = 10f;
	public float fuerzaSalto = 1000f;
	public Transform[] chequeoTierra;
	public LayerMask queEsTierra;
	public GameObject cuchilloPrefab;

	void Start (){
		mirandoDerecha = true;
		miRigidbody = GetComponent<Rigidbody2D> ();
		miAnimator = GetComponent<Animator> ();
	}

	void Update (){
		Ingreso ();
	}

	void FixedUpdate (){
		enTierra = EstaEnTierra ();
		float horizontal = Input.GetAxis ("Horizontal");
		Movimiento (horizontal);
		Girar (horizontal);
		Ataques ();
		CambiarCapasAnimacion ();
		ResetearValores ();
	}

	void Ingreso (){
		if (Input.GetKeyDown (KeyCode.Space)){
			saltar = true;
		}
		if (Input.GetKeyDown (KeyCode.LeftShift)){
			atacar = true;
			atacarAire = true;
		}
		if (Input.GetKeyDown (KeyCode.LeftControl)){
			lanzar = true;
		}
	}

	void Movimiento (float horizontal){
		if (miRigidbody.velocity.y < 0.0f){
			miAnimator.SetBool ("aterrizar", true);
		}
		if (enTierra && !miAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Atacar") && !miAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Lanzar")){
			miRigidbody.velocity = new Vector2 (horizontal * velocidadMovimiento, miRigidbody.velocity.y);
			miAnimator.SetFloat ("velocidad", Mathf.Abs (miRigidbody.velocity.x));
		}
		if (saltar && enTierra){
			enTierra = false;
			miAnimator.SetTrigger ("despegar");
			miRigidbody.AddForce (Vector2.up*fuerzaSalto);
		}
	}

	void Ataques (){
		if (atacar && enTierra && !miAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Atacar")){
			miAnimator.SetTrigger ("atacar");
			miRigidbody.velocity = Vector2.zero;
		}
		if (lanzar && !miAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Lanzar") && !miAnimator.GetCurrentAnimatorStateInfo (1).IsName ("LanzarAire")) {
			miAnimator.SetTrigger ("lanzar");
			miRigidbody.velocity = Vector2.zero;
		}
		if (atacarAire && !enTierra && !miAnimator.GetCurrentAnimatorStateInfo (1).IsName ("AtacarAire")){
			miAnimator.SetBool ("atacarAire", true);
		}
		if (!atacarAire && !miAnimator.GetCurrentAnimatorStateInfo (1).IsName ("AtacarAire")){
			miAnimator.SetBool ("atacarAire", false);
		}
	}

	void Girar (float horizontal){
		if ((horizontal > 0 && !mirandoDerecha || horizontal < 0 && mirandoDerecha) && enTierra && !atacar){
			mirandoDerecha = !mirandoDerecha;
			Vector3 miEscala = transform.localScale;
			miEscala.x *= -1;
			transform.localScale = miEscala;
		}
	}

	bool EstaEnTierra (){
		foreach (Transform punto in chequeoTierra){
			Collider2D[] colisiones = Physics2D.OverlapCircleAll (punto.position, radioTierra, queEsTierra);
			for (int i = 0; i < colisiones.Length; i++) {
				if (colisiones[i].gameObject.tag.Equals ("Piso") || colisiones[i].gameObject.tag.Equals ("Plataforma")){
					miAnimator.ResetTrigger ("despegar");
					miAnimator.SetBool ("aterrizar", false);
					return true;
				}
			}
		}
		return false;
	}

	public void LanzarObjeto(int capa){
		Vector3 rotacion;
		Vector2 direccion;
		if (!enTierra && capa == 1 || enTierra && capa == 0) {
			if (mirandoDerecha) {
				rotacion = new Vector3 (0, 0, -90);
				direccion = Vector2.right;
			} else {
				rotacion = new Vector3 (0, 0, 90);
				direccion = Vector2.left;
			}
			GameObject tmp = (GameObject)Instantiate (cuchilloPrefab, transform.position, Quaternion.Euler (rotacion));
			tmp.GetComponent<ObjetoArrojable> ().Iniciar (direccion);
		}
	}

	void ResetearValores (){
		saltar = false;
		atacar = false;
		atacarAire = false;
		lanzar = false;
	}

	void CambiarCapasAnimacion(){
		if (!enTierra){
			miAnimator.SetLayerWeight (1,1f);
		}else{
			miAnimator.SetLayerWeight (1,0.0f);
		}
	}
}
