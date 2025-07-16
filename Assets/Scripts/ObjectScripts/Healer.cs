using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : MonoBehaviour
{
    [SerializeField] int _healtAmmount = 1;

    private void OnTriggerEnter(Collider other)
    {
        LifeController lifeController = other.GetComponent<LifeController>();
        lifeController.AddHp(_healtAmmount);
       Destroy(gameObject);

    }
}
