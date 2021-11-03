using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScript : MonoBehaviour
{
    public static bool winState = false;
    Text win;
    // Start is called before the first frame update
    void Start()
    {
        win = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (winState) {
            win.text = "YOU WIN!";
        } else {
            win.text = "";
        }
    }
}
