using UnityEngine;
using TMPro;

public class FinalScoreText : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    void Start()
    {
        if (scoreText != null && ScoreManager.Instance != null)
        {
            scoreText.text = $"FINAL SCORE: {ScoreManager.Instance.CurrentScore}";
        }
    }
}