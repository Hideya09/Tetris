using UnityEngine;
using System.Collections;

public class cColor : MonoBehaviour {

	public enum eColor{
		Red,
		Yellow,
		Purple,
		Green,
		Blue,
		Orange,
		LightBlue,
		Brown,
		Gray,
		Transparency,
		Max
	};

	public Material[] m_Color;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Material GetMaterial( eColor index ){
		return m_Color [(int)index];
	}
}
