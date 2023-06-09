using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateTimerText : MonoBehaviour {

  private TMP_Text timerText;

  private void OnEnable() {
    timerText = GameObject.Find("Timer Text").GetComponent<TMP_Text>();
    EventManager.OneSecondTick += UpdateTime;
    EventManager.LevelLoaded += SetInitialTime;
  }

  private void OnDisable() {
    EventManager.OneSecondTick -= UpdateTime;
    EventManager.LevelLoaded -= SetInitialTime;
  }

  private void UpdateTime(float timeToDisplay) {
    // timeToDisplay += 1;
    float minutes = Mathf.FloorToInt(timeToDisplay / 60);
    float seconds = Mathf.FloorToInt(timeToDisplay % 60);
    timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
  }

  private void SetInitialTime(LevelManager levelManager) {
    UpdateTime(levelManager.timeRemaining);
  }

}
