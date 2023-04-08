using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour {
  public Tilemap levelTilemap;
  public Tilemap containerTilemap;

  private void Start() {
    levelTilemap = GameObject.Find("Level Tilemap").GetComponent<Tilemap>();
    containerTilemap = GameObject.Find("Container Tilemap").GetComponent<Tilemap>();
    EventManager.LevelLoaded(this);
  }
}
