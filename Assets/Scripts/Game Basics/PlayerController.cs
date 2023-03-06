using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviourPun, IPunObservable
{
    [Header("Stats")]
    [SerializeField]
    private float speed = 600f;

    [SerializeField]
    private float jumpForce = 800f;

    private Rigidbody2D rb;
    private float desiredMovementAxis = 0f;

    private PhotonView pv;
    private Vector3 enemyPosition = Vector3.zero;
    [SerializeField]
    SpriteRenderer spriteRenderer;
    [SerializeField]
    Animator anim;
    public bool isFlipped;
    [SerializeField]
    Transform bulletSpawn;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pv = GetComponent<PhotonView>();

        PhotonNetwork.SendRate = 20;
        PhotonNetwork.SerializationRate = 20;

    }

    private void Update()
    {
        if (pv.IsMine)
        {
            CheckInputs();
        }
        else
        {
            SmoothReplicate();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(desiredMovementAxis * Time.fixedDeltaTime * speed, rb.velocity.y);

    }

    private void CheckInputs()
    {
        desiredMovementAxis = Input.GetAxisRaw("Horizontal");
        if(desiredMovementAxis < 0 && !isFlipped)
        {
            Flip();
        }else if(desiredMovementAxis > 0 && isFlipped)
        {
            Flip();
        }
        if(desiredMovementAxis < 0 || desiredMovementAxis > 0)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Approximately(rb.velocity.y, 0f))
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            anim.SetBool("isJumping", true);
        }
        else
        {
            anim.SetBool("isJumping", false);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Pium();
        }
    }

    private void SmoothReplicate()
    {
        transform.position = Vector3.Lerp(bulletSpawn.position, enemyPosition, Time.deltaTime * 20);
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {

            stream.SendNext(transform.position);

        }
        else if (stream.IsReading)
        {

            enemyPosition = (Vector3)stream.ReceiveNext();
        }
    }

    void Pium()
    {
        PhotonNetwork.Instantiate("Bullet", transform.position + new Vector3(1, 0, 0), Quaternion.identity);
    }

    public void Damage()
    {
        pv.RPC("NetworkDamage", RpcTarget.All);
    }

    [PunRPC]
    private void NetworkDamage()
    {
        Destroy(this.gameObject);
    }

    void Flip()
    {
        //Flip player
        isFlipped = !isFlipped;
        spriteRenderer.flipX = isFlipped;

        bulletSpawn.localPosition = new Vector3(bulletSpawn.localPosition.x * -1, bulletSpawn.localPosition.y, bulletSpawn.localPosition.z);
    }
}
