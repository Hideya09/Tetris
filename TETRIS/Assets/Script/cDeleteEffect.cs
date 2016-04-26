using UnityEngine;
using System.Collections;

public class cDeleteEffect : MonoBehaviour {

	private bool m_MoveFlag;

	private string[] m_DeleteString;

	private TextMesh m_Text;

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

	public void SetMoveFlag( int deleteNumber ){
		if (deleteNumber < 0 || deleteNumber >= cScore.ScoreMax) {
			return;
		}
		m_Text.text = m_DeleteString [deleteNumber];
		m_MoveFlag = true;
	}
}
