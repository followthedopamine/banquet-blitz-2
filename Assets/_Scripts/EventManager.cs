using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour {
  // <Level Tilemap, Background Tilemap>
  public static Action<LevelManager> LevelLoaded;
  // <Mouse position in world>
  public static Action TilemapClicked;
}
