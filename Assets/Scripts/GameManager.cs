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
    public float animSpeed = 0.5f;

    [Header("Kids")]
    public GameObject kidPrefab;
    public float minIdleTime = 2f;
    public float maxIdleTime = 5f;
    private float idleTimer;
    public float kidBoredTime = 5f;
    private bool kidPresent;
    private float kidPresentTimer;
    private float interacting;
    
    void Start()
    {
        hoodMode = false;
        gameMode = true;
        kidPresent = false;
    }

    void Update()
    {
        if (kidPresent) return;
        
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

    private void KidTimer()
    {
        if (kidPresent)
        {
            kidBoredTime -= Time.deltaTime;
            if (kidBoredTime <= 0f)
            {
                //Todo:
            }
        }
        
    }
    
}
