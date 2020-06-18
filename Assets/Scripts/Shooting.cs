using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float bulletForce;
    public Transform gun;
    public GameObject bulletPrefab;

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, gun.position, gun.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(gun.up * bulletForce, ForceMode2D.Impulse);
    }

}
