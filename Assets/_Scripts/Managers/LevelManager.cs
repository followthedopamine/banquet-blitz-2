using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour {
  [HideInInspector] public Tilemap levelTilemap;
  [HideInInspector] public Tilemap containerTilemap;
  [HideInInspector] public List<Vector3Int> containerTilePositions;
  [HideInInspector] public int score;

  public List<GameTile> spawnableTiles;
  public int movesRemaining;


  private void Start() {
    levelTilemap = GameObject.Find("Level Tilemap").GetComponent<Tilemap>();
    containerTilemap = GameObject.Find("Container Tilemap").GetComponent<Tilemap>();
    containerTilePositions = TilemapHelper.GetTilePositions(containerTilemap);
    EventManager.LevelLoaded(this);
  }
}
