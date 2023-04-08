using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour {
  // <Level Tilemap, Background Tilemap>
  public static Action<LevelManager> LevelLoaded;
  public static Action TilemapMouseDown;
  public static Action TilemapMouseUp;
  public static Action TilesSwitched;
  public static Action<List<Match>> MatchesFound;
}
