using UnityEngine;
using System.Collections;

public class cGameOver : MonoBehaviour {

	//指定位置への方向
	private Vector3 m_Direction;

	//指定位置
	public Vector3 m_MovePosition;
	//何秒で到達させるか
	public float m_MaxCount;

	//スピード
	private float m_Speed;
	//現在何フレーム進んだか
	private float m_Count;

	//移動フラグ
	private bool m_MoveFlag;

	// Use this for initialization
	void Start () {
		//指定位置到達に必要な情報を計算する

		m_Count = 0;

		Vector3 position = transform.position;

		float distance = Vector3.Distance (m_MovePosition, position);

		m_Direction.x = (m_MovePosition.x - position.x) / distance;
		m_Direction.y = (m_MovePosition.y - position.y) / distance;
		m_Direction.z = (m_MovePosition.z - position.z) / distance;

		m_Speed = distance / m_MaxCount;

		m_MoveFlag = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (m_MoveFlag == true) {
			//指定位置に移動させる

			Vector3 position = transform.position;

			if (m_Count < m_MaxCount) {
				if (Input.GetKeyDown (KeyCode.Return)) {
					transform.position = m_MovePosition;

					m_Count = m_MaxCount;
				} else {
					position.x += m_Direction.x * (m_Speed * Time.deltaTime);
					position.y += m_Direction.y * (m_Speed * Time.deltaTime);
					position.z += m_Direction.z * (m_Speed * Time.deltaTime);
					transform.position = position;
				}

				m_Count += Time.deltaTime;

				if (m_Count >= m_MaxCount) {
					GameObject.Find ("PushText").GetComponent< cPush > ().SetPush ();
				}
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
