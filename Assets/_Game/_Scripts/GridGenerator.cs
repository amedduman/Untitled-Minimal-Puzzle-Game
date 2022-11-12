using System;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;

public class GridGenerator : MonoBehaviour 
{
	[SerializeField] Camera gameCam;
	[SerializeField] Tile tilePrefab;
	[SerializeField] float _tilesPosOnZAxis;
	[SerializeField] int xSize = 8;
	[SerializeField] int ySize = 8;
	
	Tile[,] tiles;
#if UNITY_EDITOR
	
	[PropertySpace] [Button]
	void GenerateGrid () 
	{
		tiles = new Tile[xSize, ySize];
		
		// calculate needed distance between tiles
		Vector2 offset = tilePrefab.GetComponentInChildren<SpriteRenderer>().bounds.size;
		
		// calculate grid size
		float gridWidth = offset.x * xSize;
		float gridHeight = offset.y * ySize;
		
        // set grid's parent object's position
        Vector3 pos = gameCam.transform.position;

        pos.y -= Mathf.Abs(gridHeight / 2);
        pos.y += offset.y / 2;

        pos.x -= Mathf.Abs(gridWidth / 2);
        pos.x += offset.x / 2;

        pos.z = _tilesPosOnZAxis;
        
        transform.position = pos;

		// generate grid
		for (int i = 0; i < xSize; i++) 
		{
			for (int j = 0; j < ySize; j++) 
			{
				Tile tile = PrefabUtility.InstantiatePrefab(tilePrefab, transform) as Tile ;

                tile.transform.localPosition = new Vector3(offset.x * i, offset.y * j, 0);

				NameTile(tile, i, j);

				SetTileIds(tile, i, j);
				
				tiles[i, j] = tile; 
			}
        }
    }
#endif

	private void Start()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			Tile tile = transform.GetChild(i).GetComponent<Tile>();
			tile.SetNeighborsNewMethod();
		}
	}

	void SetTileIds(Tile tile, int x, int y)
	{
		tile.tileIdX = x;
		tile.tileIdY = y;
	}

	void NameTile(Tile tile, int x, int y)
	{
		tile.gameObject.name = $"Tile ({x + 1},{y + 1})";
	}

	[PropertySpace] [Button]
    void DestroyGrid()
	{
		for (int i = transform.childCount - 1; i >= 0; i--)
		{
			DestroyImmediate(transform.GetChild(i).gameObject);
		}	
	}
}
