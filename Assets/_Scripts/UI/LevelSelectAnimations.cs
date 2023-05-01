using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectAnimations : MonoBehaviour {
  private const float CHARACTER_MOVE_SPEED = 5.0f;

  private void Start() {
    EventManager.LevelButtonClicked += MoveCharacterToLevelIndicator;
  }

  private void MoveCharacterToLevelIndicator(int levelToMoveTo) {

  }
}
