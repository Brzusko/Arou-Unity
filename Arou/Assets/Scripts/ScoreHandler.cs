using UnityEngine;
using UnityEngine.UI;
using EventArguments;
public class ScoreHandler : MonoBehaviour
{
    private Text _scoreText;
    private GameManager _manager;

    public string baseText = $"";
    void Start()
    {
        _scoreText = GetComponent<Text>();
        _manager = GameManager.Instance;
        _manager.OnScoreEvent += OnScoreEventHandler;
        _scoreText.text = $"{baseText}: {_manager.Score}".ToUpper();
    }

    private void OnScoreEventHandler(object sender, OnScoreArgs e)
    {
        _scoreText.text = $"{baseText}: {e.Score}".ToUpper();
        Debug.Log("test");
    }

}
