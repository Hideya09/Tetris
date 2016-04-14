using UnityEngine;
using System.Collections;

public class cBlock : MonoBehaviour {

	private cColor.eColor m_ColorType;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void SetColorType( cColor.eColor setColor ){
		m_ColorType = setColor;

		Renderer mesh = this.GetComponent< Renderer > ();

		cColor getColor = GameObject.Find ("Color").GetComponent< cColor > ();

		mesh.material = getColor.GetMaterial (m_ColorType);
	}
}
