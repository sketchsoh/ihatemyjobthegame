using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToEnd : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("EndScreen");
        }
    }
}
