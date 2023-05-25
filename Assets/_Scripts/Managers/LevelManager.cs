using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour {
  [HideInInspector] public Tilemap levelTilemap;
  [HideInInspector] public Tilemap containerTilemap;
  [HideInInspector] public Tilemap overlayTilemap;
  [HideInInspector] public List<Vector3Int> containerTilePositions;
  /*[HideInInspector]*/
  public int score;

  public int movesRemaining = -1;
  public float timeRemaining = -1;
  public List<GameTile> goalTiles;
  public List<int> goalRemaining;
  public List<GameTile> spawnableTiles;
  public List<int> trophyScores;
  public bool levelIsWon = false;
  public bool levelIsLost = false;
  public bool gameLoopRunning = false;
  public int currentTrophy = -1;
  public HashSet<int> tilesIdsThatCantBeSwitched;



  private void OnEnable() {
    levelTilemap = GameObject.Find("Level Tilemap").GetComponent<Tilemap>();
    containerTilemap = GameObject.Find("Container Tilemap").GetComponent<Tilemap>();
    containerTilePositions = TilemapHelper.GetTilePositions(containerTilemap);
    overlayTilemap = GameObject.Find("Overlay Tilemap").GetComponent<Tilemap>();
    EventManager.LevelLoaded(this);
    Debug.Log("Level loaded");
  }

  public void AddTileToListOfTilesThatCantBeSwitched(int tileId) {
    tilesIdsThatCantBeSwitched.Add(tileId);
  }
}
