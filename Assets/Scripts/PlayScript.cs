using LitMotion;
using LitMotion.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayScript : MonoBehaviour, IPointerEnterHandler
{
    public AudioClip[] hoverbtnSFX;
    public AudioClip[] startbtnSFX;

    public GameObject BG;
    public GameObject StartBtn;
    public GameObject QuitBtn;
    public GameObject StartTextBG;
    public GameObject ControlsBG;
    public GameObject Fade;

    private enum MenuState { WaitingToStart, ShowStartText, ShowControls }
    private MenuState currentState = MenuState.WaitingToStart;

    void Start()
    {
        LMotion.Create(1f, 0f, 0.25f)
            .WithOnComplete((() => Fade.SetActive(false)))
            .BindToColorA(Fade.GetComponent<Image>());
    }

    // -------------------- Unity Events --------------------
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleClick();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Hover();
    }

    // -------------------- Menu Logic --------------------
    private void HandleClick()
    {
        switch (currentState)
        {
            case MenuState.WaitingToStart:
                // Nothing happens on background click; start is handled by button
                break;

            case MenuState.ShowStartText:
                // Move to Controls screen
                ControlsBG.SetActive(true);
                currentState = MenuState.ShowControls;
                break;

            case MenuState.ShowControls:
                // Load the game scene
                Fade.SetActive(true);
                LMotion.Create(0f, 1f, 0.25f)
                    .WithOnComplete((() => SceneManager.LoadScene("Game")))
                    .BindToColorA(Fade.GetComponent<Image>());
                SoundManager.Instance.TransitionMusicClip(MusicType.Game, 0.5f, 0.2f);
                break;
        }
    }

    // -------------------- Button Functions --------------------
    public void StartGame()
    {
        if (currentState != MenuState.WaitingToStart) return;

        Fade.SetActive(true);
        LMotion.Create(0f, 1f, 0.25f)
            .WithOnComplete(SettleFade)
            .BindToColorA(Fade.GetComponent<Image>());
    }

    private void SettleFade()
    {
        Fade.SetActive(false);
        StartTextBG.SetActive(true);
        SoundManager.Instance.PlayRandomSFXClip(startbtnSFX, transform, true, 1f);
        currentState = MenuState.ShowStartText;
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    private void Hover()
    {
        SoundManager.Instance.PlayRandomSFXClip(hoverbtnSFX, transform, true, 1f);
    }
}
