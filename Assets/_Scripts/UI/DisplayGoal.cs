using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayGoal : MonoBehaviour {
  private List<GameObject> goalObjects = new();

  private void Start() {
    EventManager.GoalUpdated += UpdateGoal;
    EventManager.LevelLoaded += CreateGoalObjects;
  }

  private void CreateGoalObjects(LevelManager levelManager) {
    GameObject goalContainer = GameObject.Find("Goal Container");
    GameObject initialGoalObject = GameObject.Find("Goal");
    goalObjects.Add(initialGoalObject);
    for (int i = 1; i < levelManager.goalRemaining.Count; i++) {
      GameObject goalObjectCopy = Instantiate(initialGoalObject);
      goalObjectCopy.transform.SetParent(goalContainer.transform);
      goalObjects.Add(goalObjectCopy);
    }
    UpdateGoal(levelManager);
  }

  private void UpdateGoal(LevelManager levelManager) {
    for (int i = 0; i < levelManager.goalRemaining.Count; i++) {
      int goal = levelManager.goalRemaining[i];
      TMP_Text goalText = goalObjects[i].transform.Find("Goal Text").GetComponent<TMP_Text>();
      goalText.text = goal.ToString();
    }
  }

  private void DisplayGoalText() {

  }

  private void DisplayGoalIcons() {

  }
}
