using UnityEngine;

public class RandomizeTrashBins : MonoBehaviour
{
    public Transform[] trashBins; // Çöp kutularýnýn referanslarýný buraya atayacaðýz

    public void RandomizePositions()
    {
        Vector3[] originalPositions = new Vector3[trashBins.Length];

        // Mevcut pozisyonlarý kaydediyoruz
        for (int i = 0; i < trashBins.Length; i++)
        {
            originalPositions[i] = trashBins[i].position;
        }

        // Pozisyonlarý rastgele karýþtýrýyoruz
        for (int i = 0; i < trashBins.Length; i++)
        {
            int randomIndex = Random.Range(0, trashBins.Length);
            Vector3 temp = originalPositions[i];
            originalPositions[i] = originalPositions[randomIndex];
            originalPositions[randomIndex] = temp;
        }

        // Yeni rastgele pozisyonlarý çöp kutularýna atýyoruz
        for (int i = 0; i < trashBins.Length; i++)
        {
            trashBins[i].position = originalPositions[i];
        }
    }
}
