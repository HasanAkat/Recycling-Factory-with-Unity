using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance { get; private set; }

    public GameObject gameOverCanvas;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;
    public GameObject ScoreCanvas;
    public bool isHardMode; // Oyunun zorluk seviyesini belirlemek için

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        gameOverCanvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PaperTrash") || other.CompareTag("GlassTrash") || other.CompareTag("PlasticTrash") || other.CompareTag("MetalTrash") || other.CompareTag("OrganicTrash"))
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0f; // Oyunu duraklat
        ScoreCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);

        int score = ScoreManager.Instance.GetScore();
        scoreText.text = "Score: " + score;

        string bestScoreKey = isHardMode ? "BestScoreHard" : "BestScore";
        int bestScore = PlayerPrefs.GetInt(bestScoreKey, 0);
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt(bestScoreKey, bestScore);
        }
        bestScoreText.text = "Best Score: " + bestScore;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Oyun hýzýný normal hýzýna döndür
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Mevcut sahneyi yeniden yükler
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // Oyun hýzýný normal hýzýna döndür
        SceneManager.LoadScene("MainMenu"); // Ana menü sahnesini yükler (sahne ismini kendinize göre düzenleyin)
    }
}
