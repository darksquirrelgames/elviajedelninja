using UnityEngine;
using System.Collections;

public class JugadorControl : MonoBehaviour {

	public Rigidbody2D miRigidbody;

	Animator miAnimator;
	bool mirandoDerecha;
	bool enTierra;
	bool atacar = false;
	bool saltar = false;
	bool deslizar;

	public float radioTierra = 0.2f;
	public float velocidadMovimiento = 10f;
	public float fuerzaSalto = 1000f;
	public Transform[] chequeoTierra;
	public LayerMask queEsTierra;

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
		miAnimator.SetBool ("tierra", enTierra);
		miAnimator.SetFloat ("velocidadVertical", miRigidbody.velocity.y);
		float horizontal = Input.GetAxis ("Horizontal");
		Movimiento (horizontal);
		Girar (horizontal);
		Ataques ();
		ResetearValores ();
	}

	void Ingreso (){
		if (Input.GetKeyDown (KeyCode.Space)){
			saltar = true;
		}
		if (Input.GetKeyDown (KeyCode.LeftShift)){
			atacar = true;
		}
		if (Input.GetKeyDown (KeyCode.LeftControl)) {
			miAnimator.SetTrigger ("deslizar");
			deslizar = true;
		}
	}

	void Movimiento (float horizontal){
		if (enTierra && !miAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Atacar")){
			miRigidbody.velocity = new Vector2 (horizontal * velocidadMovimiento, miRigidbody.velocity.y);
			miAnimator.SetFloat ("velocidad", Mathf.Abs (miRigidbody.velocity.x));
		}
		if (saltar && enTierra){
			miRigidbody.AddForce (Vector2.up*fuerzaSalto);
		}
	}

	void Ataques (){
		if (atacar && enTierra && !miAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Atacar")){
			miAnimator.SetTrigger ("atacar");
			miRigidbody.velocity = Vector2.zero;
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
					return true;
				}
			}
		}
		return false;
	}

	void ResetearValores (){
		saltar = false;
		atacar = false;
	}
}
