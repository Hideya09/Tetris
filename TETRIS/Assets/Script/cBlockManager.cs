using UnityEngine;
using System.Collections;

public class cBlockManager : MonoBehaviour {

	//ブロックが自動で下がる量
	private float m_DownSpeed;

	public static float m_DownBasicSpeed = 3.6f;
	public static float m_DownAdd = 1.2f;
	public static int m_DownAddNumber = 0;

	//ブロックの最大数
	private const int BlockMax = 4;

	//奇数か偶数かのフラグ
	private bool m_RotateFlag;

	//センターの下にブロックがあるかないかのフラグ
	private bool m_BottomFlag;

	//急速落下を行うフラグ
	private bool m_HardDropFlag;
	private int m_HardDropPossibleCount;
	//自動で下がる量を引き上げるフラグ
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
	private float m_Fixing;

	//固定される時間
	private static float FixingMax = 0.5f;

	//右方向の移動について前回上部分で当たっていた場合
	private bool m_RightHitUpFlag;
	//左方向の移動について前回上部分で当たっていた場合
	private bool m_LeftHitUpFlag;

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

		m_HardDropFlag = false;
		m_HardDropPossibleCount = 15;

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

		m_DownSpeed = m_DownBasicSpeed + (m_DownAdd * m_DownAddNumber);
	}
	
	// Update is called once per frame
	void Update () {

		//フィールドに存在している時
		if (m_FieldFlag == true) {
			m_DownSpeed = m_DownBasicSpeed + (m_DownAdd * m_DownAddNumber);

			if (m_HardDropPossibleCount > 0) {
				--m_HardDropPossibleCount;
			}

			//フラグが立っているなら早く落下させる
			Vector3 position = transform.position;

			if (m_HardDropFlag == true) {
				position.y = Mathf.Floor (position.y);
				m_DownSpeedFlag = false;
			}

			if (m_DownSpeedFlag == true) {
				position.y -= ( m_DownSpeed * Time.deltaTime ) * 3;
				m_DownSpeedFlag = false;
			} else {
				position.y -= ( m_DownSpeed * Time.deltaTime );
			}

			transform.position = position;

			//固定されるかをチェック
			int ghostNumber = cField.HightMax;

			int i;
			for (i = 0; i < BlockMax; i++) {

				if (m_Field.HitCheck (m_MoveBlock [i].GetPosition ())) {
					m_Fixing += Time.deltaTime;
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
				position = transform.position;
				position.y = Mathf.Ceil (position.y);
				transform.position = position;
				if (m_HardDropFlag == true) {
					m_Fixing = FixingMax;
				}
			}

			m_HardDropFlag = false;

			//固定が行われた
			if (m_Fixing >= FixingMax) {
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
				if (RotateMove () == false) {
					RotateCancel ();
					transform.position = prevPosition;
				}
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
				if (RotateMove () == false) {
					RotateCancel ();
					transform.position = prevPosition;
				}
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

		} else if (m_BlockColor == cColor.eColor.Purple) {
			if (m_RotateFlag == true) {
				RotateLeft ();
			} else {
				RotateRight ();
			}
		} else {
			return false;
		}

		if (RotateHitCheck () == true) {
			if (RotateMove () == false) {
				RotateCancel ();
			}
		}

		return true;
	}

	//移動することによって回転が可能かをチェックする関数
	private bool RotateMove(){
		Vector3 position = transform.position;
		position.x += 1;
		transform.position = position;

		if (RotateHitCheck () == false) {
			return true;
		}

		position = transform.position;
		position.x -= 2;
		transform.position = position;

		if (RotateHitCheck () == false) {
			return true;
		}

		position = transform.position;
		position.x += 1;
		transform.position = position;

		return false;
	}

	//回転が出来ない場合元の位置に戻す
	private void RotateCancel(){
		for (int i = 0; i < BlockMax; ++i) {
			m_MoveBlock [i].SetPrevPosition ();
		}
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
			return true;
		}

		return false;
	}

	//右への移動
	public bool MoveRight(){
		m_LeftHitUpFlag = false;

		Vector3 position = transform.position;
		position.x += 1;
		transform.position = position;

		bool upHit = false;

		int i;
		for (i = 0; i < BlockMax; i++) {
			if (m_Field.HitCheck (m_MoveBlock [i].GetPosition ()) == true) {
				break;
			}

			if (m_Field.HitCheck2 (m_MoveBlock [i].GetPosition ()) == true) {
				upHit = true;
			}
		}
		if (i != BlockMax && m_RightHitUpFlag == false) {
			position.x -= 1;
			transform.position = position;

			return false;
		} else if (upHit == true) {
			position.x -= 1;
			transform.position = position;

			m_RightHitUpFlag = true;

			return false;
		}

		m_RightHitUpFlag = false;

		return true;
	}

	//左への移動
	public bool MoveLeft(){
		m_RightHitUpFlag = false;

		Vector3 position = transform.position;
		position.x -= 1;
		transform.position = position;

		bool upHit = false;

		int i;
		for (i = 0; i < BlockMax; i++) {
			if (m_Field.HitCheck (m_MoveBlock [i].GetPosition ()) == true) {
				break;
			}
			if (m_Field.HitCheck2 (m_MoveBlock [i].GetPosition ()) == true) {
				upHit = true;
			}
		}
		if (i != BlockMax && m_LeftHitUpFlag == false) {
			position.x += 1;
			transform.position = position;

			return false;
		} else if (upHit == true) {
			position.x += 1;
			transform.position = position;

			m_LeftHitUpFlag = true;

			return false;
		}

		m_LeftHitUpFlag = false;

		return true;
	}

	public void DontMove(){
		m_RightHitUpFlag = false;
		m_LeftHitUpFlag = false;
	}

	public void HardDropMode(){
		if (m_HardDropPossibleCount == 0) {
			m_HardDropFlag = true;
		}
	}

	//下がる速度上昇フラグを立てる
	public void DownSpeedChange(){
		m_DownSpeedFlag = true;
	}

	//さがる基本速度を変える
	public static void DownSpeedUp(){
		++m_DownAddNumber;
	}

	public static void InitSpeed(){
		m_DownAddNumber = 0;
	}

	//フィールドに情報を渡して自殺する
	public void DestroyManager(){
		bool gameContinueFlag = false;

		for( int i = 0 ; i < 4 ; i++ ){

			//色情報を渡す
			 gameContinueFlag |= m_Field.SetBlock (m_MoveBlock [i], m_BlockColor);

			//ブロックを破棄する
			m_MoveBlock[i].DestroyBlock();
		}

		if (gameContinueFlag == false) {
			m_Field.GameOverSet ();
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
