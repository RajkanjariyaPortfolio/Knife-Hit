using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Knife : MonoBehaviour
{
    [SerializeField] private float speed;

    public Rigidbody2D rb;
    public bool IsReleased { get; set; }
    public bool Hit { get; set; }

    // Start is called before the first frame update
  

    // Update is called once per frame
    

    public void FireKnife()
    {
        if (!IsReleased )
        {
            IsReleased = true;
            rb.AddForce(new Vector2(0f, speed), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wheel") && !GameManager.Instance.IsGameOver && IsReleased)
        {
           
            other.gameObject.GetComponent<Wheel>().KnifeHit(this);
            GameManager.Instance.Score++;
        }
        else if (other.gameObject.CompareTag("Knife") && !Hit && IsReleased && !GameManager.Instance.IsGameOver && other.gameObject.GetComponent<Knife>().IsReleased)
        {
            //GameOver
            transform.SetParent(other.transform);
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;

            GameManager.Instance.IsGameOver = true;
            Invoke(nameof(GameOver), 0.5f);
        }
    }

    private void GameOver()
    {
        UiManager.Instance.GameOver();
    }
}
