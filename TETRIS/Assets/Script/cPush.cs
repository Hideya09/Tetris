using UnityEngine;
using System.Collections;

public class cPush : MonoBehaviour {
	private TextMesh m_Text;

	private int m_Sign;

	// Use this for initialization
	void Start () {
		m_Text = GetComponent< TextMesh > ();

		m_Sign = 1;
	}
	
	// Update is called once per frame
	void Update () {
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
