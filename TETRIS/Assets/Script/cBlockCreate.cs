using UnityEngine;
using System.Collections;

public class cBlockCreate : MonoBehaviour {

	char[] m_BlockType;

	private const int NumberMax = 7;

	private GameObject m_BlockManager;

	// Use this for initialization
	void Start () {
		m_BlockType = new char[ NumberMax ];

		m_BlockType [0] = 'I';
		m_BlockType [1] = 'O';
		m_BlockType [2] = 'Z';
		m_BlockType [3] = 'S';
		m_BlockType [4] = 'J';
		m_BlockType [5] = 'L';
		m_BlockType [6] = 'T';

		BlockCreate ();
		Create ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void BlockCreate(){
		m_BlockManager = null;

		int number = Random.Range (0, NumberMax);
		string name = "BlockManager" + m_BlockType [number];

		Vector3 setPosition;

		setPosition.x = 15.0f;
		setPosition.y = 20.0f;
		setPosition.z = 0.0f;

		GameObject blockManager = (GameObject)Resources.Load (name);

		m_BlockManager = (GameObject)Instantiate( blockManager , setPosition , Quaternion.identity );
	}

	public void Create(){
		
		cBlockManager manager = m_BlockManager.GetComponent< cBlockManager > ();
		manager.SetField ();

		BlockCreate ();
	}
}
