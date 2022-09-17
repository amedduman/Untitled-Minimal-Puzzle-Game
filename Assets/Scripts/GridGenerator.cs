using UnityEngine;

public class GridGenerator : MonoBehaviour 
{
	[SerializeField] Camera gameCam;
	[SerializeField] Tile tilePrefab;
	[SerializeField] int xSize = 8;
	[SerializeField] int ySize = 8;
	
	Tile[,] tiles;

	void Start () 
	{
		tiles = new Tile[xSize, ySize];
		
		// calculate needed distance between tiles
		Vector2 offset = tilePrefab.TileSpriteRenderer.bounds.size;
		
		// calculate grid size
		float gridWidth = offset.x * xSize;
		float gridHeight = offset.y * ySize;
		
        // set grid's parent object's position
        Vector3 pos = gameCam.transform.position;

        pos.y -= Mathf.Abs(gridHeight / 2);
        pos.y += offset.y / 2;

        pos.x -= Mathf.Abs(gridWidth / 2);
        pos.x += offset.x / 2;

        pos.z = 0;
        
        transform.position = pos;

		// generate grid
		for (int i = 0; i < xSize; i++) 
		{
			for (int j = 0; j < ySize; j++) 
			{
				Tile tile = Instantiate(tilePrefab, transform);

                tile.transform.localPosition = new Vector3(offset.x * i, offset.y * j, 0);

				NameTile(tile, i, j);

				SetTileIds(tile, i, j);
				
				tiles[i, j] = tile; 
			}
        }

		foreach (var tile in tiles)
		{
			tile.SetNeighbors(tiles);
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
}
