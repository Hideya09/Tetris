using UnityEngine;
using System.Collections;

public class cColor : MonoBehaviour {

	//ブロックの色情報
	public enum eColor{
		Red,             //赤いブロック
		Yellow,          //黄色いブロック
		Purple,          //紫色のブロック
		Green,           //緑色のブロック
		Blue,            //青いブロック
		Orange,          //橙のブロック
		LightBlue,       //水色のブロック
		Brown,           //茶色いブロック(壁)
		Gray,            //灰色のブロック(ゴースト)
		Transparency,    //透明なブロック(何もないところ)
		Max
	};

	//色のマテリアル
	public Material[] m_Color;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//指定色のマテリアルを取得
	public Material GetMaterial( eColor index ){
		return m_Color [(int)index];
	}
}
