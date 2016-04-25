using UnityEngine;
using System.Collections;

public class cPlayer : MonoBehaviour {

	public class cKeyDownCheck{
		private int m_Count;
		private bool m_InputFlag;
		private KeyCode m_Key;

		private const int FirstCountMax = 20;
		private const int CountMax = 5;

		public cKeyDownCheck( KeyCode key ){
			m_Count = 0;

			m_InputFlag = false;

			m_Key = key;
		}

		public bool KeyCheck(){
			if (Input.GetKey (m_Key)) {
				if (m_Count == 0) {
					++m_Count;
					return true;
				}

				++m_Count;

				if (m_InputFlag == true) {
					if (m_Count == CountMax) {
						m_Count = 0;
					}
				} else {
					if (m_Count == ( FirstCountMax )) {
						m_Count = 0;

						m_InputFlag = true;
					}
				}

				return false;
			} else {
				m_InputFlag = false;
				m_Count = 0;
				return false;
			}
		}
	}

	private cKeyDownCheck[] m_KeyCheck;

	private const int KeyNumber = 4;

	// Use this for initialization
	void Start () {
		m_KeyCheck = new cKeyDownCheck[ KeyNumber ];

		m_KeyCheck [0] = new cKeyDownCheck(KeyCode.RightArrow);
		m_KeyCheck [1] = new cKeyDownCheck(KeyCode.LeftArrow);
		m_KeyCheck [2] = new cKeyDownCheck(KeyCode.A);
		m_KeyCheck [3] = new cKeyDownCheck(KeyCode.S);
	}
	
	// Update is called once per frame
	void Update () {
		GameObject manager = GameObject.Find ("BlockManager");
		if (manager == null) {
			return;
		}

		//ボタン入力があったら回転、移動、高速の指示を出す
		if (m_KeyCheck[0].KeyCheck()) {
			manager.GetComponent< cBlockManager > ().MoveRight ();
		}
		if (m_KeyCheck[1].KeyCheck()) {
			manager.GetComponent< cBlockManager > ().MoveLeft ();
		}

		if (m_KeyCheck[2].KeyCheck()) {
			manager.GetComponent< cBlockManager > ().RotateLeftCheck ();
		}
		if (m_KeyCheck[3].KeyCheck()) {
			manager.GetComponent< cBlockManager > ().RotateRightCheck ();
		}

		if (Input.GetKey(KeyCode.DownArrow )) {
			manager.GetComponent< cBlockManager > ().DownSpeedChange ();
		}
	}
}
