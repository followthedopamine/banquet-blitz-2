using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapHelper : MonoBehaviour {
  public static GameObject ReplaceTileWithGameObject(Tilemap tilemap, Vector3Int tilePosition, bool useMask = false) {
    Vector3 worldPosition = tilemap.GetCellCenterWorld(tilePosition);
    Tile tile = tilemap.GetTile<Tile>(tilePosition);
    if (tile == null) {
      Debug.LogError("Tile is null at " + tilePosition + " in " + tilemap.name);
    }
    GameObject newTile = new();
    // Match game grid scale
    Transform grid = tilemap.transform.parent;
    newTile.transform.localScale = grid.localScale;
    SpriteRenderer spriteRenderer = newTile.AddComponent<SpriteRenderer>();
    spriteRenderer.sprite = tile.sprite;
    if (useMask) {
      spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
    }
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

  public static List<Vector3Int> GetNeighbourTilePositions(Vector3Int tile) {
    Vector3Int northTile = new(tile.x, tile.y + 1, tile.z);
    Vector3Int southTile = new(tile.x, tile.y - 1, tile.z);
    Vector3Int eastTile = new(tile.x + 1, tile.y, tile.z);
    Vector3Int westTile = new(tile.x - 1, tile.y, tile.z);
    List<Vector3Int> neighbourTilePositions = new() { northTile, southTile, eastTile, westTile };
    return neighbourTilePositions;
  }

  public static List<Vector3Int> GetAllTilesOfId(Tilemap tilemap, int tileId) {
    List<Vector3Int> allTiles = GetTilePositions(tilemap);
    for (int i = 0; i < allTiles.Count; i++) {
      Vector3Int tile = allTiles[i];
      if (tilemap.GetTile<GameTile>(tile).id != tileId) {
        allTiles.RemoveAt(i);
        i--;
      }
    }
    return allTiles;
  }
}
