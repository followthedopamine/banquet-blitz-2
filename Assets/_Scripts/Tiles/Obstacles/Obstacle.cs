using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour {
  [HideInInspector] public int tileId;
  [HideInInspector] public bool isDamagedByNearDestroyedTile = true;
  [HideInInspector] public int nextDamagedTileId = -1;
  [HideInInspector] public bool tileFallsNormally = true;
  [HideInInspector] public bool isInCoverTilemap = false;

  private void OnEnable() {
    if (isDamagedByNearDestroyedTile) EventManager.DestroyedTiles += DamageTileByNearDestroyedTile;
  }

  private void OnDisable() {
    if (isDamagedByNearDestroyedTile) EventManager.DestroyedTiles -= DamageTileByNearDestroyedTile;
  }

  private void DamageTileByNearDestroyedTile(List<Vector3Int> destroyedTiles) {

  }
}
