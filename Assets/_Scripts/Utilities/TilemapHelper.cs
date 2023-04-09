using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapHelper : MonoBehaviour {
  public static GameObject ReplaceTileWithGameObject(Tilemap tilemap, Vector3Int tilePosition) {
    Vector3 worldPosition = tilemap.GetCellCenterWorld(tilePosition);
    Tile tile = tilemap.GetTile<Tile>(tilePosition);
    GameObject newTile = new();
    // Match game grid scale
    Transform grid = tilemap.transform.parent;
    newTile.transform.localScale = grid.localScale;
    SpriteRenderer spriteRenderer = newTile.AddComponent<SpriteRenderer>();
    spriteRenderer.sprite = tile.sprite;
    tilemap.SetTile(tilePosition, null);
    newTile.transform.position = worldPosition;
    return newTile;
  }

  public static GameObject CopyTileAsGameObject(Tilemap tilemap, Vector3Int tilePosition) {
    Vector3 worldPosition = tilemap.GetCellCenterWorld(tilePosition);
    Tile tile = tilemap.GetTile<Tile>(tilePosition);
    GameObject newTile = new();
    // Match game grid scale
    Transform grid = tilemap.transform.parent;
    newTile.transform.localScale = grid.localScale;
    SpriteRenderer spriteRenderer = newTile.AddComponent<SpriteRenderer>();
    spriteRenderer.sprite = tile.sprite;
    newTile.transform.position = worldPosition;
    return newTile;
  }

  public static GameObject CopyTileAsSpriteMask(Tilemap tilemap, Vector3Int tilePosition) {
    Vector3 worldPosition = tilemap.GetCellCenterWorld(tilePosition);
    Tile tile = tilemap.GetTile<Tile>(tilePosition);
    GameObject newTile = new();
    // Match game grid scale
    Transform grid = tilemap.transform.parent;
    newTile.transform.localScale = grid.localScale;
    newTile.transform.position = worldPosition;
    SpriteMask mask = newTile.AddComponent<SpriteMask>();
    mask.sprite = tile.sprite;
    return newTile;
  }



  public static List<Vector3Int> GetTilePositions(Tilemap tilemap) {
    List<Vector3Int> tiles = new();
    foreach (Vector3Int position in tilemap.cellBounds.allPositionsWithin) {
      if (tilemap.HasTile(position)) {
        tiles.Add(position);
      }
    }
    return tiles;
  }
}
