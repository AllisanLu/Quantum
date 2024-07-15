using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yarn : MonoBehaviour
{
    public int id = 0;
    [SerializeField] bool following;
    GameObject player;

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

            Vector2 direction = (player.transform.position - this.transform.position);

            if (direction.magnitude > threshold)
            {
                direction = direction.normalized;
                this.transform.position = this.transform.position + new Vector3(direction.x, direction.y, 0) * speed;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        PlayerCollision pc = collision.gameObject.GetComponent<PlayerCollision>();

        if (tag == "Player" && pc && pc.onGround)
        {
            GameManager.instance.collectYarn(id);
            Destroy(this.gameObject);
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

        if (tag == "Player" && pc && pc.onGround)
        {
            GameManager.instance.collectYarn(id);
            Destroy(this.gameObject);
        }
    }
}
