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
      Pair<Vector3Int, int> replacementTile = FindClosestTileWithPositiveYInList(emptyTile, remainingTiles);
      Pair<Vector3Int, Vector3Int> tilePair = new(emptyTile, replacementTile.First);
      tilesThatNeedToFall.Add(tilePair);
      // Remove replacement tile from remainingTiles list
      // Check if the replacement tile index is -1, if it is then something went horribly wrong
      if (replacementTile.Second == -1) break;
      remainingTiles.RemoveAt(replacementTile.Second);
    }
    foreach (Pair<Vector3Int, Vector3Int> tile in tilesThatNeedToFall) {
      Debug.Log(tile.First + " " + tile.Second);
    }
    MakeTilesFall(tilesThatNeedToFall);
  }

  // Should also return index of list for easy removal from list
  private Pair<Vector3Int, int> FindClosestTileWithPositiveYInList(Vector3Int tile, List<Vector3Int> tileList) {
    int closestPositiveY = int.MaxValue;
    int closestPositiveYIndex = -1;
    for (int i = 0; i < tileList.Count; i++) {
      Vector3Int listPosition = tileList[i];
      if (listPosition.x != tile.x) continue;
      if (listPosition.y < closestPositiveY && listPosition.y > tile.y) {
        closestPositiveY = listPosition.y;
        closestPositiveYIndex = i;
      }
    }
    Pair<Vector3Int, int> result = new(tileList[closestPositiveYIndex], closestPositiveYIndex);
    return result;
  }

  private void MakeTilesFall(List<Pair<Vector3Int, Vector3Int>> tilesThatNeedToFall) {

  }
}
