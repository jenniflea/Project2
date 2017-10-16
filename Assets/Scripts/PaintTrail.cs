using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintTrail : MonoBehaviour {

    public GameObject PaintPrefab;

    private int Counter;
    private int CurrentColor;
    private int ObjectPoolIterator = 0;
    private GameObject[] ObjectPool = new GameObject[100];

    public Direction direction;
    public enum Direction { Forward, Down, Right };

    private void Start() {
        Counter = -32000;
        CurrentColor = 0;
        ObjectPoolIterator = 0;

        for (int i = 0; i < ObjectPool.Length; i++) {
            ObjectPool[i] = Instantiate(PaintPrefab, new Vector3(-1000, -1000, -1000), Quaternion.LookRotation(Vector3.up));
        }
	
    }

    private void Layer() {
        ObjectPool[ObjectPoolIterator].GetComponent<MeshRenderer>().sortingOrder = Counter;
        Counter++;
    }

    private void UpdateOPIterator() {
        ObjectPoolIterator++;

        if (ObjectPoolIterator >= ObjectPool.Length)
            ObjectPoolIterator = 0;
    }

    public void ChangeColor() {
        if (CurrentColor >= 3)
            CurrentColor = 0;
        else
            CurrentColor += 1;
    }

    public void AddToTrail() {
        Vector3 dir = Vector3.zero;
        Paint paint = ObjectPool[ObjectPoolIterator].GetComponent<Paint>();

        switch (direction) {
            case Direction.Down: dir = transform.up * -1; break;
            case Direction.Forward: dir = transform.forward; break;
            case Direction.Right: dir = transform.right * -1; break;
        }

        Layer();
        Vector3 position = gameObject.transform.position + dir * (gameObject.transform.localScale.y / 2 - 0.01f);
        Quaternion rotation = Quaternion.LookRotation(transform.forward, dir);
        paint.UpdateTransform(position, rotation);

        paint.ChangeColor(CurrentColor);
        UpdateOPIterator();
    }
}