using UnityEngine;
using System.Collections;

public class JugadorControl : MonoBehaviour {

	private static JugadorControl jugador;
	public static JugadorControl Jugador {
		get{
			if (jugador == null){
				jugador = GameObject.FindObjectOfType<JugadorControl> ();
			}
			return jugador;
		}
	}

	public Rigidbody2D MiRigidbody { get; set; }
	public bool Saltar { get; set; }
	public bool Atacar { get; set; }
	public bool Deslizarse { get; set; }
	public bool EnTierra { get; set; }
	public bool controlAereo = false;

	Animator miAnimator;

	bool mirandoDerecha;

	public float radioTierra = 0.2f;
	public float velocidadMovimiento = 10f;
	public float fuerzaSalto = 1000f;
	public Transform[] chequeoTierra;
	public LayerMask queEsTierra;
	public GameObject cuchilloPrefab;
	public Transform cuchilloPosision;

	void Start (){
		mirandoDerecha = true;
		MiRigidbody = GetComponent<Rigidbody2D> ();
		miAnimator = GetComponent<Animator> ();
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
			miAnimator.SetTrigger ("saltar");
		}
		if (Input.GetKeyDown (KeyCode.LeftShift)){
			miAnimator.SetTrigger ("atacar");
		}
		if (Input.GetKeyDown (KeyCode.LeftControl)){
			miAnimator.SetTrigger ("lanzar");
		}
		if (Input.GetKeyDown (KeyCode.LeftAlt)){
			miAnimator.SetTrigger ("deslizarse");
		}
	}

	void Movimiento (float horizontal){
		if (MiRigidbody.velocity.y < 0){
			miAnimator.SetBool ("aterrizar", true);
		}
		if (!Atacar && !Deslizarse && (EnTierra || controlAereo)){
			MiRigidbody.velocity = new Vector2 (horizontal * velocidadMovimiento, MiRigidbody.velocity.y);
		}
		if (Saltar && MiRigidbody.velocity.y == 0){
			MiRigidbody.AddForce (new Vector2(0, fuerzaSalto));
		}
		miAnimator.SetFloat ("velocidad", Mathf.Abs(horizontal));
	}

	void Girar (float horizontal){
		if (horizontal > 0 && !mirandoDerecha || horizontal < 0 && mirandoDerecha){
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
				if (colisiones[i].gameObject != gameObject){
					return true;
				}
			}
		}
		return false;
	}

	public void LanzarObjeto(int capa){
		Vector3 rotacion;
		Vector2 direccion;
		if (!EnTierra && capa == 1 || EnTierra && capa == 0) {
			if (mirandoDerecha) {
				rotacion = new Vector3 (0, 0, -90);
				direccion = Vector2.right;
			} else {
				rotacion = new Vector3 (0, 0, 90);
				direccion = Vector2.left;
			}
			GameObject tmp = (GameObject)Instantiate (cuchilloPrefab, cuchilloPosision.position, Quaternion.Euler (rotacion));
			tmp.GetComponent<ObjetoArrojable> ().Iniciar (direccion);
		}
	}

	void CambiarCapasAnimacion(){
		if (!EnTierra){
			miAnimator.SetLayerWeight (1,1f);
		}else{
			miAnimator.SetLayerWeight (1,0.0f);
		}
	}
}
