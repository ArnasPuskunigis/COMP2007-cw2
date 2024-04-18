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

    public AudioSource shootSound;
    public AudioSource reloadSound;
    public AudioSource step1;
    public AudioSource waterStep;
    public AudioSource woodStep;

    public string currentSurface;

    public bool playingStepSound;

    void Start()
    {
        //Set cursor to hiden and locked
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //Reset the variables
        //Get the pauseSystem script
        pauseSystem = GameObject.Find("pauseManager").GetComponent<pauseManager>();
        currentBullets = clipSize;
        canShoot = true;
        hasAmmo = true;
        bulletText.text = (currentBullets + "/" + clipSize);
    }

    void Update()
    {

        //Check if the game is paused
        if (!pauseSystem.gamePaused)
        {

            //If not paused, allow character movement
            //Unity input system
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            //Check if the player is on the ground and if they are pressing jump and if the game is not paused and if they are not in the boat
            if (characterController.isGrounded && Input.GetButtonDown("Jump") && !pauseSystem.gamePaused && !boatDrivingScript.isDriving)
            {
                //Add jump velocity
                verticalVelocity = jumpSpeed;
                characterAnim.SetTrigger("Jumping");
            }
            verticalVelocity -= gravity * Time.deltaTime;

            if (!boatDrivingScript.isDriving && !pauseSystem.gamePaused)
            {
                Vector3 jumpDir = new Vector3(0f, verticalVelocity, 0f);
                characterController.Move(jumpDir * jumpSpeed * Time.deltaTime);
            }

            //Check for walking movement x and z being above 0.1f
            Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;
            if (direction.magnitude >= 0.1f && !pauseSystem.gamePaused)
            {
                //Move the player and camera
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmooth);
                transform.rotation = Quaternion.Euler(0f, playerCamera.eulerAngles.y, 0f);

                if (!boatDrivingScript.isDriving)
                {
                    //Set character animations
                    characterAnim.SetBool("Walking", true);
                    characterAnim.SetBool("Idle", false);
                    characterAnim.SetBool("Sitting", false);
                    Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                    characterController.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);
                    if (!playingStepSound)
                    {
                        //Play stepping sound
                        woodStep.Play();
                        playingStepSound = true;
                    }
                }

                if (boatDrivingScript.isDriving)
                {
                    //Turn off player walking animations and enable the sitting animation, stop the stepping sound
                    playingStepSound = false;
                    characterAnim.SetBool("Walking", false);
                    characterAnim.SetBool("Idle", false);
                    characterAnim.SetBool("Sitting", true);
                }

            }
            else
            {
                if (!boatDrivingScript.isDriving)
                {
                    //Otherwise the player is not moving or in the boat and should play the idle animation
                    //Pause the stepping sound
                    characterAnim.SetBool("Walking", false);
                    characterAnim.SetBool("Idle", true);
                    woodStep.Pause();
                    playingStepSound = false;
                }
                else
                {
                    //Else the player is in the boat
                    woodStep.Pause();
                    playingStepSound = false;
                    characterAnim.SetBool("Walking", false);
                    characterAnim.SetBool("Idle", false);
                }
                
                if (!pauseSystem.gamePaused)
                {
                    transform.rotation = Quaternion.Euler(0f, playerCamera.eulerAngles.y, 0f);
                }
            }

            //Draw ray from the camera forwards for the aiming system
            Ray camForwardRay = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

            shootingIntervalTimer += Time.deltaTime;

            // If the reload timer has completed and the game is not paused, set can shoot to true so the player can shoot
            if (shootingIntervalTimer >= timeBetweenShots && !pauseSystem.gamePaused)
            {
                canShoot = true;
            }

            //If the game is not paused and the player is not in the boat, and the player pressed R
            if (Input.GetKeyDown(KeyCode.R) && !pauseSystem.gamePaused && !boatDrivingScript.isDriving)
            {
                //If the player can reload
                if (currentBullets < clipSize)
                {
                    //Set the reload animation variable
                    characterAnim.SetTrigger("Reload");
                    //Start reloading
                    isReloading = true;
                    canShoot = false;
                    Reload();
                }
            }

            if (currentBullets == 0)
            {
                //Set hasAmmo to false;
                hasAmmo = false;
            }

            //If player uses left mouse button or other default fire1 inputs from the unity system and they have ammo and can shoot and are not reloading and the game is not paused and the player is not in the boat
            if (Input.GetButtonDown("Fire1") && canShoot && hasAmmo && !isReloading && !pauseSystem.gamePaused && !boatDrivingScript.isDriving)
            {
                //Set the shotting animation variable
                characterAnim.SetTrigger("Shooting");
                //Spawn the bullet
                Instantiate(bullet, firePoint.position, playerCamera.transform.rotation, bulletParent.transform);
                //Set shooting variables
                canShoot = false;
                shootingIntervalTimer = 0f;
                currentBullets -= 1;
                shootSound.Play();
                bulletText.text = (currentBullets + "/" + clipSize);
            }
            else if (!hasAmmo)
            {
                //Otherwise if the player has no ammo then show reload text and set the ammo text to red
                reloadText.SetActive(true);
                bulletText.color = Color.red;
            }
            else
            {
                //Else the player has ammo and reset the text and color 
                reloadText.SetActive(false);
                bulletText.color = Color.white;
            }

            if (boatDrivingScript.isDriving)
            {
                //If the player is driving then they have infinite ammo on the boat cannon
                bulletText.text = "∞";
            }
            else
            {
                //Otherwise just show the normal buttlet text
                bulletText.text = (currentBullets + "/" + clipSize);
            }

        }

    }

    public void Reload()
    {
        //Play the reload sound and then run the bullet count function
        reloadSound.Play();
        Invoke("UpdateBulletcount", 2.2f);
    }

    public void UpdateBulletcount()
    {
        //Reset reserve bullet system
        currentBullets = clipSize;
        canShoot = true;
        isReloading = false;
        hasAmmo = true;
        bulletText.color = Color.white;
        bulletText.text = (currentBullets + "/" + clipSize);
    }

}

