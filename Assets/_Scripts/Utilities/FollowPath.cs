using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour {

  public static IEnumerator MoveObjectAlongPath(GameObject objectToMove, GameObject[] path, float moveSpeed) {
    foreach (GameObject point in path) {
      while (objectToMove.transform.position != point.transform.position) {
        objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, point.transform.position, moveSpeed * Time.deltaTime);
        yield return new WaitForEndOfFrame();
      }
    }
  }
}
