using UnityEngine;
using System.Collections;

public class cBlockManager : MonoBehaviour {

	//ブロックが自動で下がる量
	private static float m_DownSpeed = 0.04f;

	//ブロックの最大数
	private const int BlockMax = 4;

	//奇数か偶数かのフラグ
	private bool m_RotateFlag;

	//センターの下にブロックがあるかないかのフラグ
	private bool m_BottomFlag;

	//自動で下がる量を引き上げる
	private bool m_DownSpeedFlag;

	//フィールド上に存在しているかのフラグ
	private bool m_FieldFlag;

	//ブロック全体の色
	public cColor.eColor m_BlockColor;

	//管理するブロックの配列
	private cMoveBlock[] m_MoveBlock;

	//フィールドへのアクセス
	private cField m_Field;

	//固定されるまでの猶予
	private int m_Fixing;

	//固定される時間
	private static int FixingMax = 30;

	void Awake(){
		//待機用のため名前を変更
		transform.gameObject.name = "BlockManagerWait";
		//フィールドには存在してない
		m_FieldFlag = false; 
	}

	// Use this for initialization
	void Start () {
		//各ブロックの準備
		m_MoveBlock = new cMoveBlock[ BlockMax ];

		m_DownSpeedFlag = false;

		for (int i = 0; i < BlockMax; i++) {
			string name = "MoveBlock" + (i + 1).ToString ();

			GameObject block = transform.FindChild (name).gameObject;

			m_MoveBlock[i] = block.GetComponent< cMoveBlock > ();

			m_MoveBlock[i].SetColor (m_BlockColor);
		}

		//フィールドのスクリプトを取得
		GameObject field = GameObject.Find ("Field");
		m_Field = field.GetComponent< cField > ();

		m_Fixing = 0;
	
		m_RotateFlag = false;
		m_BottomFlag = false;
	}
	
	// Update is called once per frame
	void Update () {

		//フィールドに存在している時
		if (m_FieldFlag == true) {
			//フラグが立っているなら早く落下させる
			if (m_DownSpeedFlag == true) {
				Vector3 position = transform.position;
				position.y -= m_DownSpeed * 3;
				transform.position = position;
				m_DownSpeedFlag = false;
			} else {
				Vector3 position = transform.position;
				position.y -= m_DownSpeed;
				transform.position = position;
			}

			//固定されるかをチェック
			int ghostNumber = cField.HightMax;

			int i;
			for (i = 0; i < BlockMax; i++) {

				if (m_Field.HitCheck (m_MoveBlock [i].GetPosition ())) {
					++m_Fixing;
					break;
				} else {
					int down = m_Field.GhostCheck (m_MoveBlock [i].GetPosition ());
					if (down < ghostNumber) {
						ghostNumber = down;
					}
				}
			}
			if (i == BlockMax) {
				m_Fixing = 0;

				if (ghostNumber > 0) {
					for (int j = 0; j < BlockMax; ++j) {
						m_Field.SetBlock (m_MoveBlock [j], cColor.eColor.Gray, ghostNumber);
					}
				}

			} else {
				//位置の修正
				Vector3 position = transform.position;
				position.y = Mathf.Ceil (position.y);
				transform.position = position;
			}

			if (m_Fixing == FixingMax) {
				m_FieldFlag = false;
				m_Field.SetCheck();

				DestroyManager ();
			}
		}
	}

	//右回転ボタンが押された際の回転チェック
	public void RotateRightCheck(){
		if (RotateCheck () == false) {
			Vector3 position = transform.position;
			Vector3 prevPosition = position;
			if (m_BottomFlag == true) {
				position.y += 1;

				transform.position = position;

				m_BottomFlag = false;
			}

			m_BottomFlag = RotateRight ();

			m_BottomFlag ^= true;

			if (m_BottomFlag == true){
				position.y -= 1;
				transform.position = position;
			}

			if (RotateHitCheck () == true) {
				transform.position = prevPosition;
			}
		}

		m_RotateFlag ^= true;
	}

	//左ボタンが押された際の回転チェック
	public void RotateLeftCheck(){
		if (RotateCheck () == false) {
			Vector3 position = transform.position;
			Vector3 prevPosition = position;
			if (m_BottomFlag == true) {
				position.y += 1;

				transform.position = position;

				m_BottomFlag = false;
			}

			m_BottomFlag = RotateLeft ();

			m_BottomFlag ^= true;

			if (m_BottomFlag == true){
				position.y -= 1;
				transform.position = position;
			}

			if (RotateHitCheck () == true) {
				transform.position = prevPosition;
			}
		}

		m_RotateFlag ^= true;
	}

	//ボタンによらない回転チェック
	private bool RotateCheck(){
		if (m_BlockColor == cColor.eColor.Yellow) {
			return true;
		} else if (m_BlockColor == cColor.eColor.Red || m_BlockColor == cColor.eColor.Green) {
			if (m_RotateFlag == true) {
				RotateRight ();
			} else {
				RotateLeft ();
			}

			RotateHitCheck ();

			return true;
		} else if (m_BlockColor == cColor.eColor.Purple) {
			if (m_RotateFlag == true) {
				RotateLeft ();
			} else {
				RotateRight ();
			}

			RotateHitCheck ();

			return true;
		} 

		return false;
	}

	//右回転させるプログラム
	private bool RotateRight(){
		bool bottom = false;

		for (int i = 0; i < BlockMax; ++i) {
			bottom |= m_MoveBlock[ i ].RotateRight ();
		}

		return bottom;
	}

	//左回転のプログラム
	private bool RotateLeft(){
		bool bottom = false;

		for (int i = 0; i < BlockMax; ++i) {
			bottom |= m_MoveBlock[ i ].RotateLeft ();
		}

		return bottom;
	}

	//回転後のあたり判定処理
	private bool RotateHitCheck(){
		int i;
		for (i = 0; i < BlockMax; ++i) {
			if (m_Field.HitCheck (m_MoveBlock [i].GetPosition ()) == true) {
				break;
			}
		}

		if (i != BlockMax) {
			for (int j = 0; j < 4; ++j) {
				m_MoveBlock [j].SetPrevPosition ();
			}

			return true;
		}

		return false;
	}

	//右への移動
	public void MoveRight(){
		Vector3 position = transform.position;
		position.x += 1;
		transform.position = position;

		int i;
		for (i = 0; i < BlockMax; i++) {
			if (m_Field.HitCheck (m_MoveBlock [i].GetPosition ()) == true) {
				break;
			}

			if (m_Field.HitCheck2 (m_MoveBlock [i].GetPosition ()) == true) {
				break;
			}
		}
		if (i != BlockMax) {
			position.x -= 1;
			transform.position = position;
		}
	}

	//左への移動
	public void MoveLeft(){
		Vector3 position = transform.position;
		position.x -= 1;
		transform.position = position;

		int i;
		for (i = 0; i < BlockMax; i++) {
			if (m_Field.HitCheck (m_MoveBlock [i].GetPosition ()) == true) {
				break;
			}
		}
		if (i != BlockMax) {
			position.x += 1;
			transform.position = position;
		}
	}

	//下がる速度上昇フラグを立てる
	public void DownSpeedChange(){
		m_DownSpeedFlag = true;
	}

	//さがる基本速度を変える
	public static void DownSpeedUp(){
		if (m_DownSpeed < 1.0f) {
			m_DownSpeed += 0.02f;
		}
	}

	public static void InitSpeed(){
		m_DownSpeed = 0.04f;
	}

	//フィールドに情報を渡して自殺する
	public void DestroyManager(){
		for( int i = 0 ; i < 4 ; i++ ){

			//色情報を渡す
			m_Field.SetBlock (m_MoveBlock [i], m_BlockColor);

			//ブロックを破棄する
			m_MoveBlock[i].DestroyBlock();
		}

		//自信を破棄する
		Destroy (gameObject , .001f);
	}

	//フィールドにこのマネージャーの管理するブロックを出現させる
	public void SetField(){

		//位置のセット
		Vector3 position;
		position.x = 6.0f;
		position.y = 22.0f;
		position.z = 0.0f;

		transform.position = position;

		gameObject.name = "BlockManager";

		m_FieldFlag = true;
		m_Fixing = 0;
	}
}
