using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class Obstacle : MonoBehaviour {
  [HideInInspector] public int tileId;
  [HideInInspector] public bool isDamagedByNearDestroyedTile = true;
  [HideInInspector] public int nextDamagedTileId = -1;
  [HideInInspector] public bool tileFallsNormally = true;
  [HideInInspector] public bool isInCoverTilemap = false;

  private void OnEnable() {
    EventManager.DestroyedTiles += DamageTileByNearDestroyedTile;
  }

  private void OnDisable() {
    EventManager.DestroyedTiles -= DamageTileByNearDestroyedTile;
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


}
