using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{

	[Serializable]
	 public class Count
	 {
		public int minimum;
		public int maximum;

		public Count( int min, int max)
		{
			minimum = min;
			maximum = max;
		}
	 }

	public int columns = 20;
	public int rows = 8;
	//public Count wallCount = new Count(4,9);
	public Count foodcount = new Count(1,5);
	public GameObject exit;
	public GameObject[] floorTiles;
	public GameObject[] enemyTiles;
	public GameObject[] skyTiles;
	private Transform boardHolder;
	private List <Vector3> gridPositions = new List<Vector3>();


	void InitialiseList()
	{
		gridPositions.Clear();

		for (int x=0; x < columns; x++){
			for(int y=0; y <2; y++){

				gridPositions.Add(new Vector3(x,y,0f));
			}
		}
	}


//THIS MAKES YOU BUILD THE LEVEL MATs
	void BoardSetup()
	{
		boardHolder = new GameObject ("Board").transform;
		for (int x=0; x < columns; x++)
		{
			for( int y=0; y<rows; y++)
			{
				GameObject toInstantiate = floorTiles[Random.Range (0, floorTiles.Length)];
				if ( y == 0 || y == 1 ){
					toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
					GameObject instance = Instantiate(toInstantiate, new Vector3 (x,y,0f), Quaternion.identity) as GameObject;
					instance.transform.SetParent (boardHolder);
					}
				else{
					toInstantiate = skyTiles[Random.Range(0, skyTiles.Length)];
					GameObject instance = Instantiate(toInstantiate, new Vector3 (x,y,0f), Quaternion.identity) as GameObject;
					instance.transform.SetParent (boardHolder);
					}
			}
		}
	}

	Vector3 RandomPosition()
	{
		int randomIndex = Random.Range (0, gridPositions.Count);
		Vector3 randomPosition = gridPositions [randomIndex];
		gridPositions.RemoveAt (randomIndex);
		return randomPosition;
	}



	void LayoutObjectAtRandom(GameObject[]  tileArray, int minimum, int maximum)
	{
		int objectCount = Random.Range (minimum, maximum + 1);
		for (int i=0; i<objectCount; i++)
		{

			Vector3 randomPosition = RandomPosition();
			GameObject tileChoice = tileArray[Random.Range (0, tileArray.Length)];
			Instantiate(tileChoice, randomPosition, Quaternion.identity);

		}

	}



	public void SetupScene(int level)
	{
		BoardSetup ();
		InitialiseList ();
		//LayoutObjectAtRandom (wallTiles, wallCount.minimum, wallCount.maximum);
		//LayoutObjectAtRandom (foodTiles, foodcount.minimum, foodcount.maximum);
		int enemyCount = (int)Mathf.Log (level, 2f);
		LayoutObjectAtRandom (enemyTiles, enemyCount, enemyCount);
		Instantiate (exit, new Vector3 (columns-1, 1, 0f), Quaternion.identity);

	}



}
