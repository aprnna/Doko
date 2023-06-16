using UnityEngine;
using Lean.Touch;

public class Snap : MonoBehaviour
{
    [SerializeField]
    private Transform snapPosition; // Posisi yang diinginkan untuk menjepit objek

    [SerializeField]
    private float snapScaleMultiplier = 1.5f; // Faktor skala setelah menjepit

    private Vector3 startPosition;
    private Vector3 startScale;
    private bool isDragging = false;
    private bool isSnapped = false;
    private GameObject snappedObject;

    private void Start()
    {
        startScale = transform.localScale;
    }

    private void OnMouseDown()
    {
        if (!isDragging && !isSnapped)
        {
            startPosition = transform.position;
            isDragging = true;
        }
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
        }
    }

    private void OnMouseUp()
    {
        if (isDragging)
        {
            isDragging = false;

            // Cek apakah objek berada di posisi yang diinginkan dan tidak ada objek lain di posisi snap
            if (IsInSnapPosition() && snappedObject == null)
            {
                // Jika iya, atur posisi objek ke posisi snap
                transform.position = snapPosition.position;
                isSnapped = true;

                // Perbesar skala objek setelah menjepit
                transform.localScale = startScale * snapScaleMultiplier;

                // Set objek yang menjepit sebagai objek yang telah menjepit di posisi snap
                snappedObject = gameObject;
            }
            else
            {
                // Jika tidak, kembalikan objek ke posisi awal dan skala awal
                transform.position = startPosition;
                transform.localScale = startScale;
            }
        }
        else if (isSnapped)
        {
            // Jika objek sudah menjepit dan diubah posisinya, kembalikan ke posisi awal dan skala awal
            transform.position = startPosition;
            transform.localScale = startScale;
            isSnapped = false;

            // Reset objek yang telah menjepit di posisi snap
            snappedObject = null;
        }
    }

    private bool IsInSnapPosition()
    {
        // Ganti kode ini dengan kondisi yang sesuai untuk menentukan apakah objek berada di posisi snap yang diinginkan.
        // Misalnya, jika menggunakan jarak toleransi:
        float tolerance = 1f;
        float distance = Vector3.Distance(transform.position, snapPosition.position);
        return distance <= tolerance;
    }
    private void Update()
    {
        Debug.Log(snappedObject);
    }
}
