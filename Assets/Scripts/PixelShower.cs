using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelShower : MonoBehaviour
{
    [SerializeField] private GameObject canvas;

    private void Start()
    {
        canvas.SetActive(true);
    }
}
