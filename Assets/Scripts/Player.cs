using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] float mouseSens = 3f;
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float mass = 1f;
    [SerializeField] Transform cameraTransform;

    [SerializeField] private GameObject pauseOverlay;
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _sliderText;

    CharacterController controller;
    Vector3 velocity;
    Vector2 look;

    private bool isPaused = false;

    private PhotonView view;

    // Meme Stuff
    [SerializeField] private GameObject feet;
    [SerializeField] private Toggle feetToggle;
    private bool isFeetActive = true;

    void Awake()
    {
        view = GetComponent<PhotonView>();
        mouseSens = PlayerPrefs.GetFloat("Sensitivity");
        controller = GetComponent<CharacterController>();
    }

    void Start()
    {
        _slider.value = mouseSens;
        _sliderText.text = mouseSens.ToString("0.0");

        Cursor.lockState = CursorLockMode.Locked;
        _slider.onValueChanged.AddListener((v) =>
        {
            mouseSens = v;
            _sliderText.text = v.ToString("0.0");

            PlayerPrefs.SetFloat("Sensitivity", mouseSens);
        });

        feetToggle.onValueChanged.AddListener((v) =>
        {
            isFeetActive = v;
        });

        if (mouseSens < 1f)
            mouseSens = 1f;
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
                Cursor.lockState = CursorLockMode.Locked;
                pauseOverlay.SetActive(false);
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                pauseOverlay.SetActive(true);
            }
        }
            

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }

        feet.SetActive(isFeetActive);
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

    public bool getPaused()
    {
        return isPaused;
    }

    public void setPaused(bool paused)
    {
        this.isPaused = paused;
    }
}
