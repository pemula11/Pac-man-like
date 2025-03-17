using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Action OnPowerUpStart;
    public Action OnPowerUpEnd;

    private Rigidbody _rigidbody;

    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform _camera;
    [SerializeField] float _powerUpDuration = 5f;

    private Coroutine powerUpCoroutine; 

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
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
        Debug.Log("start ffff");
        OnPowerUpStart?.Invoke();
        yield return new WaitForSeconds(_powerUpDuration);
        OnPowerUpEnd?.Invoke();
    }

    public void pickPowerUp()
    {
        Debug.Log("start powerupsssssssssssssssssssssssssss");
        if (powerUpCoroutine != null)
        {
            StopCoroutine(powerUpCoroutine);
        }

        powerUpCoroutine = StartCoroutine(StartPowerUp());

    }
}
