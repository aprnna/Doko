using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class UserPermission : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Permission.RequestUserPermission(Permission.Camera);
    }
}
