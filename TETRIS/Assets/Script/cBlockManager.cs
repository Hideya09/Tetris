using UnityEngine;
using System.Collections;

public class cBlockManager : MonoBehaviour {
	private static float m_DownSpeed = 0.04f;

	private bool m_DownSpeedFlag;

	private bool m_FieldFlag;

	private Vector3 m_PrevPosition;

	public cColor.eColor m_BlockColor;

	void Awake(){
		transform.gameObject.name = "BlockManagerWait";
		m_FieldFlag = false; 
	}

	// Use this for initialization
	void Start () {
		m_DownSpeedFlag = false;

		for (int i = 0; i < 4; i++) {
			string name = "MoveBlock" + (i + 1).ToString ();

			GameObject block = transform.FindChild (name).gameObject;

			cMoveBlock moveBlock = block.GetComponent< cMoveBlock > ();

			moveBlock.SetColor (m_BlockColor);
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (m_FieldFlag == true) {
			if (m_DownSpeedFlag == true) {
				Vector3 position = transform.position;
				position.y -= m_DownSpeed * 2;
				transform.position = position;
			} else {
				Vector3 position = transform.position;
				position.y -= m_DownSpeed;
				transform.position = position;
			}

			for (int i = 0; i < 4; i++) {
				string name = "MoveBlock" + (i + 1).ToString ();

				GameObject block = transform.FindChild (name).gameObject;
			}
		}

		m_DownSpeedFlag = false;
	}

	public void RotateRight(){
		if (m_BlockColor != cColor.eColor.Yellow) {
			for (int i = 0; i < 4; i++) {
				string name = "MoveBlock" + (i + 1).ToString ();

				GameObject block = transform.FindChild (name).gameObject;
				cMoveBlock moveBlock = block.GetComponent< cMoveBlock > ();

				moveBlock.RotateRight ();
				//moveBlock.MovePosition ();
			}
		}
	}

	public void RotateLeft(){
		if (m_BlockColor != cColor.eColor.Yellow) {
			int i;
			for (i = 0; i < 4; i++) {
				string name = "MoveBlock" + (i + 1).ToString ();

				GameObject block = transform.FindChild (name).gameObject;
				cMoveBlock moveBlock = block.GetComponent< cMoveBlock > ();

				moveBlock.RotateLeft ();
				//moveBlock.MovePosition ();
			}
		}
	}

	public void MoveRight(){
		Vector3 position = transform.position;
		position.x += 1;
		transform.position = position;

		for( int i = 0 ; i < 4 ; i++ ){
			string name = "MoveBlock" + (i + 1).ToString ();

			GameObject block = transform.FindChild (name).gameObject;

			//GameObject block = GameObject.Find (name);
			//cMoveBlock moveBlock = block.GetComponent< cMoveBlock > ();

			//Vector3 position = moveBlock.GetPosition(transform.position);
		}
	}

	public void MoveLeft(){
		Vector3 position = transform.position;
		position.x -= 1;
		transform.position = position;

		for( int i = 0 ; i < 4 ; i++ ){
			string name = "MoveBlock" + (i + 1).ToString ();

			GameObject block = transform.FindChild (name).gameObject;

			//GameObject block = GameObject.Find (name);
			//cMoveBlock moveBlock = block.GetComponent< cMoveBlock > ();

			//Vector3 position = moveBlock.GetPosition(transform.position);

		}
	}

	public void DownSpeedChange(){
		m_DownSpeedFlag = true;
	}

	public void DownSpeedUp(){
		m_DownSpeed += 0.02f;
	}

	public void DestroyBlock(){
		for( int i = 0 ; i < 4 ; i++ ){
			string name = "MoveBlock" + (i + 1).ToString ();

			GameObject block = transform.FindChild (name).gameObject;

			Destroy (block, .1f);
		}
		Destroy (gameObject, .1f);
	}

	public void SetField(){
		Vector3 position;
		position.x = 6.0f;
		position.y = 23.0f;
		position.z = 0.0f;

		transform.position = position;

		transform.gameObject.name = "BlockManager";

		m_FieldFlag = true;
	}
}
