using UnityEngine;
using System.Collections;

public class cField : MonoBehaviour {

	public const int HightMax = 21;
	public const int WidthMax = 12;
	public const int BlockMax = HightMax * WidthMax;

	private GameObject[] m_FixingBlock;

	// Use this for initialization
	void Start () {
		m_FixingBlock = new GameObject[ BlockMax ];

		for (int i = 0; i < BlockMax; i++) {

			m_FixingBlock[ i ] = (GameObject)Resources.Load ("Block");

			m_FixingBlock[ i ].name = "FixingBlock" + i.ToString();

			Vector3 setPosition = new Vector3(  ( i % WidthMax ) , ( i / WidthMax ) , 0.0f );

			if ( ( i / WidthMax == 0 )||
				 ( i % WidthMax == 0 )||
				 ( i % WidthMax == (WidthMax - 1) )) {
				m_FixingBlock [i].GetComponent< cBlock > ().SetColorType (cColor.eColor.Brown);
			}
			else {
				m_FixingBlock [i].GetComponent< cBlock > ().SetColorType (cColor.eColor.Transparency);
			}

			m_FixingBlock[i] = (GameObject)Instantiate( m_FixingBlock[ i ] , setPosition , Quaternion.identity );
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool HitCheck( Vector3 position ){
		int x = (int)position.x;
		int y = (int)position.y;

		if (y < 0 || x < 0 || x > WidthMax) {
			return true;
		}

		cBlock block = m_FixingBlock [x + ( y * WidthMax )].GetComponent< cBlock > ();

		if (block < cColor.eColor.Transparency) {
			return true;
		}
	}
}
