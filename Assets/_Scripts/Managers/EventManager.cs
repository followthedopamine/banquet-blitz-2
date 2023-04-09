using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour {
  public static Action<LevelManager> LevelLoaded;
  public static Action TilemapMouseDown;
  public static Action TilemapMouseUp;
  public static Action TilesSwitched;
  public static Action<List<Match>> MatchesFound;
  public static Action<List<Vector3Int>> DestroyedTiles;
}
