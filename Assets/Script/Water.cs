using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    private bool _isRising;

    // Start is called before the first frame update
    void Start()
    {
        _isRising = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(_isRising)
        {
            gameObject.transform.position += new Vector3(0, 1, 0) * Time.deltaTime;
        }
    }
}
