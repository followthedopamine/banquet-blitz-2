using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour {
  public Tilemap levelTilemap;
  public Tilemap containerTilemap;
  public List<GameTile> spawnableTiles;
  public List<Vector3Int> containerTilePositions;


  private void Start() {
    levelTilemap = GameObject.Find("Level Tilemap").GetComponent<Tilemap>();
    containerTilemap = GameObject.Find("Container Tilemap").GetComponent<Tilemap>();
    containerTilePositions = TilemapHelper.GetTilePositions(containerTilemap);
    EventManager.LevelLoaded(this);
  }
}
