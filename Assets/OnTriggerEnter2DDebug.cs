using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerEnter2DDebug : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D");
    }
}
