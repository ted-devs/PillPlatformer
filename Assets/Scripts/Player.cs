using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    [SerializeField] private Transform groundCheckTransform = null;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float moveSpeed;
    private bool jumpKeyWasPressed;
    private bool superJumpKeyWasPressed;
    // private int superJumpsRemaining;
    private float horizontalInput;
    private Rigidbody rigidBodyComponent;

    // Start is called before the first frame update
    void Start() {
        rigidBodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        // Check if space key is pressed down 
        if (Input.GetKeyDown(KeyCode.Space)) {
            jumpKeyWasPressed = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && SuperJumpScript.superJumpsRemaining > 0) {
            superJumpKeyWasPressed = true;
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            ResetLevel();
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }

        if (OrbsCollected.orbsCollected == 6) {
            WinScript.winState = true;
        }

        horizontalInput = Input.GetAxis("Horizontal");
    }

    // FixedUpdate is called once every physics update
    private void FixedUpdate() {
        Move();
        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f).Length == 1) {
            jumpKeyWasPressed = false;
            superJumpKeyWasPressed = false;
            return;
        }
        if (jumpKeyWasPressed) {
            Jump();
        }
        if (superJumpKeyWasPressed) {
            SuperJump();
        }
    }

    private void Jump() {        
        rigidBodyComponent.AddForce(Vector3.up * jumpHeight, ForceMode.VelocityChange);
        jumpKeyWasPressed = false;
    }

    private void SuperJump() {
        float jumpPower = jumpHeight;
        if (SuperJumpScript.superJumpsRemaining > 0) {
            jumpPower *= 1.5f;
            SuperJumpScript.superJumpsRemaining--;
        } else {
            return;
        }
        Rotation.addRotation = true;
        rigidBodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
        superJumpKeyWasPressed = false;
    }

    private void Move() {
        rigidBodyComponent.velocity = new Vector3(horizontalInput * moveSpeed, rigidBodyComponent.velocity.y, 0);
    }

    private void ResetLevel() {
        OrbsCollected.orbsCollected = 0;
        SuperJumpScript.superJumpsRemaining = 0;
        WinScript.winState = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == 6) {
            Destroy(other.gameObject);
            SuperJumpScript.superJumpsRemaining++;
            OrbsCollected.orbsCollected++;
        }
        if (other.name == "ResetLevel") {
            ResetLevel();
        }
    }
}
