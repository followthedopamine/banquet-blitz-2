using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour {
  [HideInInspector] public Tilemap levelTilemap;
  [HideInInspector] public Tilemap containerTilemap;
  [HideInInspector] public List<Vector3Int> containerTilePositions;
  /*[HideInInspector]*/
  public int score;

  public int movesRemaining = -1;
  public float timeRemaining = -1;
  public List<GameTile> goalTiles;
  public List<int> goalRemaining;
  public List<GameTile> spawnableTiles;
  public List<int> trophyScores;
  public bool gameLoopRunning;

  private void Start() {
    levelTilemap = GameObject.Find("Level Tilemap").GetComponent<Tilemap>();
    containerTilemap = GameObject.Find("Container Tilemap").GetComponent<Tilemap>();
    containerTilePositions = TilemapHelper.GetTilePositions(containerTilemap);
    EventManager.LevelLoaded(this);
    Debug.Log("Level loaded");
    EventManager.GameLoopStarted += GameLoopIsRunning;
    EventManager.GameLoopFinished += GameLoopIsNotRunning;

  }

  private void GameLoopIsRunning() {
    gameLoopRunning = true;
  }

  private void GameLoopIsNotRunning() {
    gameLoopRunning = false;
  }
}
