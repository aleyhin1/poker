using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class GameManager : MonoSingleton<GameManager>
{
    PlayerSequenceHandler playerSequenceHandler;
    [SerializeField] private List<PlayerController> _players = new List<PlayerController>();

    private void Awake()
    {
        playerSequenceHandler = new PlayerSequenceHandler(_players);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerSequenceHandler.NextPlayerAction?.Invoke();
        }   
    }
    private void OnDisable()
    {
        playerSequenceHandler.OnDisable();
    }
}