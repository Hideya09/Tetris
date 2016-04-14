using UnityEngine;
using System.Collections;

public class cMoveBlock : MonoBehaviour {

	private Vector3 m_WorldPosition;
	private Vector3 m_PrevPosition;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		m_PrevPosition = transform.position;
	}

	public Vector3 GetPosition( Vector3 worldPosition ){
		Vector3 position = transform.position;
		position.x += worldPosition.x;
		position.y += worldPosition.y;
		position.z += worldPosition.z;

		return position;
	}

	public Vector3 GetPosition2(){
		return transform.position;
	}

	public void RotateRight(){
		Vector3 position = transform.localPosition;
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

		transform.localPosition = position;
	}

	public void RotateLeft(){
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
	}

	public void SetPrevPosition(){
		transform.position = m_PrevPosition;
	}

	public void SetColor( cColor.eColor setColor ){
		GameObject color = GameObject.Find ("Color");

		cColor colorType = color.GetComponent< cColor >();

		MeshRenderer meshRenderer = GetComponent< MeshRenderer > ();

		meshRenderer.material = colorType.GetMaterial (setColor);
	}
}
