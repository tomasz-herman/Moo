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
    [SerializeField] Image flashImage;

    [SerializeField] float AnimationTime = 2;
    [SerializeField] float FlashTime = 0.5f;

    [SerializeField] string WinText = "You survived!";
    [SerializeField] string LoseText = "You died!";

    private bool Win;
    private float elapsedTime;
    public void Open(bool win)
    {
        Win = win;
        elapsedTime = 0;
        userInterface.TryToggleWindow(this);
        userInterface.hud.gameObject.SetActive(false);
        if (FlashTime > AnimationTime)
            FlashTime = AnimationTime;

        Color baseFlashColor = win ? winColor : loseColor;
        baseFlashColor /= 2;
        baseFlashColor.a = 0;
        flashImage.color = baseFlashColor;
    }

    void Start()
    {
        panel.gameObject.SetActive(false);
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
        if(elapsedTime < AnimationTime)
        {
            elapsedTime += Time.unscaledDeltaTime;
            if (elapsedTime >= AnimationTime)
            {
                elapsedTime = AnimationTime;
                panel.gameObject.SetActive(true);
            }
            Time.timeScale = 1 - elapsedTime / AnimationTime;

            var flashColor = flashImage.color;
            flashColor.a = Mathf.Clamp01(1 - elapsedTime / FlashTime);
            flashImage.color = flashColor;
        }
    }
    public void EndGame()
    {
        gameWorld.ReturnToMenu(Win);
    }
}
