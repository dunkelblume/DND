using UnityEngine;
using System.Collections;

/// <summary>
/// The CoinSpawn Script deals with the coin spawn. It takes Coin PreFab as GameObject and takes input 
/// from EnemyHealth to find out Zombie's position when it died. 
/// </summary>

public class CoinSpawn : MonoBehaviour {

	#region Public Variabes
	public GameObject Coin;											//For PreFab Coin
	#endregion
	
	#region Private Variables
	Vector3 CoinPosition;
	#endregion

	#region Utilty Function
	/// <summary>
	/// CoinInstantiate Function, takes in the vector 3 value from Enemy Health
	/// and instantiate the Coin Prefab 
	/// </summary>
	/// <param name="ZombieLocation">Zombie location.</param>
	
	public void CoinInstantiate(Vector3 ZombieLocation)
	{

			CoinPosition = ZombieLocation;							    //Get the position of Zombie and create V3 
			CoinPosition.y = -0.75f;									//TempFix, Animation was done in high position in Maya
			Coin.transform.position = CoinPosition;						//assign the v3 position to coin
			
			
			
			Instantiate(Coin); //[1]

	}
	#endregion
}

#region Reference
//[1.]http://docs.unity3d.com/ScriptReference/Object.Instantiate.html
#endregion
