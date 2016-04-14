using UnityEngine;
using System.Collections;

public class cPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			GameObject.Find ("BlockManager").GetComponent< cBlockManager > ().MoveRight ();
		}
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			GameObject.Find ("BlockManager").GetComponent< cBlockManager > ().MoveLeft ();
		}

		if (Input.GetKeyDown (KeyCode.A)) {
			GameObject.Find ("BlockManager").GetComponent< cBlockManager > ().RotateLeft ();
		}
		if (Input.GetKeyDown (KeyCode.S)) {
			GameObject.Find ("BlockManager").GetComponent< cBlockManager > ().RotateRight ();
		}

		if (Input.GetKey (KeyCode.DownArrow)) {
			GameObject.Find ("BlockManager").GetComponent< cBlockManager > ().DownSpeedChange ();
		}

		//デバッグコマンド
		if (Input.GetKeyDown (KeyCode.Return)) {
			GameObject.Find ("BlockManager").GetComponent< cBlockManager > ().DestroyBlock ();
			GameObject.Find ("BlockCreate").GetComponent< cBlockCreate > ().Create ();
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			GameObject.Find ("BlockManager").GetComponent< cBlockManager > ().DownSpeedUp ();
		}
	}
}
