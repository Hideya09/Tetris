using UnityEngine;
using System.Collections;

public class cGameStart : MonoBehaviour {
	//カウントダウン用変数
	private int m_Count;

	//カウントを下げる用
	private const int m_Second = 60;
	private float m_SecondCount;

	//表示するテキストメッシュ
	private TextMesh m_TextMesh;

	// Use this for initialization
	void Start () {
		m_Count = 3;
		m_SecondCount = 0;

		m_TextMesh = GetComponent< TextMesh > ();
	}
	
	// Update is called once per frame
	void Update () {
		m_SecondCount += Time.deltaTime;

		if ( m_Count > 0 && Input.GetKeyDown (KeyCode.Return)) {
			m_SecondCount = m_Second;

			m_Count = 1;
		}

		//一秒経過したら表示文字を変える
		if (m_SecondCount >= 1.0f) {
			m_SecondCount = 0.0f;
			--m_Count;
			if (m_Count > 0) {
				m_TextMesh.text = m_Count.ToString ();
			} else if (m_Count < 0) {
				GameObject fieldObject = GameObject.Find ("Field");
				cField field = fieldObject.GetComponent< cField > ();

				field.GameStartSet ();

				Destroy (gameObject, .001f);
			} else {
				m_TextMesh.text = "START!";
			}

			m_TextMesh.characterSize = 0.5f;
			Color color = m_TextMesh.color;
			color.a = 1.0f;
			m_TextMesh.color = color;
		} else {
			//文字を大きくしつつ、透明にしていく
			m_TextMesh.characterSize += Time.deltaTime;
			Color color = m_TextMesh.color;
			color.a -= Time.deltaTime;
			m_TextMesh.color = color;
		}
	}
}
