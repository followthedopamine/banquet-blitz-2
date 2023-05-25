using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DestroyTiles : MonoBehaviour {
  private void OnEnable() {
    EventManager.MatchesFound += DestroyMatches;
  }

  private void OnDisable() {
    EventManager.MatchesFound -= DestroyMatches;
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
        DestroyOverlayTileAtPosition(tilePosition);
        destroyedTiles.Add(tilePosition);
      }
    }
    if (destroyedTiles.Count > 0) {
      Debug.Log("Tiles destroyed: " + destroyedTiles.Count);
      EventManager.DestroyedTiles(destroyedTiles);
    }
  }

  private void DestroyOverlayTileAtPosition(Vector3Int tilePosition) {
    Tilemap overlayTilemap = GameManager.Instance.levelManager.overlayTilemap;
    if (!overlayTilemap.HasTile(tilePosition)) return;
    // Might need a check here in future if any overlay tiles aren't destroyed by a match underneath
    overlayTilemap.SetTile(tilePosition, null);
  }
}
