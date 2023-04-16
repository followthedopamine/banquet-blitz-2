using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectButtons : MonoBehaviour {
  public GameObject world1;

  private void Start() {
    for (int i = 0; i < world1.transform.childCount; i++) {
      world1.transform.GetChild(i);
    }
  }

  public void World1Level1Button() {
    Debug.Log("Test");
  }
}
