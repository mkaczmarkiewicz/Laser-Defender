using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] float speed = 10f;
    [SerializeField] float padding = 0.02f;
    [Header("Player Combat")]
    [SerializeField] int health = 3;
    [SerializeField] GameObject laser;
    [SerializeField] float laserSpeed = 10f;
    [SerializeField] float cooldown = 0.2f;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] Vector3 respawnPosition;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    Coroutine firingCoroutine;
    AudioSource audioSource;

    private void Start()
    {
        SetUpMoveBoundaries();
        audioSource = GetComponent<AudioSource>();
        respawnPosition = transform.position;
    }

    void Update()
    {
        Move();
        Fire();
    }

    private void Move()
    {
        var newPositionX = transform.position.x + (Input.GetAxis("Horizontal") * Time.deltaTime * speed);
        var newPositionY = transform.position.y + Input.GetAxis("Vertical") * Time.deltaTime * speed;

        //Boundaries:
        if (newPositionX > xMin && newPositionX < xMax)
            transform.position = new Vector2(newPositionX, transform.position.y);

        if (newPositionY > yMin && newPositionY < yMax)
            transform.position = new Vector2(transform.position.x, newPositionY);
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Jump"))
        {
            firingCoroutine = StartCoroutine(FireContinuosly());
        }

        if (Input.GetButtonUp("Jump"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    private IEnumerator FireContinuosly()
    {
        while(true)
        {
            GameObject projectile = Instantiate(laser, transform.position, Quaternion.identity) as GameObject;
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);           

            yield return new WaitForSeconds(cooldown);
        }       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ProcessHit(other);
    }

    private void ProcessHit(Collider2D other)
    {
        if (other.tag == "Enemy Laser" || other.tag == "Enemy")
        {
            health--;

            if (health <= 0)
            {
                SceneManager.LoadScene("GameOver");
            }
            else
            {
                Die();
                Respawn();                
            }
       }
    }

    private void Die()
    {
        AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity) as GameObject;
        Destroy(explosion.gameObject, 1);        
    }

    private void Respawn()
    {
        transform.position = respawnPosition;       
        StartCoroutine(Blinking());

    }

    private IEnumerator Blinking()
    {
        Color tmp = GetComponent<SpriteRenderer>().color;

        for (int i = 0; i < 5; i++)
        {
            tmp.a = 0f;
            GetComponent<SpriteRenderer>().color = tmp;
            yield return new WaitForSeconds(0.1f);
            tmp.a = 255f;
            GetComponent<SpriteRenderer>().color = tmp;
            yield return new WaitForSeconds(0.1f);
        }
       
        StopCoroutine(Blinking());
    }


    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1f, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1f, 0)).y - padding;
    }

    public string GetHealth()
    {
        return health.ToString();
    }
}

/*private void ProcessHit(Collider2D other)
    {
        if (other.tag == "Enemy Laser")
        {
            DamageDealer damageDealer = other.GetComponent<DamageDealer>();
            health -= damageDealer.GetDamage();
            Destroy(other.gameObject);
            Debug.Log("Health: " + health);
        }

        if (other.tag == "Enemy")
        {
            Debug.Log("B");
            Die();
            
        }
    }*/
