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
	}

	//ブロックに色情報をセットする
	public void SetColorType( cColor.eColor setColor ){
		m_ColorType = setColor;

		Renderer mesh = this.GetComponent< Renderer > ();

		cColor getColor = GameObject.Find ("Color").GetComponent< cColor > ();

		mesh.material = getColor.GetMaterial (m_ColorType);
	}

	//ブロックの色情報を取得する
	public cColor.eColor GetColorType(){
		return m_ColorType;
	}
}
