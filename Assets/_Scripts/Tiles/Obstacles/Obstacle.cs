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
    Debug.Log("Should return: " + isDamagedByNearDestroyedTile);
    if (!isDamagedByNearDestroyedTile) return;
    Debug.Log("Tiles being checked by obstacle script");
    Tilemap levelTilemap = GameManager.Instance.levelManager.levelTilemap;
    foreach (Vector3Int tilePosition in destroyedTiles) {
      List<Vector3Int> neighbourTiles = TilemapHelper.GetNeighbourTilePositions(tilePosition);
      foreach (Vector3Int neighbourTile in neighbourTiles) {
        if (!levelTilemap.HasTile(neighbourTile)) continue;
        Debug.Log(levelTilemap.GetTile<GameTile>(neighbourTile).id);
        Debug.Log(tileId);
        if (levelTilemap.GetTile<GameTile>(neighbourTile).id == tileId) {
          DamageTile(neighbourTile);
        }
      }
    }
  }

  private void DamageTile(Vector3Int tilePosition) {
    Tilemap levelTilemap = GameManager.Instance.levelManager.levelTilemap;

    if (nextDamagedTileId == -1) {
      DestroyTile();
      return;
    }
    foreach (GameTile gameTile in GameManager.Instance.allGameTiles) {
      if (gameTile.id == nextDamagedTileId) {
        levelTilemap.SetTile(tilePosition, gameTile);
      }
    }
    Debug.Log("Obstacle damaged by nearby tile being destroyed");
  }

  private void DestroyTile() {

  }


}
