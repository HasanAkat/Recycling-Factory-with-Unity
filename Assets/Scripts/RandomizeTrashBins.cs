using UnityEngine;

public class RandomizeTrashBins : MonoBehaviour
{
    public Transform[] trashBins; // ��p kutular�n�n referanslar�n� buraya atayaca��z

    public void RandomizePositions()
    {
        Vector3[] originalPositions = new Vector3[trashBins.Length];

        // Mevcut pozisyonlar� kaydediyoruz
        for (int i = 0; i < trashBins.Length; i++)
        {
            originalPositions[i] = trashBins[i].position;
        }

        // Pozisyonlar� rastgele kar��t�r�yoruz
        for (int i = 0; i < trashBins.Length; i++)
        {
            int randomIndex = Random.Range(0, trashBins.Length);
            Vector3 temp = originalPositions[i];
            originalPositions[i] = originalPositions[randomIndex];
            originalPositions[randomIndex] = temp;
        }

        // Yeni rastgele pozisyonlar� ��p kutular�na at�yoruz
        for (int i = 0; i < trashBins.Length; i++)
        {
            trashBins[i].position = originalPositions[i];
        }
    }
}
