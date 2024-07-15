using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yarn : MonoBehaviour
{
    public int id = 0;
    bool following;
    bool dying;
    GameObject player;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (following)
        {
            float threshold = 1f;
            float speed = 0.1f;
            if (player.GetComponent<PlayerCollision>().onGround)
            {
                threshold = 0.001f;
                speed = 0.2f;
            }

            if (dying)
            {
                speed = 0;
            }

            Vector2 direction = (player.transform.position - this.transform.position);

            if (direction.magnitude > threshold)
            {
                direction = direction.normalized;
                this.transform.position = this.transform.position + new Vector3(direction.x, direction.y, 0) * speed;
            }
        }
    }

    public void DestroyYarn()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        PlayerCollision pc = collision.gameObject.GetComponent<PlayerCollision>();
        PlayerSettings ps = collision.gameObject.GetComponent<PlayerSettings>();

        if (tag == "Player" && pc.onGround && !ps.dead)
        {
            animator.SetTrigger("get");
            GameManager.instance.collectYarn(id);
        }
        else if (ps.dead)
        {
            animator.SetTrigger("die");
            dying = true;
        }
        else if (tag == "Player")
        {
            following = true;
            player = collision.gameObject;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        PlayerCollision pc = collision.gameObject.GetComponent<PlayerCollision>();
        PlayerSettings ps = collision.gameObject.GetComponent<PlayerSettings>();

        if (tag == "Player" && pc.onGround && !ps.dead)
        {
            animator.SetTrigger("get");
            GameManager.instance.collectYarn(id);
        }
        else if (ps.dead)
        {
            animator.SetTrigger("die");
            dying = true;
        }
    }
}
