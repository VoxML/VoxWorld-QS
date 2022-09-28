using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoBalancer : MonoBehaviour
{
    [SerializeField] private Scale scale;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (scale.Tip() == 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, 0f), 2f * Time.deltaTime);
        } else {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, 15f * scale.Tip()), 2f * Time.deltaTime);
        }
    }
}
