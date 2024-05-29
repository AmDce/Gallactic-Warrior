using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using System;
using System.Collections;

public class EnemyLauncherController : MonoBehaviour
{
    #region SerializeField
    [SerializeField]
    public GameObject Bullet;

    [SerializeField]
    public Transform player;

    [SerializeField]
    private int poolSize = 5;

    [SerializeField]
    public float LaunchVelocity;
    [SerializeField]
    private float removeDelay = 10f;
    [SerializeField]
    private float hitEffectDuration = 0.2f;
    [SerializeField]
    private float hitEffectAlpha = 0.5f;
    [SerializeField]
    private float launchInterval = 2f;

    [SerializeField]
    private List<GameObject> BulletObjPool;
    #endregion

    private int _lives = 2;
    private SpriteRenderer _spriteRenderer;

    #region PrivateMethods
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        InitializeObjectPooling();
    }

    private void OnEnable()
    {
        Invoke($"OnRemove", removeDelay);
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("OnLauncingProjectile", launchInterval, 1);
    }

    private void Update()
    {
        float distance = UnityEngine.Vector3.Distance(transform.position, player.transform.position);
        if (distance <= 1)
        {
            Invoke("OnRemove", 0f);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LoseLife();
        }
    }

    private void OnLauncingProjectile()
    {
        foreach (var e in BulletObjPool)
        {
            if (!e.activeInHierarchy)
            {
                e.transform.position = transform.position;
                e.SetActive(true);
                e.GetComponent<Rigidbody>().velocity = (player.position - e.transform.position).normalized * LaunchVelocity * Time.deltaTime;
                return;
            }
        }
    }

    private void InitializeObjectPooling()
    {
        for (int i = 0; i < 5; i++)
        {
            var obj = Instantiate(Bullet);
            obj.tag = "Enemy";
            BulletObjPool.Add(obj);
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
    #endregion
}
