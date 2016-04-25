using UnityEngine;
using System.Collections;

public class cBlockCreate : MonoBehaviour {

	//ブロックマネージャー名の末尾配列
	char[] m_BlockType;

	//ブロックマネージャー数
	private const int NumberMax = 7;

	//ブロックマネージャーへのアクセス
	private GameObject m_BlockManager;

	// Use this for initialization
	void Start () {
		m_BlockType = new char[ NumberMax ];

		//末尾を入れる
		m_BlockType [0] = 'I';
		m_BlockType [1] = 'O';
		m_BlockType [2] = 'Z';
		m_BlockType [3] = 'S';
		m_BlockType [4] = 'J';
		m_BlockType [5] = 'L';
		m_BlockType [6] = 'T';
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//ブロックを生成し、待機状態にする
	private void BlockCreate(){
		m_BlockManager = null;

		int number = Random.Range (0, NumberMax);
		string name = "BlockManager" + m_BlockType [number];

		Vector3 setPosition;

		setPosition.x = 15.0f;
		setPosition.y = 17.5f;
		setPosition.z = 0.0f;

		GameObject blockManager = (GameObject)Resources.Load (name);

		m_BlockManager = (GameObject)Instantiate( blockManager , setPosition , Quaternion.identity );
	}

	//待機ブロックをフィールドに出し、新しいブロックを生成させる
	public void Create(){
		
		cBlockManager manager = m_BlockManager.GetComponent< cBlockManager > ();
		manager.SetField ();

		BlockCreate ();
	}

	public void StartCreate(){
		//一番最初のブロックとその次のブロックを生成
		BlockCreate ();
		Create ();
	}
}
