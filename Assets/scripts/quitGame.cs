using UnityEngine;
using UnityEngine.InputSystem;

public class quitGame : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.escapeKey.isPressed)
        {
            Debug.Log("this works");
            Application.Quit();
        }
    }
}
