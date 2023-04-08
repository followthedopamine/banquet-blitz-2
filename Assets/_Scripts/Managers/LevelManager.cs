using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour {
  public Tilemap levelTilemap;
  public Tilemap backgroundTilemap;

  private void Start() {
    levelTilemap = GameObject.Find("Level Tilemap").GetComponent<Tilemap>();
    backgroundTilemap = GameObject.Find("Container Tilemap").GetComponent<Tilemap>();
    EventManager.LevelLoaded(this);
  }
}
