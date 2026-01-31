using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForInputs();
    }

    void CheckForInputs()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            //TODO: call flip hood in Game Manager
            gameManager.HoodToggle();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            //TODO: call bring down game in Game Manager
            gameManager.GameToggle();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            //TODO: Do Quicktime Event
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            //TODO: Do Quicktime Event
        }
    }
}
