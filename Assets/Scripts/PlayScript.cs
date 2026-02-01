using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayScript : MonoBehaviour, IPointerEnterHandler
{
    public AudioClip[] hoverbtnSFX;
    public AudioClip[] startbtnSFX;

    public GameObject BG;
    public GameObject StartBtn;
    public GameObject QuitBtn;
    public GameObject StartTextBG;
    public GameObject ControlsBG;

    private enum MenuState { WaitingToStart, ShowStartText, ShowControls }
    private MenuState currentState = MenuState.WaitingToStart;

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
                SceneManager.LoadScene("Game");
                SoundManager.Instance.TransitionMusicClip(MusicType.Game, 0.5f, 0.2f);
                break;
        }
    }

    // -------------------- Button Functions --------------------
    public void StartGame()
    {
        if (currentState != MenuState.WaitingToStart) return;

        currentState = MenuState.ShowStartText;

        SoundManager.Instance.PlayRandomSFXClip(startbtnSFX, transform, true, 1f);

        StartTextBG.SetActive(true);
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
