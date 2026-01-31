using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool hoodMode;
    public bool gameMode;
    private Coroutine hoodToggleCoroutine;
    private Coroutine gameToggleCoroutine;
    void Start()
    {
        hoodMode = false;
        gameMode = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HoodToggle()
    {
        hoodMode = !hoodMode;
    }
}
