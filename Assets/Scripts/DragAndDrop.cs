using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 offset;
    private Vector3 lastPosition;
    private float zCoord;

    void OnMouseDown()
    {
        lastPosition = transform.position;
        zCoord = Camera.main.WorldToScreenPoint(transform.position).z; // Derinli�i sabitleyin
        offset = transform.position - MouseWorldPosition();
        GetComponent<Collider>().enabled = false;
    }

    void OnMouseDrag()
    {
        transform.position = MouseWorldPosition() + offset;
    }

    void OnMouseUp()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo))
        {
            if (hitInfo.transform.CompareTag("PaperBin") || hitInfo.transform.CompareTag("GlassBin") || hitInfo.transform.CompareTag("PlasticBin") || hitInfo.transform.CompareTag("MetalBin") || hitInfo.transform.CompareTag("OrganicBin"))
            {
                // ��p objesi bir ��p kutusuna d���r�ld���nde animasyonu tetikle
                TrashBinAnimation trashBinAnimation = hitInfo.transform.GetComponent<TrashBinAnimation>();
                if (trashBinAnimation != null)
                {
                    trashBinAnimation.PlayOpenCloseAnimation();
                }

                // Skoru g�ncelle ve ��p objesini yok et
                ScoreManager.Instance.HandleTrashDrop(gameObject, hitInfo.transform.tag);
                Destroy(gameObject);
            }
            else
            {
                // E�er ��p objesi bir ��p kutusuna d��mediyse, ��p objesi t�klanmadan �nceki konumuna geri d�ner.
                transform.position = lastPosition;
            }
        }
        else
        {
            // E�er raycast bir objeye �arpmad�ysa, ��p objesi t�klanmadan �nceki konumuna geri d�ner.
            transform.position = lastPosition;
        }
        GetComponent<Collider>().enabled = true;
    }

    private Vector3 MouseWorldPosition()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = zCoord; // Sabit Z koordinat�n� kullan�n
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }
}
