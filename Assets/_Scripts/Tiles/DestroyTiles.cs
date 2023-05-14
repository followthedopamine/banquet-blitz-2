using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DestroyTiles : MonoBehaviour {
  private void Start() {
    EventManager.MatchesFound += DestroyMatches;
  }

  private void DestroyTileAtPosition(Vector3Int tilePosition) {
    Tilemap levelTilemap = GameManager.Instance.levelManager.levelTilemap;
    levelTilemap.SetTile(tilePosition, null);
  }

  private void DestroyMatches(List<Match> matches) {
    List<Vector3Int> destroyedTiles = new();
    foreach (Match match in matches) {
      foreach (Vector3Int tilePosition in match.tilePositions) {
        DestroyTileAtPosition(tilePosition);
        destroyedTiles.Add(tilePosition);
      }
    }
    if (destroyedTiles.Count > 0) {
      Debug.Log(destroyedTiles.Count);
      EventManager.DestroyedTiles(destroyedTiles);
    }
  }
}
