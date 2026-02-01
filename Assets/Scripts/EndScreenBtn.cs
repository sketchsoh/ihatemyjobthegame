using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class EndScreenBtn : MonoBehaviour, IPointerEnterHandler
{
    public AudioClip[] hoverbtnSFX;
    public AudioClip[] startbtnSFX;
    private IPointerEnterHandler _pointerEnterHandlerImplementation;
    
    public void MainMenu()
    {
        SoundManager.Instance.TransitionMusicClip(MusicType.MainMenu, 0.5f);
        SceneManager.LoadScene("Menu");
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
