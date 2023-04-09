using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnTiles : MonoBehaviour {

  private void Start() {
    EventManager.DestroyedTiles += SpawnMissingTiles;
  }

  private void SpawnMissingTiles(List<Vector3Int> destroyedTiles) {
    Tilemap levelTilemap = GameManager.Instance.levelManager.levelTilemap;

    Dictionary<int, int> map = new();
    foreach (Vector3Int destroyedTile in destroyedTiles) {
      if (map.ContainsKey(destroyedTile.x)) {
        map[destroyedTile.x] = map[destroyedTile.x] + 1;
      } else {
        map.Add(destroyedTile.x, 1);
      }
    }

    foreach (KeyValuePair<int, int> keyValuePair in map) {
      GameTile newTile = GetRandomSpawnableTile();
      int column = keyValuePair.Key;
      int count = keyValuePair.Value;
      int firstRowSpawn = GetTopRowOfColumn(column) + 1;
      for (int i = 0; i < count; i++) {
        Vector3Int positionToSpawnTile = new(column, firstRowSpawn + i, 0);
        levelTilemap.SetTile(positionToSpawnTile, newTile);
      }
    }
  }

  private int GetTopRowOfColumn(int column) {
    List<Vector3Int> containerTilePositions = GameManager.Instance.levelManager.containerTilePositions;
    int maxY = int.MinValue;
    foreach (Vector3Int containerTilePosition in containerTilePositions) {
      if (column != containerTilePosition.x) continue;
      maxY = Mathf.Max(maxY, containerTilePosition.y);
    }
    return maxY;
  }

  private GameTile GetRandomSpawnableTile() {
    List<GameTile> spawnableTiles = GameManager.Instance.levelManager.spawnableTiles;
    return spawnableTiles[Random.Range(0, spawnableTiles.Count)];
  }
}
