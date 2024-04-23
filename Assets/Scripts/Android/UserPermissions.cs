using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class UserPermissions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Permission.RequestUserPermission(Permission.Camera);
        Permission.RequestUserPermission(Permission.ExternalStorageRead);
        Permission.RequestUserPermission(Permission.ExternalStorageWrite);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
