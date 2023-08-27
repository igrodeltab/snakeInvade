using TMPro;
using UnityEngine;

public class HighScoreDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _highScoreText;
    [SerializeField] private RecordHandler _recordHandler;

    private void Start()
    {
        UpdateHighScoreDisplay();
    }

    public void UpdateHighScoreDisplay()
    {
        int highScore = _recordHandler.GetHighScore();
        _highScoreText.text = $"High Score: {highScore}";
    }
}