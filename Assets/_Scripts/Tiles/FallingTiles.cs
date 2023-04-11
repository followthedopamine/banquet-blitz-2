using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FallingTiles : MonoBehaviour {

  private const float FALL_SPEED = 5.5f;
  private int tilesFalling = 0;

  private void Start() {
    EventManager.SpawnedTiles += FindTilesThatNeedToFall;
  }

  private void FindTilesThatNeedToFall(List<Vector3Int> emptyTiles) {
    // <Hole to be filled, tile to fall there>
    //List<Pair<Vector3Int, Vector3Int>>
    Tilemap levelTilemap = GameManager.Instance.levelManager.levelTilemap;
    List<Vector3Int> containerTilePositions = GameManager.Instance.levelManager.containerTilePositions;
    List<Vector3Int> remainingTiles = TilemapHelper.GetTilePositions(levelTilemap);
    List<Pair<Vector3Int, Vector3Int>> tilesThatNeedToFall = new();
    emptyTiles = SortTilePositionsByYValue(emptyTiles);

    while (emptyTiles.Count > 0) {
      // Vector3Int emptyTile in emptyTiles
      Vector3Int emptyTile = emptyTiles[0];
      Pair<Vector3Int, int> replacementTile = FindClosestTileWithPositiveYInList(emptyTile, remainingTiles);
      if (replacementTile.Second == -1) {
        Debug.LogError("Empty tile " + emptyTile + " could not find a replacement tile");
        break;
      }
      Pair<Vector3Int, Vector3Int> tilePair = new(emptyTile, replacementTile.First);
      tilesThatNeedToFall.Add(tilePair);
      // Remove replacement tile from remainingTiles list
      // Check if the replacement tile index is -1, if it is then something went horribly wrong
      emptyTiles.RemoveAt(0);
      if (containerTilePositions.Contains(replacementTile.First)) {
        emptyTiles.Add(replacementTile.First);
        emptyTiles = SortTilePositionsByYValue(emptyTiles); // TODO: Optimize by inserting into correct position instead of inserting then sorting
      }
      remainingTiles.RemoveAt(replacementTile.Second);
    }
    string log = "";
    foreach (Pair<Vector3Int, Vector3Int> tile in tilesThatNeedToFall) {
      log += tile.Second + " falls to " + tile.First + " ";
    }
    Debug.Log(log);
    MakeTilesFall(tilesThatNeedToFall);
  }

  private List<Vector3Int> SortTilePositionsByYValue(List<Vector3Int> tilePositions) {
    tilePositions.Sort((a, b) => a.y.CompareTo(b.y));
    return tilePositions;
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
    List<IEnumerator> fallingTilesCoroutines = new();
    foreach (Pair<Vector3Int, Vector3Int> tilePair in tilesThatNeedToFall) {
      Vector3Int emptyTile = tilePair.First;
      Vector3Int fallingTile = tilePair.Second;
      fallingTilesCoroutines.Add(DropTile(fallingTile, emptyTile));
      StartCoroutine(DropTile(fallingTile, emptyTile));
      tilesFalling++;
    }
    // foreach (IEnumerator coroutine in fallingTilesCoroutines) {
    //   StartCoroutine(coroutine);
    // }
    // List<Vector3Int> emptyTiles = GetAllEmptyTiles();
    // FindTilesThatNeedToFall(emptyTiles);
  }

  private IEnumerator DropTile(Vector3Int fallingTile, Vector3Int emptyTile) {
    Tilemap levelTilemap = GameManager.Instance.levelManager.levelTilemap;
    // Vector3 fallingTileWorldPosition = levelTilemap.GetCellCenterWorld(fallingTile);
    Vector3 emptyTileWorldPosition = levelTilemap.GetCellCenterWorld(emptyTile);
    GameTile tile = levelTilemap.GetTile<GameTile>(fallingTile);
    GameObject fallingTileObject = TilemapHelper.ReplaceTileWithGameObject(levelTilemap, fallingTile, false);
    while (fallingTileObject.transform.position != emptyTileWorldPosition) {
      fallingTileObject.transform.position = Vector3.MoveTowards(fallingTileObject.transform.position, emptyTileWorldPosition, FALL_SPEED * Time.deltaTime);
      yield return new WaitForEndOfFrame();
    }
    Destroy(fallingTileObject);

    levelTilemap.SetTile(emptyTile, tile);
    tilesFalling--;
    if (tilesFalling == 0) {
      EventManager.TilesFinishedFalling();
    }
    // falling--;
    // yield return new WaitForSeconds(0.3f); // TODO: Change to tile falling time
  }

  // private List<Vector3Int> GetAllEmptyTiles() {
  //   List<Vector3Int> containerTilePositions = GameManager.Instance.levelManager.containerTilePositions;
  //   List<Vector3Int> emptyTile = new();
  //   foreach (Vector3Int containerTilePosition in containerTilePositions) {

  //   }
  // }
}
