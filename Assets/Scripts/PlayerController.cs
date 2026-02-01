using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager gameManager;
    public AudioClip[] maskSFX;
    public AudioClip[] controllerSFX;
    public AudioClip[] rubSFX;

    void Update()
    {
        CheckForInputs();
    }

    void CheckForInputs()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            gameManager.HoodToggle();
            SoundManager.Instance.PlayRandomSFXClip(maskSFX, transform, true, 1f);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            gameManager.GameToggle();
            SoundManager.Instance.PlayRandomSFXClip(controllerSFX, transform, true, 1f);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (!gameManager.hoodMode || gameManager.gameMode) return;
            gameManager.InteractWithKid("a", rubSFX);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (!gameManager.hoodMode || gameManager.gameMode) return;
            gameManager.InteractWithKid("d", rubSFX);
        }
    }
}
