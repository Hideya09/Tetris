using UnityEngine;
using System.Collections;

public class cGameOver : MonoBehaviour {

	//指定位置への方向
	private Vector3 m_Direction;

	//指定位置
	public Vector3 m_MovePosition;
	//何フレームで到達させるか
	public int m_MaxCount;

	//１フレームごとのスピード
	private float m_Speed;
	//現在何フレーム進んだか
	private int m_Count;

	//移動フラグ
	private bool m_MoveFlag;

	// Use this for initialization
	void Start () {
		//指定位置到達に必要な情報を計算する

		Vector3 position = transform.position;

		float distance = Vector3.Distance (m_MovePosition, position);

		m_Speed = distance / m_MaxCount;

		m_Direction.x = (m_MovePosition.x - position.x) / distance;
		m_Direction.y = (m_MovePosition.y - position.y) / distance;
		m_Direction.z = (m_MovePosition.z - position.z) / distance;

		m_MoveFlag = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (m_MoveFlag == true) {
			//指定位置に移動させる

			if (m_Count < m_MaxCount) {
				Vector3 position = transform.position;
				position.x += m_Direction.x * m_Speed;
				position.y += m_Direction.y * m_Speed;
				position.z += m_Direction.z * m_Speed;
				transform.position = position;

				++m_Count;
			} else {
				if( Input.GetKeyDown( KeyCode.Return )){
					GameObject fade = GameObject.Find ("Fade");
					cFadeInOut fadeInOut = fade.GetComponent< cFadeInOut > ();

					fadeInOut.SetFadeState (cFadeInOut.eFadeState.FadeOut);
				}
			}
		}
	}

	//移動を開始させる
	public void SetMoveFlag(){
		m_MoveFlag = true;
	}
}
