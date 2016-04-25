using UnityEngine;
using System.Collections;

public class cSceneMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if( Input.GetKeyDown( KeyCode.Return )){
			GameObject fade = GameObject.Find ("Fade");
			cFadeInOut fadeInOut = fade.GetComponent< cFadeInOut > ();

			fadeInOut.SetFadeState (cFadeInOut.eFadeState.FadeOut);

			Destroy (gameObject, .001f);
		}
	}
}
