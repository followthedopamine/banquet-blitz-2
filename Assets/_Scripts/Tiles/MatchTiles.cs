using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public struct Match {
  public int tileId;
  public Vector3Int location;
  public List<Vector3Int> tilePositions;
}

public class MatchTiles : MonoBehaviour {
  private const int MINIMUM_MATCH_SIZE = 4;

  private void Start() {
    EventManager.TilesSwitched += GetAllMatches;
    EventManager.TilesFinishedFalling += GetAllMatches;
  }

  public void GetAllMatches() {
    Tilemap levelTilemap = GameManager.Instance.levelManager.levelTilemap;
    Stack<Vector3Int> tilesRemaining = new();
    List<Vector3Int> visitedTiles = new();
    List<Match> matches = new();

    foreach (Vector3Int position in levelTilemap.cellBounds.allPositionsWithin) {
      if (!levelTilemap.HasTile(position)) continue;
      if (visitedTiles.Contains(position) || tilesRemaining.Contains(position)) continue;
      tilesRemaining.Push(position);
      Match match = InitializeMatch(position);

      while (tilesRemaining.Count > 0) {
        Vector3Int tilePosition = tilesRemaining.Pop();
        if (levelTilemap.HasTile(tilePosition) && !tilesRemaining.Contains(tilePosition)) {
          GameTile tile = levelTilemap.GetTile<GameTile>(tilePosition);
          if (match.tileId == 0) {
            match.tileId = tile.id;
          }
          if (tile.id == match.tileId) {
            match.tilePositions.Add(tilePosition);
            visitedTiles.Add(tilePosition);

            Vector3Int east = new(tilePosition.x - 1, tilePosition.y, tilePosition.z);
            Vector3Int west = new(tilePosition.x + 1, tilePosition.y, tilePosition.z);
            Vector3Int south = new(tilePosition.x, tilePosition.y - 1, tilePosition.z);
            Vector3Int north = new(tilePosition.x, tilePosition.y + 1, tilePosition.z);
            if (!visitedTiles.Contains(east))
              tilesRemaining.Push(east);
            if (!visitedTiles.Contains(west))
              tilesRemaining.Push(west);
            if (!visitedTiles.Contains(north))
              tilesRemaining.Push(north);
            if (!visitedTiles.Contains(south))
              tilesRemaining.Push(south);
          }
        }
      }
      if (match.tilePositions.Count >= MINIMUM_MATCH_SIZE) matches.Add(match);
    }
    if (matches.Count > 0) {
      PrintMatches(matches);
      EventManager.MatchesFound(matches);
    }
  }

  private Match InitializeMatch(Vector3Int position) {
    Match match;
    match.location = position;
    match.tilePositions = new List<Vector3Int>();
    match.tileId = 0;
    return match;
  }

  private void PrintMatches(List<Match> matches) {
    Debug.Log("Total matches " + matches.Count);
    foreach (Match match in matches) {
      Debug.Log("Tile id: " + match.tileId);
      Debug.Log("Match size: " + match.tilePositions.Count);
      string matchPositions = "";
      foreach (Vector3Int tilePosition in match.tilePositions) {
        matchPositions += tilePosition;
      }
      Debug.Log("Positions: " + matchPositions);
    }
  }
}
