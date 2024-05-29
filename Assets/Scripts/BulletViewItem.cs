using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletViewItem : MonoBehaviour
{
    private void OnEnable() => Invoke($"OnRemove", 5f);
    private void OnRemove() => this.gameObject.SetActive(false);
}
