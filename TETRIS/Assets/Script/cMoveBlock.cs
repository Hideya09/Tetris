using UnityEngine;
using System.Collections;

public class cMoveBlock : MonoBehaviour {

	//前回のローカル座標
	private Vector3 m_PrevPosition;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//前回座標の更新
		m_PrevPosition = transform.localPosition;
	}

	//ワールド座標の取得
	public Vector3 GetPosition(){
		return transform.position;
	}

	//ブロックを右に回転させる
	public bool RotateRight(){
		Vector3 position = transform.localPosition;

		//中央から見て上下にあるときと斜めにある時で処理を変える
		if (position.x == 0 || position.y == 0) {
			if (position.x == 0) {
				position.x = -position.y;
				position.y = 0;
			} else {
				position.y = position.x;
				position.x = 0;
			}
		} else {
			if ((position.x > 0 && position.y > 0) || (position.x < 0 && position.y < 0)) {
				position.x = -position.x;
			} else {
				position.y = -position.y;
			}
		}

		//位置を更新
		transform.localPosition = position;

		//中央より下にあるかを返す
		if (position.y < 0) {
			return true;
		} else {
			return false;
		}
	}

	//ブロックを左に回転させる
	public bool RotateLeft(){
		Vector3 position = transform.localPosition;
		if (position.x == 0 || position.y == 0) {
			if (position.y == 0) {
				position.y = -position.x;
				position.x = 0;
			} else {
				position.x = position.y;
				position.y = 0;
			}
		} else {
			if ((position.x > 0 && position.y > 0) || (position.x < 0 && position.y < 0)) {
				position.y = -position.y;
			} else {
				position.x = -position.x;
			}
		}

		transform.localPosition = position;

		if (position.y < 0) {
			return true;
		} else {
			return false;
		}
	}

	//前回位置に戻す
	public void SetPrevPosition(){
		transform.localPosition = m_PrevPosition;
	}

	//色を設定する
	public void SetColor( cColor.eColor setColor ){
		GameObject color = GameObject.Find ("Color");

		cColor colorType = color.GetComponent< cColor >();

		SpriteRenderer sprite  = GetComponent< SpriteRenderer > ();

		sprite.sprite = colorType.GetSprite (setColor);
	}

	//ブロックを破壊する
	public void DestroyBlock(){
		Destroy (gameObject , .001f);
	}
}
