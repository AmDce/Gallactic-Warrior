using System;
using System.Collections;
using System.Collections.Generic;
using Runner.MenuController;
using TMPro;
using UnityEngine;

public class ObstacleViewItem : MonoBehaviour
{
    #region SerializeField
    [SerializeField]
    private int _lives = 2;

    [SerializeField]
    private float removeDelay = 10f;
    [SerializeField]
    private float hitEffectDuration = 0.2f;
    [SerializeField]
    private float hitEffectAlpha = 0.5f;
    #endregion

    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        Invoke($"OnRemove", removeDelay);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LoseLife();
        }
    }

    private void LoseLife()
    {
        _lives--;
        StartCoroutine(ShowHitEffects());
    }

    private IEnumerator ShowHitEffects()
    {
        if (_spriteRenderer != null)
        {
            Color originalColor = _spriteRenderer.color;
            Color hitColor = originalColor;
            hitColor.a = hitEffectAlpha;
            _spriteRenderer.color = hitColor;

            yield return new WaitForSeconds(hitEffectDuration);

            _spriteRenderer.color = originalColor;

            if (_lives <= 0)
            {
                OnRemove();
            }
        }
    }

    private void OnRemove()
    {
        gameObject.SetActive(false);
    }
}
