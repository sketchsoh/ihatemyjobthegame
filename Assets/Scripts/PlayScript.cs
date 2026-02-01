using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayScript : MonoBehaviour , IPointerEnterHandler
{
    public AudioClip[] hoverbtnSFX;
    public AudioClip[] startbtnSFX;
    private IPointerEnterHandler _pointerEnterHandlerImplementation;
    
    public GameObject BG;
    public GameObject StartBtn;
    public GameObject QuitBtn;
    public GameObject StartTextBG;
    
    private float confirmTime;
    
    private bool hasConfirmedStart = false;

    void Update()
    {
        if (StartTextBG.activeInHierarchy && Time.time - confirmTime > 0.2f &&
            Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("Game");
            Debug.Log("Scene loaded");
        }
    }
    
    public void StartGame()
    {
        if (!hasConfirmedStart)
        {
            hasConfirmedStart = true;
            confirmTime = Time.time;

            SoundManager.Instance.PlayRandomSFXClip(startbtnSFX, transform, true, 1f);
            StartTextBG.SetActive(true);

            return;
        }

        // Second click
       // SceneManager.LoadScene("Game");
    }
    
    public void QuitGame()
    {
        Debug.Log("Quit Game");

        Application.Quit();
    }
    void Hover()
    {
        SoundManager.Instance.PlayRandomSFXClip(hoverbtnSFX, transform, true, 1f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Hover();
    }
}