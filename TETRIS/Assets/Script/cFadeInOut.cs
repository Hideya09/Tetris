using UnityEngine;
using System.Collections;

public class cFadeInOut : MonoBehaviour {

	//フェードのステート
	public enum eFadeState{
		FadeIn,
		FadeInEnd,
		FadeStop,
		FadeOut,
		FadeOutEnd
	}

	//加算と減算をする数値
	private const float FadeArpha = 0.01f;

	//フェード用画像
	private SpriteRenderer m_Sprite;

	//現在のフェードステート
	private eFadeState m_State;

	//フェードインが終わった後に呼び出すオブジェクト
	public GameObject m_StartObject;

	//フェードアウト後に呼び出すシーン番号
	public int m_SceneNumber;

	void Awake(){
		//フレームレート設定
		Application.targetFrameRate = 60;
	}

	// Use this for initialization
	void Start () {
		m_Sprite = GetComponent< SpriteRenderer > ();
		m_State = eFadeState.FadeIn;
	}
	
	// Update is called once per frame
	void Update () {
		switch (m_State) {
		case eFadeState.FadeIn:
			//徐々に白いテクスチャを薄くしていく
			Color colorIn = m_Sprite.color;
			colorIn.a -= Time.deltaTime;
			m_Sprite.color = colorIn;

			if (m_Sprite.color.a <= 0.0f) {
				m_State = eFadeState.FadeInEnd;
			}
			break;
		case eFadeState.FadeInEnd:
			//オブジェクトを生成
			m_State = eFadeState.FadeStop;
			Instantiate (m_StartObject);
			break;
		case eFadeState.FadeStop:
			//特に処理をしない
			break;
		case eFadeState.FadeOut:
			//徐々に白いテクスチャを濃くしていく
			Color colorOut = m_Sprite.color;
			colorOut.a += Time.deltaTime;
			m_Sprite.color = colorOut;

			if (m_Sprite.color.a >= 1.0f) {
				m_State = eFadeState.FadeOutEnd;
			}
			break;
		case eFadeState.FadeOutEnd:
			//シーンを切り替える
			UnityEngine.SceneManagement.SceneManager.LoadScene (m_SceneNumber);
			break;
		default:
			//処理なし
			break;
		}
	}

	public void SetFadeState( eFadeState state ){
		//フェードステートの切り替え
		m_State = state;
	}
}
