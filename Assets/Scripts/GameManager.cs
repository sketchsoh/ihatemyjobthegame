using System.Collections;
using LitMotion;
using LitMotion.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private bool isInteracting;
    private int aPress;
    private int dPress;
    
    public GameObject leftHand;
    public GameObject rightHand;
    
    public AudioClip[] kidEnterSFX;
    public AudioClip[] kidExitSFX;
    public SpriteRenderer Fade;
    
    void Start()
    {
        hoodMode = true;
        gameMode = true;
        kidPresent = false;
        isInteracting = false;
        idleTimer = maxIdleTime;
        aPress = 0;
        dPress = 0;
        LMotion.Create(1f, 0f, 0.5f)
            .BindToColorA(Fade);
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
        if (isInteracting) return;
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
                LMotion.Create(0f, 1f, 0.5f)
                    .WithOnComplete(() => SceneManager.LoadScene("Lose1"))
                    .BindToColorA(Fade);
                Debug.Log("LOSE by no interaction");
                SoundManager.Instance.TransitionMusicClip(MusicType.Lose, 0.5f);
                kidPresentTimer = 1000f;
            }
            return;
        }
        kidPresentTimer = kidBoredTime;
        idleTimer -=  Time.deltaTime;
        if (idleTimer <= 0f)
        {
            kidPresent =  true;
            SoundManager.Instance.PlayRandomSFXClip(kidEnterSFX, transform, true, 0.5f);
            idleTimer = Random.Range(minIdleTime, maxIdleTime);
            currentKid = Instantiate(kidPrefab);
            LMotion.Create(kidStartingPosX, kidWaitingPosX, 1f)
                .WithEase(Ease.InOutElastic)
                .BindToPositionX(currentKid.transform);
        }
    }

    public void InteractWithKid(string button, AudioClip[] interactSound)
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

        interactingTimer = 0.8f;
        if (!isInteracting)
        {
            SoundManager.Instance.PlayRandomSFXClip(interactSound, transform);
            isInteracting = true;
            LSequence.Create()
                .Append(LMotion.Create(15f, 25f, 0.25f)
                    .WithImmediateBind(false)
                    .WithEase(Ease.InOutBack)
                   .BindToLocalPositionX(leftHand.transform))
                .Append(LMotion.Create(25f, 15f, 0.25f)
                    .WithImmediateBind(false)
                    .WithEase(Ease.InOutBack)
                    .WithOnComplete(() => isInteracting = false)
                    .BindToLocalPositionX(leftHand.transform))
                .Run();
            LSequence.Create()
                .AppendInterval(0.05f)
                .Append(LMotion.Create(45f, 35f, 0.25f)
                    .WithImmediateBind(false)
                    .WithEase(Ease.InOutBack)
                    .BindToLocalPositionX(rightHand.transform))
                .Append(LMotion.Create(35f, 45f, 0.25f)
                    .WithImmediateBind(false)
                    .WithEase(Ease.InOutBack)
                    .BindToLocalPositionX(rightHand.transform))
                .Run();
        }
        if (aPress >= 10 && dPress >= 10)
        {
            aPress = 0;
            dPress = 0;
            kidPresent = false;
            SoundManager.Instance.PlayRandomSFXClip(kidExitSFX, transform, true, 0.5f);
            LMotion.Create(kidWaitingPosX, kidStartingPosX, 1f)
                .WithEase(Ease.InOutElastic)
                .WithOnComplete(() => Destroy(currentKid))
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
        if (hoodTimer <= 2f) return;
        float hoodStartAlpha = 0f;
        float targetAlpha = 0.7f;
        float currentAlphaPercent = hoodTimer - 2 / hoodTime - 2;
        float hoodAlpha = Mathf.Lerp(hoodStartAlpha, targetAlpha, currentAlphaPercent);
        oopsOverlayColor.a = hoodAlpha;
        oopsOverlay.color = oopsOverlayColor;
        if (hoodTimer >= hoodTime)
        {
            LMotion.Create(0f, 1f, 0.5f)
                .WithOnComplete(() => SceneManager.LoadScene("Lose2"))
                .BindToColorA(Fade);
            Debug.Log("LOSE By Hood");
            hoodTimer = -999f;
            SoundManager.Instance.TransitionMusicClip(MusicType.Lose, 0.5f);
        }
    }
}
