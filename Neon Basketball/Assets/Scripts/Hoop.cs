using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoop : MonoBehaviour {

    public float amp;
    public float freq;

    private Vector3 initial;

    void Start() {
        initial = transform.position;
    }

    void Update() {
        transform.position = new Vector3(initial.x, Mathf.Sin(Time.time * freq) * amp * initial.y, 0);
    }

}
