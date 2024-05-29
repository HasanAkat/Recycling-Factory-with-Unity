using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 offset;
    private Vector3 lastPosition;
    private float zCoord;

    void OnMouseDown()
    {
        lastPosition = transform.position;
        zCoord = Camera.main.WorldToScreenPoint(transform.position).z; // Derinliði sabitleyin
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
                // Çöp objesi bir çöp kutusuna düþürüldüðünde animasyonu tetikle
                TrashBinAnimation trashBinAnimation = hitInfo.transform.GetComponent<TrashBinAnimation>();
                if (trashBinAnimation != null)
                {
                    trashBinAnimation.PlayOpenCloseAnimation();
                }

                // Skoru güncelle ve çöp objesini yok et
                ScoreManager.Instance.HandleTrashDrop(gameObject, hitInfo.transform.tag);
                Destroy(gameObject);
            }
            else
            {
                // Eðer çöp objesi bir çöp kutusuna düþmediyse, çöp objesi týklanmadan önceki konumuna geri döner.
                transform.position = lastPosition;
            }
        }
        else
        {
            // Eðer raycast bir objeye çarpmadýysa, çöp objesi týklanmadan önceki konumuna geri döner.
            transform.position = lastPosition;
        }
        GetComponent<Collider>().enabled = true;
    }

    private Vector3 MouseWorldPosition()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = zCoord; // Sabit Z koordinatýný kullanýn
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }
}
