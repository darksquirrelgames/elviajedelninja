using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof (Camara))]
public class CamaraControlEditor : Editor {

	public override void OnInspectorGUI (){
		DrawDefaultInspector ();
		Camara camara = (Camara) target;
		if(GUILayout.Button ("Posición Mínima")){
			camara.SetPosicionMinima ();
		}
		if(GUILayout.Button ("Posición Máxima")){
			camara.SetPosicionMaxima ();
		}
	}

}