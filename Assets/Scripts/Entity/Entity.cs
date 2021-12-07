using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    private GameWorld gameWorld = null;
    public GameWorld GameWorld
    { 
        get
        {
            if (gameWorld == null)
                gameWorld = FindObjectOfType<GameWorld>();
            return gameWorld;
        }
        private set { gameWorld = value; }
    }
}
