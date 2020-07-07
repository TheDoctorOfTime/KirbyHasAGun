using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public Rigidbody2D rb;
    private Camera cam;

    public GameObject bulletPrefab;
    public GameObject stealPrefab;

    Vector3 mousePosition;
    Vector3 lookDir;

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0)) shoot();
        if (Input.GetMouseButtonDown(1)) steal();
    }

    private void FixedUpdate()
    {
        lookDir = mousePosition - gameObject.transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        rb.position = GameObject.FindGameObjectWithTag("Player").gameObject.transform.position;
        rb.rotation = angle;
    }

    void shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, gameObject.transform.position, gameObject.transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        rb.AddForce(gameObject.transform.up * 20f, ForceMode2D.Impulse);
    }

    void steal()
    {
        GameObject bullet = Instantiate(stealPrefab, gameObject.transform.position, gameObject.transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        rb.AddForce(gameObject.transform.up * 8f, ForceMode2D.Impulse);
    }
}
