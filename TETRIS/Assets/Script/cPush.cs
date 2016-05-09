using UnityEngine;
using System.Collections;

public class cPush : MonoBehaviour {
	private TextMesh m_Text;

	private int m_Sign;

	private bool m_Push;

	// Use this for initialization
	void Start () {
		m_Text = GetComponent< TextMesh > ();

		m_Sign = 1;

		m_Push = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (m_Push == true) {
			Color color = m_Text.color;
			color.a += Time.deltaTime * m_Sign;
			m_Text.color = color;

			if (color.a < 0.0f) {
				m_Sign = 1;
			}

			if (color.a > 1.0f) {
				m_Sign = -1;
			}
		}
	}

	public void SetPush(){
		m_Push = true;
	}
}
