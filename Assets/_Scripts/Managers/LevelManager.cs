using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour {
  [HideInInspector] public Tilemap levelTilemap;
  [HideInInspector] public Tilemap containerTilemap;
  [HideInInspector] public List<Vector3Int> containerTilePositions;
  [HideInInspector] public int score;

  public int movesRemaining = -1;
  public int timeRemaining = -1;
  public Goals.Type goalType;
  public List<GameTile> goalTile;
  public List<int> goalRemaining;
  public List<GameTile> spawnableTiles;

  private void Start() {
    levelTilemap = GameObject.Find("Level Tilemap").GetComponent<Tilemap>();
    containerTilemap = GameObject.Find("Container Tilemap").GetComponent<Tilemap>();
    containerTilePositions = TilemapHelper.GetTilePositions(containerTilemap);
    EventManager.LevelLoaded(this);
  }
}
