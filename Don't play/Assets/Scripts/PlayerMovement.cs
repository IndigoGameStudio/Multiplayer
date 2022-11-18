using System.Collections;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
	CharacterController2D controller;
    Animator Anim;
    SpriteRenderer PlayerSkin;
    PhotonView photonView;

    [Range(0, 100)] [SerializeField] float RunSpeed = 40f;
    [Range(1, 5)] [SerializeField] int JumpTimes = 3;
    [Range(0, 4)] [SerializeField] int HasWeapon = 0;

    [SerializeField] GameObject[] Weapons;

    int Jumping;
    float HorizontalMove = 0f;
    bool isJumping = false;
    bool isCrouching = false;
    bool isAttacking = false;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        controller = GetComponent<CharacterController2D>();
        Anim = GetComponent<Animator>();
        PlayerSkin = GetComponent<SpriteRenderer>();

        Jumping = JumpTimes;
    }

    // ========================================================================

    private void Update()
    {
        if (photonView.IsMine)
        {
            HorizontalMove = Input.GetAxisRaw("Horizontal") * RunSpeed;
            Anim.SetFloat("Running", Mathf.Abs(HorizontalMove));

            if (Input.GetKey(KeyCode.Space))
            {
                Anim.SetBool("Jumping", isJumping);
                isJumping = true; 
            }

            if (Input.GetKeyDown(KeyCode.Space) && Jumping > 1)
            {
                Jumping -= 1;
                controller.Jump();
            }

            isCrouching = Input.GetKeyDown(KeyCode.C) ? true : false;

            if (Input.GetKey(KeyCode.F))
            {
                PlayerAttack();
            }
        }
    }

    // ========================================================================

    private void PlayerAttack()
    {
        if (isAttacking || isCrouching)
            return;
        Weapons[HasWeapon].SetActive(true);
        PlayerSkin.enabled = false;
        StartCoroutine(hideWeapon());
        isAttacking = true;
    }

    // ========================================================================

    IEnumerator hideWeapon()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerSkin.enabled = true;
        foreach (var item in Weapons)
        {
            item.SetActive(false);
        }
        isAttacking = false;
    }

    // ========================================================================

    public void OnLanding()
    {
        isJumping = false;
        Anim.SetBool("Jumping", isJumping);
        Jumping = JumpTimes;
    }

    // ========================================================================

    public void OnCrouching(bool value)
    {
        Anim.SetBool("Crouching", value);
    }

    // ========================================================================

    private void FixedUpdate()
    {
        if (isAttacking && controller.m_Grounded)
        { HorizontalMove = 0; }
        controller.Move(HorizontalMove * Time.fixedDeltaTime, isCrouching, isJumping);
    }
}
