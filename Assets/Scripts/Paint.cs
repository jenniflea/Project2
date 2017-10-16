using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paint : MonoBehaviour {

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public void ChangeColor(int Color) {
        animator.SetInteger("CurrentIndex", Color);
    }

    public void UpdateTransform(Vector3 position, Quaternion rotation) {
        transform.position = position;
        transform.rotation = rotation;
    }
}
