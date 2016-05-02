using UnityEngine;
using System.Collections;

public class cScore : MonoBehaviour {

	//加算する数値の数
	public const int ScoreMax = 4;

	//加算数値の配列
	private int[] m_ScoreNumber;

	//現在のスコア
	private int m_Score;

	//現在の消去ライン数
	private int m_Line;

	//表示スコア
	private TextMesh m_Text;

	// Use this for initialization
	void Start () {
		m_Text = GetComponent< TextMesh > ();

		cBlockManager.InitSpeed ();

		m_Line = 0;
		m_Score = 0;

		//加算する数値を設定
		m_ScoreNumber = new int[ ScoreMax ];
		for (int i = 0; i < ScoreMax; ++i) {
			m_ScoreNumber [i] = (i + 2) * (i + 2);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//スコアの加算
	public void ScoreAdd( int tetrisType ){
		//ありえない消去数が来たら抜ける
		if (tetrisType >= ScoreMax || tetrisType < 0) {
			return;
		}

		//現在スコアの記録
		int prevLine = m_Line;
		//スコアの加算
		m_Score += m_ScoreNumber [tetrisType];
		m_Line += (tetrisType + 1);

		//５０刻みに基礎落下速度を変える
		if (m_Line / 10 > prevLine / 10) {
			cBlockManager.DownSpeedUp ();
		}

		//表示テキストに現在のスコアとラインを記述
		m_Text.text = "Score:\n" + m_Score.ToString () + "\nLine:\n" + m_Line.ToString();
	}
}
