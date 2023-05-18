using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayGoal : MonoBehaviour {
  private List<GameObject> goalObjects = new();

  private void OnEnable() {
    EventManager.GoalUpdated += UpdateGoal;
    EventManager.LevelLoaded += CreateGoalObjects;
  }

  private void OnDisable() {
    EventManager.GoalUpdated -= UpdateGoal;
    EventManager.LevelLoaded -= CreateGoalObjects;
  }

  private void CreateGoalObjects(LevelManager levelManager) {
    GameObject goalContainer = GameObject.Find("Goal Container");
    GameObject initialGoalObject = GameObject.Find("Goal");
    goalObjects.Add(initialGoalObject);
    for (int i = 1; i < levelManager.goalRemaining.Count; i++) {
      GameObject goalObjectCopy = Instantiate(initialGoalObject);
      goalObjectCopy.transform.SetParent(goalContainer.transform);
      goalObjectCopy.transform.localScale = new Vector3(1, 1, 1); // Changing the parent makes the local scale smaller
      goalObjects.Add(goalObjectCopy);
    }
    UpdateGoal(levelManager);
    DisplayGoalIcons(levelManager);
  }

  private void UpdateGoal(LevelManager levelManager) {
    for (int i = 0; i < levelManager.goalRemaining.Count; i++) {
      int goal = levelManager.goalRemaining[i];
      // TODO: Could probably store goal text and goal icon objects so I don't have to use find as an optimization
      TMP_Text goalText = goalObjects[i].transform.Find("Goal Text").GetComponent<TMP_Text>();
      goalText.text = goal.ToString();
    }
  }

  private void DisplayGoalIcons(LevelManager levelManager) {
    for (int i = 0; i < levelManager.goalTiles.Count; i++) {
      Sprite goalSprite = levelManager.goalTiles[i].sprite;
      Image goalIcon = goalObjects[i].transform.Find("Goal Icon").GetComponent<Image>();
      goalIcon.sprite = goalSprite;
    }
  }
}
