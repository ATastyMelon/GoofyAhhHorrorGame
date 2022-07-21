using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using TMPro;

namespace atmgames.player
{

    [RequireComponent(typeof(PhotonView))]
    public class Player : MonoBehaviourPunCallbacks
    {
        [SerializeField] float mouseSens = 3f;
        [SerializeField] float movementSpeed = 5f;
        [SerializeField] float jumpSpeed = 5f;
        [SerializeField] float mass = 1f;
        [SerializeField] Transform cameraTransform;

        [SerializeField] private GameObject pauseOverlay;
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _sliderText;

        [SerializeField] private Animator animator;

        CharacterController controller;
        Vector3 velocity;
        Vector2 look;

        private bool isPaused = false;
        private bool muted = false;

        private PhotonView view;

        // Meme Stuff
        [SerializeField] private GameObject feet;
        [SerializeField] private Toggle feetToggle;
        private bool isFeetActive = true;

        [SerializeField] private string menuScene;

        void Awake()
        {
            view = GetComponent<PhotonView>();
            mouseSens = PlayerPrefs.GetFloat("Sensitivity");
            controller = GetComponent<CharacterController>();
        }

        void Start()
        {
            AudioListener.volume = 0;
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Update is called once per frame
        void Update()
        {

            if (view.IsMine)
            {
                UpdateGravity();

                if (!isPaused)
                {
                    UpdateLook();
                    UpdateMovement();
                }

                UpdateInputs();

                if (mouseSens < 1f)
                    mouseSens = 1f;

                if (muted)
                {
                    AudioListener.volume = 0;
                }
                else
                {
                    AudioListener.volume = 1;
                }

                if (isPaused)
                {
                    Cursor.lockState = CursorLockMode.None;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }
        }

        void UpdateGravity()
        {
            var gravity = Physics.gravity * mass * Time.deltaTime;
            velocity.y = controller.isGrounded ? -1f : velocity.y + gravity.y;
        }

        void UpdateMovement()
        {
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");

            var input = new Vector3();
            input += transform.forward * y;
            input += transform.right * x;
            input = Vector3.ClampMagnitude(input, 1f);

            if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
            {
                animator.SetTrigger("jump");
                velocity.y += jumpSpeed;
            }

            if (input.x != 0 || input.z != 0)
            {
                animator.SetBool("IsRunning", true);
            }
            else
            {
                animator.SetBool("IsRunning", false);
            }

            controller.Move((input * movementSpeed + velocity) * Time.deltaTime);
        }

        void UpdateLook()
        {
            look.x += Input.GetAxis("Mouse X") * mouseSens;
            look.y += Input.GetAxis("Mouse Y") * mouseSens;

            look.y = Mathf.Clamp(look.y, -89f, 89f);

            cameraTransform.localRotation = Quaternion.Euler(-look.y, 0, 0);
            transform.localRotation = Quaternion.Euler(0, look.x, 0);
        }

        void UpdateInputs()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PhotonNetwork.Disconnect();
                SceneManager.LoadScene(menuScene);
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                muted = !muted;
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                isPaused = !isPaused;
            }
        }
    }
}
