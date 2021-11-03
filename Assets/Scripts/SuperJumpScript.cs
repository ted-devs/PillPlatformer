using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuperJumpScript : MonoBehaviour
{
    public static int superJumpsRemaining = 0;
    Text jumps;
    // Start is called before the first frame update
    void Start()
    {
        jumps = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        jumps.text = "Super Jumps Remaining: " + superJumpsRemaining;
    }
}
