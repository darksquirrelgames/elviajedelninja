using UnityEngine;
using System.Collections;

public class Jugador : Personaje {

	static Jugador instancia;
	public static Jugador Instancia {
		get{
			if (instancia == null){
				instancia = GameObject.FindObjectOfType<Jugador> ();
			}
			return instancia;
		}
	}

	public Rigidbody2D MiRigidbody { get; set; }
	[SerializeField] Transform[] chequeoTierra;
	[SerializeField] LayerMask queEsTierra;
	[SerializeField] Transform puntoInicio;

	public bool Saltar { get; set; }
	public bool Deslizarse { get; set; }
	public bool EnTierra { get; set; }
	public bool controlAereo = false;

	[SerializeField] float radioTierra = 0.2f;
	[SerializeField] float fuerzaSalto = 1000f;

	public override void Start (){
		base.Start ();
		MiRigidbody = GetComponent<Rigidbody2D> ();
		transform.position = puntoInicio.position;
	}

	void Update (){
		Ingreso ();
	}

	void FixedUpdate (){
		float horizontal = Input.GetAxis ("Horizontal");
		EnTierra = EstaEnTierra ();
		Movimiento (horizontal);
		Girar (horizontal);
		CambiarCapasAnimacion ();
	}

	void Ingreso (){
		if (Input.GetKeyDown (KeyCode.Space)){
			MiAnimator.SetTrigger ("saltar");
		}
		if (Input.GetKeyDown (KeyCode.LeftShift)){
			MiAnimator.SetTrigger ("atacar");
		}
		if (Input.GetKeyDown (KeyCode.LeftControl)){
			MiAnimator.SetTrigger ("lanzar");
		}
		if (Input.GetKeyDown (KeyCode.LeftAlt)){
			MiAnimator.SetTrigger ("deslizarse");
		}
	}

	void Movimiento (float horizontal){
		if (MiRigidbody.velocity.y < 0){
			MiAnimator.SetBool ("aterrizar", true);
		}
		if (!Atacar && !Deslizarse && (EnTierra || controlAereo)){
			MiRigidbody.velocity = new Vector2 (horizontal * velocidadMovimiento, MiRigidbody.velocity.y);
		}
		if (Saltar && MiRigidbody.velocity.y == 0){
			MiRigidbody.AddForce (new Vector2(0, fuerzaSalto));
		}
		MiAnimator.SetFloat ("velocidad", Mathf.Abs(horizontal));
	}

	void Girar (float horizontal){
		if (horizontal > 0 && !mirandoDerecha || horizontal < 0 && mirandoDerecha){
			CambiarDireccion ();
		}
	}

	bool EstaEnTierra (){
		foreach (Transform punto in chequeoTierra){
			Collider2D[] colisiones = Physics2D.OverlapCircleAll (punto.position, radioTierra, queEsTierra);
			for (int i = 0; i < colisiones.Length; i++) {
				if (colisiones[i].gameObject != gameObject){
					return true;
				}
			}
		}
		return false;
	}

	public override void LanzarObjeto (int capa){
		if (!EnTierra && capa == 1 || EnTierra && capa == 0) {
			base.LanzarObjeto (capa);
		}
	}

	void CambiarCapasAnimacion(){
		if (!EnTierra){
			MiAnimator.SetLayerWeight (1,1f);
		}else{
			MiAnimator.SetLayerWeight (1,0.0f);
		}
	}
}
