using UnityEngine;
using System.Collections;

public class cSceneMove : MonoBehaviour {

	private bool m_SceneMoveFlag;

	public int m_SceneNumber;

	// Use this for initialization
	void Start () {
		m_SceneMoveFlag = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (m_SceneMoveFlag == true) {
			if( Input.GetKeyDown( KeyCode.Return )){
				UnityEngine.SceneManagement.SceneManager.LoadScene( m_SceneNumber );
			}
		}
	}

	public void SetSceneMoveFlag(){
		m_SceneMoveFlag = true;
	}
}
