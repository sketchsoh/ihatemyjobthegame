using System;
using UnityEngine;

public class Food : MonoBehaviour
{
    public enum FoodType
    {
        G1,
        G2,
        G3,
        B1,
        B2,
        B3
    };
    public FoodType foodType;
    private Rigidbody2D rb2d;
    private float speed = 100;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void InitializeFood(float foodSpeed)
    {
        speed = foodSpeed;
    }

    void Update()
    {
        rb2d.linearVelocity = new Vector3(0, -speed * Time.deltaTime, 0);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            switch (foodType)
            {
                case FoodType.G1:
                {
                    other.gameObject.GetComponent<HealthBar>().Heal(10);
                    break;
                }
                case FoodType.G2:
                {
                    other.gameObject.GetComponent<HealthBar>().Heal(20);
                    break;
                }
                case FoodType.G3:
                {
                    other.gameObject.GetComponent<HealthBar>().Heal(30);
                    break;
                }
                case FoodType.B1:
                {
                    other.gameObject.GetComponent<HealthBar>().TakeDamage(10);
                    break;
                }
                case FoodType.B2:
                {
                    other.gameObject.GetComponent<HealthBar>().TakeDamage(20);
                    break;
                }
                case FoodType.B3:
                {
                    other.gameObject.GetComponent<HealthBar>().TakeDamage(30);
                    break;
                }
            }
            Destroy(gameObject);
        } 
    }
}
