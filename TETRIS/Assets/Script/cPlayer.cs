using UnityEngine;
using System.Collections;

public class cPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GameObject manager = GameObject.Find ("BlockManager");
		if (manager == null) {
			return;
		}

		//ボタン入力があったら回転、移動、高速の指示を出す
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			manager.GetComponent< cBlockManager > ().MoveRight ();
		}
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			manager.GetComponent< cBlockManager > ().MoveLeft ();
		}

		if (Input.GetKeyDown (KeyCode.A)) {
			manager.GetComponent< cBlockManager > ().RotateLeftCheck ();
		}
		if (Input.GetKeyDown (KeyCode.S)) {
			manager.GetComponent< cBlockManager > ().RotateRightCheck ();
		}

		if (Input.GetKey (KeyCode.DownArrow)) {
			manager.GetComponent< cBlockManager > ().DownSpeedChange ();
		}

		//デバッグコマンド
		if (Input.GetKeyDown (KeyCode.Space)) {
			manager.GetComponent< cBlockManager > ().DownSpeedUp ();
		}
	}
}
