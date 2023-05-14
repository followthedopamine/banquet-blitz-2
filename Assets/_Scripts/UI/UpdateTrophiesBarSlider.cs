using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateTrophiesBarSlider : MonoBehaviour {

  private Slider slider;

  private void Start() {
    slider = GameObject.Find("Trophy Bar Slider").GetComponent<Slider>();
    EventManager.ScoreUpdated += UpdateSlider;
  }

  private void UpdateSlider() {
    LevelManager levelManager = GameManager.Instance.levelManager;
    int goldTrophyScore = levelManager.trophyScores[^1];
    slider.value = 1f / goldTrophyScore * levelManager.score;
    Debug.Log("Slider updated " + 1f / goldTrophyScore * levelManager.score);
  }
}
