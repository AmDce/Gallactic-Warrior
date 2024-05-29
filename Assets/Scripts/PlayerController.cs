using System;
using System.Collections;
using UnityEngine;
using UnityEngine.WSA;

namespace Runner.Player
{
    public class PlayerController : MonoBehaviour
    {
        private float speed = 10f;
        private float smoothing = 5f;

        private float _launchBetweenTime;

        private int _lives = 3;

        private Vector3 targetPosition;

        public static event Action OnLauncher;

        void Start()
        {
            targetPosition = transform.position;
        }

        void Update()
        {
            targetPosition.x += 5.0f * Time.deltaTime;
            float verticalInput = Input.GetAxis("Vertical");
            targetPosition += new Vector3(0, verticalInput, 0) * speed * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
            if (Time.time > _launchBetweenTime && Input.GetKeyDown(KeyCode.Space))
            {
                OnLauncher.Invoke();
                _launchBetweenTime = Time.time + 1f;
            }

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                LoseLife();
            }
        }

        private void LoseLife()
        {
            StartCoroutine(ShowHitEffects());
            _lives--;
            MenuController.MenuController.UpdateLives(_lives);
            if (_lives <= 0)
            {
                MenuController.MenuController.OnEnablePanel(0);
            }
        }

        IEnumerator ShowHitEffects()
        {
            Color hitColor = GetComponent<SpriteRenderer>().color;
            hitColor.a = 0.5f;
            GetComponent<SpriteRenderer>().color = hitColor;
            yield return new WaitForSeconds(0.2f);
            hitColor.a = 1f;
            GetComponent<SpriteRenderer>().color = hitColor;
        }
    }
}

