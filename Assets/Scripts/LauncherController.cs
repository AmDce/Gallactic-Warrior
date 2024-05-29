using System;
using System.Collections;
using System.Collections.Generic;
using Runner.Player;
using UnityEngine;

public class LauncherController : MonoBehaviour
{
    #region SerializeField
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float launchVelocity;


    public List<GameObject> BulletObjPool;
    #endregion

    #region UnityMethods
    private void OnEnable() => PlayerController.OnLauncher += OnLauncingProjectile;

    private void Start() => InitializeObjectPooling();

    private void OnDisable() => PlayerController.OnLauncher -= OnLauncingProjectile;
    #endregion

    #region PrivateMethods
    private void OnLauncingProjectile()
    {
        foreach (var e in BulletObjPool)
        {
            if (!e.activeInHierarchy)
            {
                e.transform.position = transform.position;
                e.SetActive(true);
                e.GetComponent<Rigidbody>().AddRelativeForce(Vector3.right * launchVelocity);
                return;
            }
        }
    }

    private void InitializeObjectPooling()
    {

        for (int i = 0; i < 15; i++)
        {
            var obj = Instantiate(bulletPrefab);
            BulletObjPool.Add(obj);
        }
    }
    #endregion
}
