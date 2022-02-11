using System;
using Assets.Scripts.SoundManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWorld : MonoBehaviour
{
    public UserInterface userInterface;
    public Timer timer;
    public AudioManager audioManager;
    public BackgroundMusicManager FightMusicManager;
    public BackgroundMusicManager IdleMusicManager;
    public Player player;
    public ChambersControler chambersControler;

    void Start()
    {
        timer.SetTicking(true);
        IdleMusicManager?.Play();

        audioManager = AudioManager.Instance;
        player = FindObjectOfType<Player>();
        chambersControler = FindObjectOfType<ChambersControler>();
    }

    public bool IsPaused() { return Time.timeScale == 0; }

    public void EndGame(bool win)
    {
        float score = player.GetComponent<ScoreSystem>().Score;
        double timeMs = timer.GetElapsedTime().TotalMilliseconds;
        if(win)
        {
            //TODO: we need a nicer API for getting these values - preferably put it in GameplayConfig scriptable object - best place for it
            var spawnScript = chambersControler.GetComponent<SpawnScript>();
            int normalChambersBeforeBoss = (int)typeof(SpawnScript).GetField("NumbersOfChambersBeforeBoss").GetValue(spawnScript);
            int optionalChambersBeforeBoss = (int)typeof(SpawnScript).GetField("NumberOfOptionalChambersBeforeBoss").GetValue(spawnScript);
            int bossChambers = (int)typeof(SpawnScript).GetField("NumberOfBossChambers").GetValue(spawnScript);

            float expectedTimeToClearAllChambers = 0f;
            float expectedTimeToClearObligatoryChambers = 0f;

            
            //foreach(var chamber in spawnScript.GetChambers())
            //{

            //}
        }


        EndGameData data = new EndGameData(Mathf.CeilToInt(score), win, (int)timeMs, DateTimeOffset.Now.ToUnixTimeSeconds());
        EndGameView.SetEndGameData(data);
        MainMenu.ScheduleShowEndgameScreen();
        SceneManager.LoadScene(Scenes.MainMenu);
    }
}
