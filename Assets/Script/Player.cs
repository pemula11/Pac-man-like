using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Action OnPowerUpStart;
    public Action OnPowerUpEnd;

    private Rigidbody _rigidbody;

    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform _camera;
    [SerializeField] float _powerUpDuration = 5f;

    [SerializeField] private int _health = 3;
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private Transform _respawnPoint;

    private Coroutine powerUpCoroutine;
    private bool _isPowerUpActive = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        UpdateUI();
    }

    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 horizontalDirection = horizontal * _camera.right;
        Vector3 verticalDirection = vertical * _camera.forward;

        horizontalDirection.y = 0;
        verticalDirection.y = 0;



        Vector3 movement = horizontalDirection + verticalDirection;

        _rigidbody.linearVelocity = movement * speed * Time.deltaTime;
    }

    private IEnumerator StartPowerUp()
    {
        _isPowerUpActive = true;
        OnPowerUpStart?.Invoke();
        yield return new WaitForSeconds(_powerUpDuration);
        _isPowerUpActive = false;
        OnPowerUpEnd?.Invoke();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (_isPowerUpActive)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<Enemy>().Dead();
            }
        }
    }

    private void UpdateUI()
    {
        _healthText.text = "Health: " + _health;
    }

    public void pickPowerUp()
    {

        if (powerUpCoroutine != null)
        {
            StopCoroutine(powerUpCoroutine);
        }

        powerUpCoroutine = StartCoroutine(StartPowerUp());

    }

    public void Dead()
    {
        _health--;
       
        if (_health <= 0)
        {
            _health = 0;
            SceneManager.LoadScene("LoseScreen");
        }
        else
        {
            transform.position = _respawnPoint.position;
        }
        UpdateUI();

       

    }

}
