using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    private Rigidbody rigidBodyComponent;
    static public bool addRotation;

    // Start is called before the first frame update
    void Start()
    {
        rigidBodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (addRotation || Input.GetKeyDown(KeyCode.LeftAlt)) {
            rigidBodyComponent.AddTorque(Vector3.up * 200);
            addRotation = false;
        }
    }
}
