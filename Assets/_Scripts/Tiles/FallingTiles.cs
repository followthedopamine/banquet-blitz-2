using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FallingTiles : MonoBehaviour {

  public static float FALL_SPEED = 5.5f;
  private int tilesFalling = 0;

  private void OnEnable() {
    EventManager.SpawnedTiles += FindTilesThatNeedToFall;
  }

  private void OnDisable() {
    EventManager.SpawnedTiles -= FindTilesThatNeedToFall;
  }

  private void FindTilesThatNeedToFall(List<Vector3Int> emptyTiles) {
    Tilemap levelTilemap = GameManager.Instance.levelManager.levelTilemap;
    List<Vector3Int> containerTilePositions = GameManager.Instance.levelManager.containerTilePositions;
    List<Vector3Int> remainingTiles = TilemapHelper.GetTilePositions(levelTilemap);
    // <Hole to be filled, tile to fall there>
    List<Pair<Vector3Int, Vector3Int>> tilesThatNeedToFall = new();
    emptyTiles = SortTilePositionsByYValue(emptyTiles);

    while (emptyTiles.Count > 0) {
      Vector3Int emptyTile = emptyTiles[0];
      Pair<Vector3Int, int> replacementTile = FindClosestTileWithPositiveYInList(emptyTile, remainingTiles);
      Pair<Vector3Int, Vector3Int> tilePair = new(emptyTile, replacementTile.First);

      if (!ShouldTileFall(replacementTile.First)) {
        remainingTiles.RemoveAt(replacementTile.Second);
        // emptyTiles.RemoveAt(0);
        continue;
      }

      tilesThatNeedToFall.Add(tilePair);
      emptyTiles.RemoveAt(0);
      if (containerTilePositions.Contains(replacementTile.First)) {
        emptyTiles.Add(replacementTile.First);
        emptyTiles = SortTilePositionsByYValue(emptyTiles); // TODO: Optimize by inserting into correct position instead of inserting then sorting
      }
      remainingTiles.RemoveAt(replacementTile.Second);
    }
    MakeTilesFall(tilesThatNeedToFall);
  }

  private bool ShouldTileFall(Vector3Int tilePosition) {
    // Might need additional checks here if any overlay tiles don't freeze tile underneath
    Tilemap overlayTilemap = GameManager.Instance.levelManager.overlayTilemap;
    return overlayTilemap.GetTile(tilePosition) == null;
  }

  private List<Vector3Int> SortTilePositionsByYValue(List<Vector3Int> tilePositions) {
    tilePositions.Sort((a, b) => a.y.CompareTo(b.y));
    return tilePositions;
  }

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

  private bool IsTileAboveFrozen(Vector3Int tilePosition) {
    Vector3Int tileAbove = new(tilePosition.x, tilePosition.y + 1, tilePosition.z);
    return !ShouldTileFall(tileAbove);
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
  }

  private IEnumerator DropTile(Vector3Int fallingTile, Vector3Int emptyTile) {
    Tilemap levelTilemap = GameManager.Instance.levelManager.levelTilemap;
    Vector3 emptyTileWorldPosition = levelTilemap.GetCellCenterWorld(emptyTile);
    GameTile tile = levelTilemap.GetTile<GameTile>(fallingTile);
    GameObject fallingTileObject = TilemapHelper.ReplaceTileWithGameObject(levelTilemap, fallingTile, true);
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
  }
}
