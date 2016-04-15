using UnityEngine;
using System.Collections;

public class cFadeInOut : MonoBehaviour {

	public enum eFadeState{
		FadeIn,
		FadeInEnd,
		FadeStop,
		FadeOut,
		FadeOutEnd
	}

	private const float FadeArpha = 0.01f;

	private SpriteRenderer m_Sprite;
	private eFadeState m_State;

	public GameObject m_StartObject;

	public int m_SceneNumber;

	// Use this for initialization
	void Start () {
		m_Sprite = GetComponent< SpriteRenderer > ();
		m_State = eFadeState.FadeIn;
	}
	
	// Update is called once per frame
	void Update () {
		switch (m_State) {
		case eFadeState.FadeIn:
			Color colorIn = m_Sprite.color;
			colorIn.a -= FadeArpha;
			m_Sprite.color = colorIn;

			if (m_Sprite.color.a <= 0.0f) {
				m_State = eFadeState.FadeInEnd;
			}
			break;
		case eFadeState.FadeInEnd:
			m_State = eFadeState.FadeStop;
			Instantiate (m_StartObject);
			break;
		case eFadeState.FadeStop:
			break;
		case eFadeState.FadeOut:
			Color colorOut = m_Sprite.color;
			colorOut.a += FadeArpha;
			m_Sprite.color = colorOut;

			if (m_Sprite.color.a >= 1.0f) {
				m_State = eFadeState.FadeOutEnd;
			}
			break;
		case eFadeState.FadeOutEnd:
			UnityEngine.SceneManagement.SceneManager.LoadScene (m_SceneNumber);
			break;
		default:
			//処理なし
			break;
		}
	}

	public void SetFadeState( eFadeState state ){
		m_State = state;
	}
}
