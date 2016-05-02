using UnityEngine;
using System.Collections;

public class cBlock : MonoBehaviour {

	//ブロックの色
	private cColor.eColor m_ColorType;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (m_ColorType == cColor.eColor.Gray) {
			SetColorType (cColor.eColor.Transparency);
		}
	}

	//ブロックに色情報をセットする
	public void SetColorType( cColor.eColor setColor ){
		m_ColorType = setColor;

		SpriteRenderer sprite = this.GetComponent< SpriteRenderer > ();

		cColor getColor = GameObject.Find ("Color").GetComponent< cColor > ();

		sprite.sprite = getColor.GetSprite (m_ColorType);

		if (setColor == cColor.eColor.Transparency) {
			sprite.color = new Color (1.0f, 1.0f, 1.0f, 0.0f);
		} else {
			sprite.color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
		}
	}

	//ブロックの色情報を取得する
	public cColor.eColor GetColorType(){
		return m_ColorType;
	}
}
