using UnityEngine;
using System.Collections;

public class cField : MonoBehaviour {

	//フィールドの縦幅
	public const int HightMax = 25;
	//フィールドのブロックが存在していい縦幅
	public const int HightBlockMax = 21;
	//フィールドの横幅
	public const int WidthMax = 12;
	//フィールドの全体の大きさ
	public const int BlockMax = HightMax * WidthMax;

	//動かないブロックの配列
	private cBlock[] m_FixingBlock;

	//ブロックが固定された瞬間のフラグ
	private bool m_FixingFlag;

	//ゲーム部分が動いているかのフラグ
	private bool m_GameFlag;

	//テトリミノを生成するスクリプトへのアクセス
	private cBlockCreate m_Create;

	// Use this for initialization
	void Start () {
		//フレームレート設定
		Application.targetFrameRate = 60;

		//テトリミノ生成スクリプトの取得
		GameObject create = GameObject.Find ("BlockCreate");

		m_Create = create.GetComponent < cBlockCreate > ();

		//フラグの初期化処理
		m_GameFlag = true;

		m_FixingFlag = false;

		//フィールド上のブロックの初期化処理
		m_FixingBlock = new cBlock[ BlockMax ];

		for (int i = 0; i < BlockMax; i++) {

			GameObject block = (GameObject)Resources.Load ("Block");

			block.name = "FixingBlock" + i.ToString();

			Vector3 setPosition = new Vector3(  ( i % WidthMax ) , ( i / WidthMax ) , 0.0f );

			block = (GameObject)Instantiate( block , setPosition , Quaternion.identity );

			m_FixingBlock [i] = block.GetComponent< cBlock > ();

			//壁や床の設定
			if ( ( i < ( HightBlockMax * WidthMax ) )&&
				 (( i / WidthMax == 0 )||
				 ( i % WidthMax == 0 )||
				 ( i % WidthMax == (WidthMax - 1) ))) {
				m_FixingBlock[ i ].SetColorType (cColor.eColor.Brown);
			}
			else {
				m_FixingBlock[ i ].SetColorType (cColor.eColor.Transparency);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		//固定が発生したら現在の状態を調べさせる
		if (m_FixingFlag == true ) {
			m_FixingFlag = false;
			WidthCheck ();

			//ゲームが続いている限りテトリミノを生み出させる
			if (m_GameFlag == true) {
				m_Create.Create ();
			}
		}
	}

	//あたり判定の処理
	public bool HitCheck( Vector3 position ){

		//そのまま入れる
		int x = (int)position.x;

		//y座標は切り捨てで計算する
		int y = (int)Mathf.Floor(position.y);

		//フィールド外部のため当たっていると判断する
		if (y < 0 || x < 0 || x >= WidthMax ) {
			return true;
		}

		//画面外に存在している
		if (y >= HightMax) {
			return false;
		}

		//ブロックが存在しているところに当たっていると判断する
		if (m_FixingBlock[ x + ( y * WidthMax ) ].GetColorType() < cColor.eColor.Transparency) {
			return true;
		}

		return false;
	}

	//フィールドのブロック状態を調べる
	private void WidthCheck(){

		//削除した列の数
		int deleteNumber = 0;

		//ブロックの配列を調べる
		for( int i = 1 ; i < HightMax ; ){
			int j;
			for (j = 1; j < WidthMax - 1; ++j) {
				//ブロックが存在していいところ以外にブロックがあればゲームオーバーにする
				//列内に透明なブロックを発見したらループを抜ける
				bool blockCheck = m_FixingBlock [j + (i * WidthMax)].GetColorType () == cColor.eColor.Transparency;
				if (i >= HightBlockMax && !blockCheck) {
					m_GameFlag = false;
					break;
				} else if (i < HightBlockMax && blockCheck) {
					break;
				}
			}
				
			if (m_GameFlag == false) {
				GameObject.Find ("GameOver").GetComponent< cGameOver > ().SetMoveFlag ();
				break;
			}

			//ループをbreak以外で抜けたら削除処理をおこなう
			if (j == WidthMax - 1) {
				++deleteNumber;

				if (WidthDelete (i) == true) {
					break;
				}
			} else {
				++i;
			}
		}
	}

	//ブロックの削除を行う
	private bool WidthDelete( int number ){
		bool blockFlag = true;

		//削除する列より上の情報を一つずつ下げる
		for (int i = ( number + 1 ) ; i < HightMax; ++i) {
			blockFlag = true;

			int j;
			for (j = 1; j < WidthMax - 1 ; ++j ) {
				cColor.eColor color = m_FixingBlock [j + (i * WidthMax)].GetColorType ();

				m_FixingBlock[ j + ( ( i - 1 ) *  WidthMax ) ].SetColorType( color );

				if (color != cColor.eColor.Transparency) {
					blockFlag = false;
				}

			}
			//一列全部空白の列があればループを抜ける
			if (blockFlag == true) {
				break;
			}
		}

		return blockFlag;
	}


	//固定フラグを立てる
	public void SetCheck(){
		m_FixingFlag = true;
	}

	//指定した場所に色情報をセットする
	public void SetBlock( cMoveBlock moveBlock , cColor.eColor color ){
		Vector3 position = moveBlock.GetPosition ();

		int x = (int)position.x;
		int y = (int)Mathf.Floor(position.y);

		m_FixingBlock [x + (y * WidthMax)].SetColorType (color);
	}
}
