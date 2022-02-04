using UnityEngine;

public class Entity : MonoBehaviour
{
    private GameWorld _gameWorld = null;
    public GameWorld GameWorld
    { 
        get
        {
            if (_gameWorld == null)
                _gameWorld = FindObjectOfType<GameWorld>();
            return _gameWorld;
        }
        private set => _gameWorld = value;
    }
}
