using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MenuDG : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        transform.DOShakePosition(1000, 0.2f, 1, 10, false, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
