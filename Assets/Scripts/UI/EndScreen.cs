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
    [SerializeField] RectTransform panel;

    [SerializeField] float AnimationTime = 2;

    [SerializeField] string WinText = "You survived!";
    [SerializeField] string LoseText = "You died!";

    private bool Win;
    public void Open(bool win)
    {
        Win = win;
        userInterface.TryToggleWindow(this);
    }
    void Start()
    {
        panel.transform.localPosition = new Vector3(0, -panel.rect.height, 0);
        if (Win)
        {
            resultText.text = WinText;
            resultText.color = winColor;
        }
        else
        {
            resultText.text = LoseText;
            resultText.color = loseColor;
        }
    }
    void Update()
    {
        if (panel.transform.localPosition.y <= 0)
        {
            var d = Time.fixedDeltaTime / AnimationTime;
            panel.transform.localPosition = panel.transform.localPosition + Vector3.up * d * panel.rect.height;
            if (panel.transform.localPosition.y >= 0)
            {
                panel.transform.localPosition = Vector3.zero;
            }
        }
    }
    public void EndGame()
    {
        gameWorld.ReturnToMenu(Win);
    }
}