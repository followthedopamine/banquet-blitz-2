using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FallingTiles : MonoBehaviour {

  private void Start() {
    EventManager.SpawnedTiles += FindTilesThatNeedToFall;
  }

  private void FindTilesThatNeedToFall(List<Vector3Int> emptyTiles) {
    // <Hole to be filled, tile to fall there>
    //List<Pair<Vector3Int, Vector3Int>>
    Tilemap levelTilemap = GameManager.Instance.levelManager.levelTilemap;
    List<Vector3Int> remainingTiles = TilemapHelper.GetTilePositions(levelTilemap);
    List<Pair<Vector3Int, Vector3Int>> tilesThatNeedToFall = new();
    foreach (Vector3Int emptyTile in emptyTiles) {
      Vector3Int replacementTile = FindClosestTileWithPositiveYInList(emptyTile, remainingTiles);
      Pair<Vector3Int, Vector3Int> tilePair = new(emptyTile, replacementTile);
      tilesThatNeedToFall.Add(tilePair);
      // Remove replacement tile from remainingTiles list
      for (int i = 0; i < remainingTiles.Count; i++) {
        if (remainingTiles[i] == replacementTile) {
          remainingTiles.RemoveAt(i);
          break;
        }
      }
    }
    MakeTilesFall(tilesThatNeedToFall);
  }

  private Vector3Int FindClosestTileWithPositiveYInList(Vector3Int tile, List<Vector3Int> tileList) {

  }

  private void MakeTilesFall(List<Pair<Vector3Int, Vector3Int>> tilesThatNeedToFall) {

  }
}
