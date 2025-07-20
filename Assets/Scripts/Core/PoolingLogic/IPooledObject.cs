using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPooledObject
{
    void OnObjectSpawn();
    void SetPoolTag(string tag); // opzionale
}