using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager instance;
    private int score;
    public TextMeshProUGUI scoreText; // Skorun gösterileceði TextMeshProUGUI bileþeni
    public bool isHardMode; // Oyunun zorluk seviyesini belirlemek için
    private RandomizeTrashBins randomizeTrashBins; // RandomizeTrashBins scriptinin referansý

    public static ScoreManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ScoreManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("ScoreManager");
                    instance = obj.AddComponent<ScoreManager>();
                }
            }
            return instance;
        }
    }

    private void Start()
    {
        // Ýlk baþta skoru sýfýr olarak ayarlayýn
        score = 0;
        UpdateScoreText();

        // RandomizeTrashBins scriptinin referansýný alýyoruz
        randomizeTrashBins = FindObjectOfType<RandomizeTrashBins>();
    }

    public void HandleTrashDrop(GameObject trashObject, string binTag)
    {
        string trashTag = trashObject.tag;
        bool isCorrectBin = (trashTag == "PaperTrash" && binTag == "PaperBin") ||
                            (trashTag == "GlassTrash" && binTag == "GlassBin") ||
                            (trashTag == "MetalTrash" && binTag == "MetalBin") ||
                            (trashTag == "PlasticTrash" && binTag == "PlasticBin") ||
                            (trashTag == "OrganicTrash" && binTag == "OrganicBin");

        if (isCorrectBin)
        {
            AddScore(1);
        }
        else
        {
            AddScore(-5);
        }

        Destroy(trashObject);

        // Her çöp atýldýðýnda çöp kutularýnýn yerlerini rastgele deðiþtiriyoruz (zor modda)
        if (isHardMode && randomizeTrashBins != null)
        {
            randomizeTrashBins.RandomizePositions();
        }
    }

    private void AddScore(int value)
    {
        score += value;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    public int GetScore()
    {
        return score;
    }
}
