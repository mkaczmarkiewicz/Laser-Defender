using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int health = 1;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 2f;
    [SerializeField] GameObject laser;
    [SerializeField] float laserSpeed = 10f;
    [SerializeField] GameObject explosionPrefab;

    ScoreSystem scoreSystem;

    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);

        scoreSystem = FindObjectOfType<ScoreSystem>();
    }

    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject projectile = Instantiate(laser, transform.position, Quaternion.identity) as GameObject;
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed * -1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player Laser" || other.tag == "Player")
        {                      
            Die();           
        }       
    }

    private void Die()
    {
        scoreSystem.AddPoints(50);
        AudioSource.PlayClipAtPoint(GetComponent<AudioSource>().clip, transform.position);       
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity) as GameObject;
        Destroy(explosion.gameObject, 1);
        Destroy(gameObject);
    }
}
