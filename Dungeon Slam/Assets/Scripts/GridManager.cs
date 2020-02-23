using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

  // The unity coords of the bottom left tile in this grid (center of tile)
  public Vector2 bottomLeft;
  private Vector2 blCorner;

  // Tiles must be square
  private float tileSize;

  // Start is called before the first frame update
  void Start()
  {
    blCorner = bottomLeft - new Vector2(tileSize / 2, tileSize / 2);

  }

  // Update is called once per frame
  void Update()
  {

  }

  // Returns grid coords of world coords (world coords anywhere in tile)
  Vector2Int WorldToGrid(Vector2 worldCoords)
  {
    Vector2 gridCoords = new Vector2Int();
    gridCoords = worldCoords - blCorner;
    gridCoords = gridCoords / tileSize;

    return new Vector2Int((int)gridCoords.x, (int)gridCoords.y);

  }

  // Returns world coords of grid coords
  Vector2 GridToWorld(Vector2Int gridCoords)
  {
    return gridCoords + new Vector2(bottomLeft.x, bottomLeft.y);


  }
}
