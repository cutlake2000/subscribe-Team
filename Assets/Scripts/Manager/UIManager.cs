using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManger : MonoBehaviour
{
    public static UIManger Instance;

    private void Awake()
    {
        Instance = this;
    }
}
