using UnityEngine;
using System.Collections;

public class cPlayer : MonoBehaviour {

	//キーの連続入力を取得するクラス
	public class cKeyDownCheck{

		//キーが押しっぱなしになっているフレーム数
		private int m_Count;

		//キーが押しっぱなしかのフラグ
		private bool m_InputFlag;

		//連続入力をとるキー
		private KeyCode m_Key;

		//単発入力か押しっぱなしかを判断するフレーム数
		private const int FirstCountMax = 20;

		//まだ押されていることを確認するフレーム数
		private const int CountMax = 5;

		//コンストラクタ
		public cKeyDownCheck( KeyCode key ){
			m_Count = 0;

			m_InputFlag = false;

			m_Key = key;
		}

		//キー入力の検出
		public bool KeyCheck(){
			if (Input.GetKey (m_Key)) {
				if (m_Count == 0) {
					++m_Count;
					return true;
				}

				++m_Count;

				if (m_InputFlag == true) {
					if (m_Count == CountMax) {
						//押しっぱなしになっているため次のフレームで入力されていると返す
						m_Count = 0;
					}
				} else {
					if (m_Count == FirstCountMax ) {
						//押しっぱなしになっていると判断する
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

		public void InputInit(){
			//入力情報を初期化する
			m_InputFlag = false;
			m_Count = 0;
		}
	}

	//キー入力配列
	private cKeyDownCheck[] m_KeyCheck;

	//連続入力を取るキーの数
	private const int KeyNumber = 4;

	// Use this for initialization
	void Start () {
		//キー情報をセット

		m_KeyCheck = new cKeyDownCheck[ KeyNumber ];

		m_KeyCheck [0] = new cKeyDownCheck(KeyCode.RightArrow);
		m_KeyCheck [1] = new cKeyDownCheck(KeyCode.LeftArrow);
		m_KeyCheck [2] = new cKeyDownCheck(KeyCode.A);
		m_KeyCheck [3] = new cKeyDownCheck(KeyCode.S);
	}
	
	// Update is called once per frame
	void Update () {
		//生成破棄を繰り返すためここで取得し、存在してないならすぐ抜ける
		GameObject manager = GameObject.Find ("BlockManager");
		if (manager == null) {
			return;
		}

		cBlockManager blockManager = manager.GetComponent< cBlockManager > ();

		//ボタン入力があったら回転、移動、高速の指示を出す
		if (m_KeyCheck [0].KeyCheck ()) {
			if (blockManager.MoveRight () == false) {
				m_KeyCheck [0].InputInit ();
			}
		}else if (m_KeyCheck [1].KeyCheck ()) {
			if (blockManager.MoveLeft () == false) {
				m_KeyCheck [1].InputInit ();
			}
		} else {
			blockManager.DontMove ();
		}

		if (m_KeyCheck[2].KeyCheck()) {
			blockManager.RotateLeftCheck ();
			blockManager.DontMove ();
		}
		if (m_KeyCheck[3].KeyCheck()) {
			blockManager.RotateRightCheck ();
			blockManager.DontMove ();
		}

		if (Input.GetKey(KeyCode.DownArrow )) {
			blockManager.DownSpeedChange ();
		}
	}
}
