using UnityEngine;
using Lean.Touch;

public class Snap : MonoBehaviour
{
    [SerializeField]
    private Transform snapPosition; // Posisi yang diinginkan untuk menjepit objek

    [SerializeField]
    private float snapScaleMultiplier = 2f; // Faktor skala setelah menjepit

    public GameObject PosisiKaleng;
    public GameObject Check;
    public GameObject Decor;
    private Vector3 startPosition;
    private Vector3 startScale;
    private bool isDragging = false;

    private void Start()
    {
        startScale = transform.localScale;
    }

    private void OnMouseDown()
    {
        if (!isDragging)
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
            if (IsInSnapPosition() && PosisiKaleng.transform.childCount <= 0)
            {
                // Jika iya, atur posisi objek ke posisi snap
                // transform.position = snapPosition.position;
                GameObject newLine = Instantiate(gameObject, PosisiKaleng.transform.position, Quaternion.identity, snapPosition);
                
                newLine.gameObject.transform.localScale = startScale * snapScaleMultiplier;

                // Perbesar skala objek setelah menjepit
                // transform.localScale = startScale * snapScaleMultiplier;
                // Set objek yang menjepit sebagai objek yang telah menjepit di posisi snap
                gameObject.SetActive(false);
                Check.SetActive(true);
                Decor.SetActive(false);
            }
            else
            {
                // Jika tidak, kembalikan objek ke posisi awal dan skala awal
                transform.position = startPosition;
                transform.localScale = startScale;
            }
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
}
