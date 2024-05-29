using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour
{
    public GameObject boomPrefab; // Boom prefab'ýný buraya atayacaðýz
    public float delayBeforeGameOver = 1.0f; // Game over olmadan önceki gecikme süresi
    public Vector3 boomOffset = new Vector3(2, 1, 0); // Boom objesinin spawnlanma konumunu ayarlamak için kullanýlacak offset
    public GameObject childObject;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Update()
    {
        // Eðer bomba objesinin y pozisyonu -2'den küçükse
        if (transform.position.y < -2f)
        {
            // Bomba objesini yok et
            Destroy(gameObject);
        }
    }

    private void OnMouseDown()
    {
        // Boom efektini oluþtur
        audioManager.PlaySFX(audioManager.bomb);
        Instantiate(boomPrefab, transform.position + boomOffset, Quaternion.identity);

        // Alt objelerin MeshRenderer'larýný devre dýþý býrak
        childObject.GetComponent<MeshRenderer>().enabled = false;
        
        // Game over durumunu gecikmeli olarak çalýþtýr
        StartCoroutine(DelayedGameOver());
    }

    private IEnumerator DelayedGameOver()
    {
        
        // Belirtilen süre kadar bekle
        yield return new WaitForSeconds(delayBeforeGameOver);

        // Game over ekranýný göster
        GameOverManager.Instance.GameOver();
    }
}
