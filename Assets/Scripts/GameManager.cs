using System.Collections;
using LitMotion;
using LitMotion.Extensions;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool hoodMode;
    public bool gameMode;
    public GameObject hood;
    private const float hoodOff = 16.0f;
    private const float hoodOn = 0f;
    public GameObject game;
    private const float gameOn = 0f;
    private const float gameOff = -8f;
    private Coroutine hoodToggleCoroutine;
    private Coroutine gameToggleCoroutine;
    public float animSpeed = 0.5f;
    void Start()
    {
        hoodMode = false;
        gameMode = true;
    }
    public void HoodToggle()
    {
        hoodMode = !hoodMode;
        LMotion.Create(hood.transform.position.y, hoodMode? hoodOn : hoodOff, animSpeed)
            .WithEase(Ease.InOutElastic)
            .BindToLocalPositionY(hood.transform);
    }

    public void GameToggle()
    {
        gameMode = !gameMode;
        LMotion.Create(game.transform.position.y, gameMode?gameOn:gameOff, animSpeed)
            .WithEase(Ease.InOutExpo)
            .BindToLocalPositionY(game.transform);
    }
}
