using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour
{
    public GameObject boomPrefab; // Boom prefab'�n� buraya atayaca��z
    public float delayBeforeGameOver = 1.0f; // Game over olmadan �nceki gecikme s�resi
    public Vector3 boomOffset = new Vector3(2, 1, 0); // Boom objesinin spawnlanma konumunu ayarlamak i�in kullan�lacak offset
    public GameObject childObject;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Update()
    {
        // E�er bomba objesinin y pozisyonu -2'den k���kse
        if (transform.position.y < -2f)
        {
            // Bomba objesini yok et
            Destroy(gameObject);
        }
    }

    private void OnMouseDown()
    {
        // Boom efektini olu�tur
        audioManager.PlaySFX(audioManager.bomb);
        Instantiate(boomPrefab, transform.position + boomOffset, Quaternion.identity);

        // Alt objelerin MeshRenderer'lar�n� devre d��� b�rak
        childObject.GetComponent<MeshRenderer>().enabled = false;
        
        // Game over durumunu gecikmeli olarak �al��t�r
        StartCoroutine(DelayedGameOver());
    }

    private IEnumerator DelayedGameOver()
    {
        
        // Belirtilen s�re kadar bekle
        yield return new WaitForSeconds(delayBeforeGameOver);

        // Game over ekran�n� g�ster
        GameOverManager.Instance.GameOver();
    }
}
