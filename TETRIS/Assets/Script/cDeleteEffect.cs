using UnityEngine;
using System.Collections;

public class cDeleteEffect : MonoBehaviour {

	//動かすかどうかのフラグ
	private bool m_MoveFlag;

	//消去した列数に応じた文字列
	private string[] m_DeleteString;

	//表示するテキスト
	private TextMesh m_Text;

	//画面内で止まっているフレーム数
	private int m_Count;
	private const int CountMax = 60;

	// Use this for initialization
	void Start () {
		m_MoveFlag = false;

		m_DeleteString = new string[ cScore.ScoreMax ];

		m_DeleteString [0] = "Single!";
		m_DeleteString [1] = "Double!";
		m_DeleteString [2] = "Triple!";
		m_DeleteString [3] = "Tetris!";

		m_Text = GetComponent< TextMesh > ();
	}

	// Update is called once per frame
	void Update () {
		// 消去が行われたら動き始める
		//特定の場所まで進んだら一度止めてその後再び進める
		//外部まで出たら初期位置に戻す
		if (m_MoveFlag == true) {
			Vector3 position = transform.position;
			if (position.x < 1.0f) {
				position.x += 1.0f;
			} else if (m_Count < CountMax) {
				++m_Count;
			} else if (position.x < 30.0f) {
				position.x += 1.5f;
			} else {
				position.x = -20.0f;
				m_MoveFlag = false;
				m_Count = 0;
			}

			transform.position = position;
		}
	}

	//文字列をセットし動かすフラグを立てる
	public void SetMoveFlag( int deleteNumber ){
		if (deleteNumber < 0 || deleteNumber >= cScore.ScoreMax) {
			return;
		}
		m_Text.text = m_DeleteString [deleteNumber];
		m_MoveFlag = true;
	}
}
