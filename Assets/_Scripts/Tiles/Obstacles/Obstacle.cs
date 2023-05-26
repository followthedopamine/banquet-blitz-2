using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class Obstacle : MonoBehaviour {
  [HideInInspector] public int tileId;
  [HideInInspector] public bool isDamagedByNearDestroyedTile = false;
  [HideInInspector] public int nextDamagedTileId = -1;
  [HideInInspector] public bool tileFallsNormally = true;
  // [HideInInspector] public bool isInCoverTilemap = false;
  [HideInInspector] public bool isDestroyedByUnderTileMatch = false;
  [HideInInspector] public bool freezesUnderTile = false;
  [HideInInspector] public bool destroyTileInBottomRow = false;


  private void OnEnable() {
    EventManager.DestroyedTiles += DamageTileByNearDestroyedTile;
    EventManager.DestroyedTiles += FallOffBottom;
  }

  private void OnDisable() {
    EventManager.DestroyedTiles -= DamageTileByNearDestroyedTile;
    EventManager.DestroyedTiles -= FallOffBottom;
  }

  private void DamageTileByNearDestroyedTile(List<Vector3Int> destroyedTiles) {
    if (!isDamagedByNearDestroyedTile) return;
    Tilemap levelTilemap = GameManager.Instance.levelManager.levelTilemap;
    foreach (Vector3Int tilePosition in destroyedTiles) {
      List<Vector3Int> neighbourTiles = TilemapHelper.GetNeighbourTilePositions(tilePosition);
      foreach (Vector3Int neighbourTile in neighbourTiles) {
        if (!levelTilemap.HasTile(neighbourTile)) continue;
        if (levelTilemap.GetTile<GameTile>(neighbourTile).id == tileId) {
          DamageTile(neighbourTile);
        }
      }
    }
  }

  private void DamageTile(Vector3Int tilePosition) {
    Tilemap levelTilemap = GameManager.Instance.levelManager.levelTilemap;

    if (nextDamagedTileId == -1) {
      DestroyTile(tilePosition);
      return;
    }
    foreach (GameTile gameTile in GameManager.Instance.allGameTiles) {
      if (gameTile.id == nextDamagedTileId) {
        levelTilemap.SetTile(tilePosition, gameTile);
      }
    }
  }

  private void DestroyTile(Vector3Int tilePosition) {
    Match fakeMatch = new();
    fakeMatch.tileId = tileId;
    fakeMatch.location = tilePosition;
    fakeMatch.tilePositions = new() { tilePosition };
    List<Match> fakeMatches = new() { fakeMatch };
    EventManager.MatchesFound(fakeMatches);
  }

  private void FallOffBottom(List<Vector3Int> destroyedTiles) {
    if (!destroyTileInBottomRow) return;
    // Get all anvil tiles
    Tilemap levelTilemap = GameManager.Instance.levelManager.levelTilemap;
    List<Vector3Int> allFallOffBottomTiles = TilemapHelper.GetAllTilesOfId(levelTilemap, tileId);
    // Check if all tiles underneath every anvil are destroyed
    foreach (Vector3Int fallOffBottomTile in allFallOffBottomTiles) {
      int bottomRow = GetBottomYValueOfTilemapForTile(fallOffBottomTile);
      Vector3Int bottomTile = new(fallOffBottomTile.x, bottomRow, fallOffBottomTile.z);
      Debug.Log("Bottom row under anvil: " + bottomRow);
      List<Vector3Int> betweenTiles = GetTilesBetweenTwoTilesOnYAxis(fallOffBottomTile, bottomTile);
      betweenTiles.Add(bottomTile);
      for (int i = 0; i < betweenTiles.Count; i++) {
        Vector3Int betweenTile = betweenTiles[i];
        if (destroyedTiles.Contains(betweenTile)) {
          betweenTiles.RemoveAt(i);
          i--;
        }
      }
      if (betweenTiles.Count == 0) {
        // All tiles underneath anvil are destroyed
        Debug.Log("All tiles underneath anvil are destroyed");
        // If tiles underneath any anvil are destroyed then make it fall off the bottom and be destroyed
        // Also need to spawn in an additional tile to fall into place of the anvil
        Vector3Int tileBelowBottomRow = new(bottomTile.x, bottomTile.y - 1, bottomTile.z);
        StartCoroutine(DropTileOffTilemap(fallOffBottomTile, tileBelowBottomRow));
      }
    }
  }

  private int GetBottomYValueOfTilemapForTile(Vector3Int tilePosition) {
    List<Vector3Int> containerTilePositions = GameManager.Instance.levelManager.containerTilePositions;
    int bottomY = int.MaxValue;
    for (int i = 0; i < containerTilePositions.Count; i++) {
      Vector3Int listPosition = containerTilePositions[i];
      if (listPosition.x != tilePosition.x) continue;
      if (listPosition.y < bottomY) {
        bottomY = listPosition.y;
      }
    }
    return bottomY;
  }

  private List<Vector3Int> GetTilesBetweenTwoTilesOnYAxis(Vector3Int higherTile, Vector3Int lowerTile) {
    List<Vector3Int> containerTilePositions = GameManager.Instance.levelManager.containerTilePositions;
    List<Vector3Int> betweenTiles = new();
    foreach (Vector3Int containerTile in containerTilePositions) {
      if (containerTile.x != higherTile.x) continue;
      if (containerTile.y < higherTile.y && containerTile.y > lowerTile.y) {
        betweenTiles.Add(containerTile);
      }
    }
    return betweenTiles;
  }

  // Very similar function to FallingTiles.DropTile()
  private IEnumerator DropTileOffTilemap(Vector3Int fallingTile, Vector3Int emptyTile) {
    Tilemap levelTilemap = GameManager.Instance.levelManager.levelTilemap;
    Vector3 emptyTileWorldPosition = levelTilemap.GetCellCenterWorld(emptyTile);
    // GameTile tile = levelTilemap.GetTile<GameTile>(fallingTile);
    GameObject fallingTileObject = TilemapHelper.ReplaceTileWithGameObject(levelTilemap, fallingTile, true);
    while (fallingTileObject.transform.position != emptyTileWorldPosition) {
      fallingTileObject.transform.position = Vector3.MoveTowards(fallingTileObject.transform.position, emptyTileWorldPosition, FallingTiles.FALL_SPEED * Time.deltaTime);
      yield return new WaitForEndOfFrame();
    }
    Destroy(fallingTileObject);
    // Could be a bug here where tile won't have finished falling and EventManager.TilesFinishedFalling triggers anyway because this tile is not tracked in any way

    // TODO: Should give score for this
  }
}
