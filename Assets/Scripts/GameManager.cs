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
    private const float hoodTime = 5f;
    private float hoodTimer;
    public SpriteRenderer oopsOverlay;
    public GameObject game;
    private const float gameOn = 0f;
    private const float gameOff = -8f;
    public float animSpeed = 0.5f;

    [Header("Kids")]
    public GameObject kidPrefab;
    private GameObject currentKid;
    public float minIdleTime = 2f;
    public float maxIdleTime = 5f;
    private const float kidWaitingPosX = 30f;
    private const float kidStartingPosX = 50f;
    private float idleTimer;
    public float kidBoredTime = 5f;
    private bool kidPresent;
    private float kidPresentTimer;
    private float interactingTimer;
    private int aPress;
    private int dPress;
    
    void Start()
    {
        hoodMode = false;
        gameMode = true;
        kidPresent = false;
        idleTimer = maxIdleTime;
        aPress = 0;
        dPress = 0;
    }

    void Update()
    {
        KidTimer();
        HoodTimer();
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
            if (interactingTimer >= 0)
            {
                interactingTimer -= Time.deltaTime;
                return;
            }
            kidPresentTimer -= Time.deltaTime;
            if (kidPresentTimer <= 0f)
            {
                //Todo: LOSE
                Debug.Log("LOSE by no interaction");
            }
            return;
        }
        kidPresentTimer = kidBoredTime;
        idleTimer -=  Time.deltaTime;
        if (idleTimer <= 0f)
        {
            kidPresent =  true;
            idleTimer = Random.Range(minIdleTime, maxIdleTime);
            currentKid = Instantiate(kidPrefab);
            LMotion.Create(kidStartingPosX, kidWaitingPosX, 1f)
                .WithEase(Ease.InOutElastic)
                .BindToPositionX(currentKid.transform);
        }
    }

    public void InteractWithKid(string button)
    {
        switch (button)
        {
            case "a":
            {
                aPress++;
                break;
            }
            case "d":
            {
                dPress++;
                break;
            }
        }

        interactingTimer = 0.5f;

        if (aPress >= 5 && dPress >= 5)
        {
            aPress = 0;
            dPress = 0;
            kidPresent = false;
            LMotion.Create(kidWaitingPosX, kidStartingPosX, 1f)
                .WithEase(Ease.InOutElastic)
                .WithOnComplete(() => DestroyImmediate(kidPrefab, true))
                .BindToPositionX(currentKid.transform);
        }
    }
    
    private void HoodTimer()
    {
        Color oopsOverlayColor = oopsOverlay.color;
        if (hoodMode)
        {
            hoodTimer = 0f;
            oopsOverlayColor.a = 0;
            oopsOverlay.color = oopsOverlayColor;
            return;
        }
        hoodTimer += Time.deltaTime;
        float hoodStartAlpha = 0f;
        float targetAlpha = 0.3f;
        float currentAlphaPercent = hoodTimer / hoodTime;
        float hoodAlpha = Mathf.Lerp(hoodStartAlpha, targetAlpha, currentAlphaPercent);
        oopsOverlayColor.a = hoodAlpha;
        oopsOverlay.color = oopsOverlayColor;
        if (hoodTimer >= hoodTime)
        {
            //TODO: Lose Scene
            Debug.Log("LOSE By Hood");
        }
    }
    
}
