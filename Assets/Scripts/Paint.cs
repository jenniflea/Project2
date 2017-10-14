using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paint : MonoBehaviour {

    private Animator animator;
    private static int CurrentColor; 

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        if (CurrentColor == -1) {
            CurrentColor = 0;
        }
        animator.SetInteger("CurrentIndex", CurrentColor);
        StartCoroutine("WaitToDestroy");
    }

    IEnumerator WaitToDestroy() {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    static public void ChangeColor() {
        if (CurrentColor >= 3)
            CurrentColor = 0;
        else
            CurrentColor += 1;
    }

}
