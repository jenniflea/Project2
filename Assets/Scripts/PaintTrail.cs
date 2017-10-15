using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintTrail : MonoBehaviour {

    internal int CurrentColor;
    internal int Counter;

    private void Start() {
        CurrentColor = 0;
        Counter = -32000;
    }

    public int Layer() {
        int layer = Counter;
        Counter++;
        return layer;
    }

    public void ChangeColor() {
        if (CurrentColor >= 3)
            CurrentColor = 0;
        else
            CurrentColor += 1;
    }


}
