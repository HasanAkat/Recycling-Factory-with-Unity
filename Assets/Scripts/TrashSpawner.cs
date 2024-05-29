using System.Collections;
using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    public GameObject[] trashPrefabs; // Çöp prefablarýný buraya atayacaðýz (kaðýt, cam, plastik)
    public GameObject bombPrefab; // Bomba prefabý
    public float spawnInterval = 1f; // Çöp oluþturma aralýðý (saniye)
    public Transform spawnPoint; // Çöp nesnelerinin oluþturulacaðý nokta
    public float bombSpawnChance = 0.2f; // Bombanýn spawnlanma ihtimali (0.2 = %20)

    void Start()
    {
        // Çöp nesnelerini belirli aralýklarla oluþturmaya baþlamak için coroutine baþlatýyoruz
        StartCoroutine(SpawnTrash());
    }

    IEnumerator SpawnTrash()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval); // Belirli süre bekliyoruz
            SpawnRandomTrashOrBomb(); // Rastgele bir çöp veya bomba nesnesi oluþturuyoruz
        }
    }

    void SpawnRandomTrashOrBomb()
    {
        // Bombanýn spawnlanýp spawnlanmayacaðýna karar veriyoruz
        if (Random.value < bombSpawnChance)
        {
            // Bomba spawnla
            Instantiate(bombPrefab, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            // Rastgele bir çöp prefabý seç ve spawnla
            int randomIndex = Random.Range(0, trashPrefabs.Length);
            GameObject trashToSpawn = trashPrefabs[randomIndex];
            Instantiate(trashToSpawn, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
