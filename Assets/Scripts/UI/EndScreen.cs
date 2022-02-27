using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : GuiWindow
{
    [SerializeField] UserInterface userInterface;
    [SerializeField] GameWorld gameWorld;

    [SerializeField] Color winColor, loseColor;
    [SerializeField] Text resultText;

    private bool Win;

    public void Open(bool win)
    {
        Win = win;
        userInterface.TryOpenWindow(this);
    }
    void Start()
    {
        if (Win)
        {
            resultText.text = "You survived!";
            resultText.color = winColor;
        }
        else
        {
            resultText.text = "You died!";
            resultText.color = loseColor;
        }
    }
    public void EndGame()
    {
        gameWorld.ReturnToMenu(Win);
    }
}
