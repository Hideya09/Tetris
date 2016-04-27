using UnityEngine;
using System.Collections;

public class cScore : MonoBehaviour {

	//加算する数値の数
	public const int ScoreMax = 4;

	//加算数値の配列
	private int[] m_ScoreNumber;

	//今回のスコア
	private int m_Score;

	//表示スコア
	private TextMesh m_Text;

	// Use this for initialization
	void Start () {
		m_Text = GetComponent< TextMesh > ();

		cBlockManager.InitSpeed ();

		m_Score = 0;

		//加算する数値を設定
		m_ScoreNumber = new int[ ScoreMax ];
		for (int i = 0; i < ScoreMax; ++i) {
			m_ScoreNumber [i] = (i + 1) * (i + 1);
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
		int prevScore = m_Score;
		//スコアの加算
		m_Score += m_ScoreNumber [tetrisType];

		//５０刻みに基礎落下速度を変える
		if (m_Score / 50 > prevScore / 50) {
			cBlockManager.DownSpeedUp ();
		}

		//表示テキストに現在のスコアを記録
		m_Text.text = "Score:\n" + m_Score.ToString ();
	}
}
