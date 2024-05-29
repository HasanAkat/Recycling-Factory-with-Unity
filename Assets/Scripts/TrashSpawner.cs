using System.Collections;
using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    public GameObject[] trashPrefabs; // ��p prefablar�n� buraya atayaca��z (ka��t, cam, plastik)
    public GameObject bombPrefab; // Bomba prefab�
    public float spawnInterval = 1f; // ��p olu�turma aral��� (saniye)
    public Transform spawnPoint; // ��p nesnelerinin olu�turulaca�� nokta
    public float bombSpawnChance = 0.2f; // Bomban�n spawnlanma ihtimali (0.2 = %20)

    void Start()
    {
        // ��p nesnelerini belirli aral�klarla olu�turmaya ba�lamak i�in coroutine ba�lat�yoruz
        StartCoroutine(SpawnTrash());
    }

    IEnumerator SpawnTrash()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval); // Belirli s�re bekliyoruz
            SpawnRandomTrashOrBomb(); // Rastgele bir ��p veya bomba nesnesi olu�turuyoruz
        }
    }

    void SpawnRandomTrashOrBomb()
    {
        // Bomban�n spawnlan�p spawnlanmayaca��na karar veriyoruz
        if (Random.value < bombSpawnChance)
        {
            // Bomba spawnla
            Instantiate(bombPrefab, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            // Rastgele bir ��p prefab� se� ve spawnla
            int randomIndex = Random.Range(0, trashPrefabs.Length);
            GameObject trashToSpawn = trashPrefabs[randomIndex];
            Instantiate(trashToSpawn, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
