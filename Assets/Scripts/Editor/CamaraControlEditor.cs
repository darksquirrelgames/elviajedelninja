using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof (CamaraControl))]
public class CamaraControlEditor : Editor {

	public override void OnInspectorGUI (){
		DrawDefaultInspector ();
		CamaraControl camaraControl = (CamaraControl) target;
		if(GUILayout.Button ("Posición Mínima")){
			camaraControl.SetPosicionMinima ();
		}
		if(GUILayout.Button ("Posición Máxima")){
			camaraControl.SetPosicionMaxima ();
		}
	}
	
}
