using UnityEngine;
using System.Collections;

public class cScore : MonoBehaviour {

	public const int ScoreMax = 4;

	private int[] m_ScoreNumber;

	private int m_PrevScore;
	private int m_Score;

	private TextMesh m_Text;

	// Use this for initialization
	void Start () {
		m_Text = GetComponent< TextMesh > ();

		cBlockManager.InitSpeed ();

		m_Score = 0;
		m_PrevScore = 0;

		m_ScoreNumber = new int[ ScoreMax ];
		for (int i = 0; i < ScoreMax; ++i) {
			m_ScoreNumber [i] = (i + 1) * (i + 1);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ScoreAdd( int tetrisType ){
		if (tetrisType >= ScoreMax || tetrisType < 0) {
			return;
		}

		m_PrevScore = m_Score;
		m_Score += m_ScoreNumber [tetrisType];

		if (m_Score / 50 > m_PrevScore / 50) {
			cBlockManager.DownSpeedUp ();
		}

		m_Text.text = "Score:\n" + m_Score.ToString ();
	}
}
