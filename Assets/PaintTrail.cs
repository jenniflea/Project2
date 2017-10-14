using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintTrail : MonoBehaviour {

    internal int CurrentColor = 0;

    public void ChangeColor() {
        if (CurrentColor >= 3)
            CurrentColor = 0;
        else
            CurrentColor += 1;
    }


}
