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
    foreach (Match match in matches) {
      foreach (Vector3Int tilePosition in match.tilePositions) {
        DestroyTileAtPosition(tilePosition);
      }
    }
  }
}
