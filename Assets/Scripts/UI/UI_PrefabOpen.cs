using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PrefabOpen : MonoBehaviour
{
    public GameObject prefabToOpen;

    private void OnMouseDown()
    {
        if (prefabToOpen != null)
        {
            GameObject instantiatedPrefab = Instantiate(prefabToOpen, transform.position, transform.rotation);
        }
    }
}