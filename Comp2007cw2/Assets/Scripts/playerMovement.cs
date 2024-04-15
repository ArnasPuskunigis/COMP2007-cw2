using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerMovement : MonoBehaviour
{
    public CharacterController characterController;
    public Transform playerCamera;

    private Vector3 moveDirection;
    public float moveSpeed = 5.0f;
    public float gravity = 9.81f;
    public float jumpSpeed = 5.0f;
    private float verticalVelocity;

    public float turnSmooth = 0.1f;
    public float turnSmoothVelocity;

    public boatDrive boatDrivingScript;


    public Transform firePoint;
    public GameObject bullet;
    public GameObject bulletParent;

    public float shootingIntervalTimer;

    public float timeBetweenShots;

    public bool canShoot;
    public bool hasAmmo;
    public bool isReloading;

    public int currentBullets;
    public int clipSize;

    public pauseManager pauseSystem;

    public TextMeshProUGUI bulletText;
    public GameObject reloadText;

    public Animator characterAnim;


    void Start()
    {
        pauseSystem = GameObject.Find("pauseManager").GetComponent<pauseManager>();
        currentBullets = clipSize;
        canShoot = true;
        hasAmmo = true;
        bulletText.text = (currentBullets + "/" + clipSize);
    }

    void Update()
    {

        //Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * 100f, Color.red, 0.5f);

        if (!pauseSystem.gamePaused)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            if (characterController.isGrounded && Input.GetButtonDown("Jump") && !pauseSystem.gamePaused && !boatDrivingScript.isDriving)
            {
                verticalVelocity = jumpSpeed;
                characterAnim.SetTrigger("Jumping");
            }
            verticalVelocity -= gravity * Time.deltaTime;

            if (!boatDrivingScript.isDriving && !pauseSystem.gamePaused)
            {
                Vector3 jumpDir = new Vector3(0f, verticalVelocity, 0f);
                characterController.Move(jumpDir * jumpSpeed * Time.deltaTime);
            }

            Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;
            if (direction.magnitude >= 0.1f && !pauseSystem.gamePaused)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmooth);
                transform.rotation = Quaternion.Euler(0f, playerCamera.eulerAngles.y, 0f);

                if (!boatDrivingScript.isDriving)
                {
                    characterAnim.SetBool("Walking", true);
                    characterAnim.SetBool("Idle", false);
                    characterAnim.SetBool("Sitting", false);
                    Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                    characterController.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);
                }

                if (boatDrivingScript.isDriving)
                {
                    characterAnim.SetBool("Walking", false);
                    characterAnim.SetBool("Idle", false);
                    characterAnim.SetBool("Sitting", true);
                }

            }
            else
            {
                if (!boatDrivingScript.isDriving)
                {
                    characterAnim.SetBool("Walking", false);
                    characterAnim.SetBool("Idle", true);
                }
                else
                {
                    characterAnim.SetBool("Walking", false);
                    characterAnim.SetBool("Idle", false);
                }
                
                if (!pauseSystem.gamePaused)
                {
                    transform.rotation = Quaternion.Euler(0f, playerCamera.eulerAngles.y, 0f);
                }
            }

            Ray camForwardRay = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

            shootingIntervalTimer += Time.deltaTime;

            if (shootingIntervalTimer >= timeBetweenShots && !pauseSystem.gamePaused)
            {
                canShoot = true;
            }

            if (Input.GetKeyDown(KeyCode.R) && !pauseSystem.gamePaused && !boatDrivingScript.isDriving)
            {
                if (currentBullets < clipSize)
                {
                    characterAnim.SetTrigger("Reload");
                    isReloading = true;
                    canShoot = false;
                    Reload();
                }
            }

            if (currentBullets == 0)
            {
                hasAmmo = false;
                //Flash reload image
            }

            if (Input.GetButtonDown("Fire1") && canShoot && hasAmmo && !isReloading && !pauseSystem.gamePaused && !boatDrivingScript.isDriving)
            {
                characterAnim.SetTrigger("Shooting");
                Instantiate(bullet, firePoint.position, playerCamera.transform.rotation, bulletParent.transform);
                canShoot = false;
                shootingIntervalTimer = 0f;
                currentBullets -= 1;
                bulletText.text = (currentBullets + "/" + clipSize);
            }
            else if (!hasAmmo)
            {
                reloadText.SetActive(true);
                bulletText.color = Color.red;
            }
            else
            {
                reloadText.SetActive(false);
                bulletText.color = Color.white;
            }

            if (boatDrivingScript.isDriving)
            {
                bulletText.text = "∞";
            }
            else
            {
                bulletText.text = (currentBullets + "/" + clipSize);
            }

        }

    }

    public void Reload()
    {
        //Play animation
        Invoke("UpdateBulletcount", 1);
    }

    public void UpdateBulletcount()
    {
        currentBullets = clipSize;
        canShoot = true;
        isReloading = false;
        hasAmmo = true;
        bulletText.color = Color.white;
        bulletText.text = (currentBullets + "/" + clipSize);
    }

}

