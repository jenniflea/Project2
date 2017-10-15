using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paint : MonoBehaviour {

    public PaintTrail paintTrail;

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        if (paintTrail.CurrentColor == -1) {
            paintTrail.CurrentColor = 0;
        }

        animator.SetInteger("CurrentIndex", paintTrail.CurrentColor);
        StartCoroutine("WaitToDestroy");
    }

    IEnumerator WaitToDestroy() {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

}
