using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyYourself : MonoBehaviour
{
    [SerializeField] private float lifeTime = 2f;
    private void Awake()
    {
        Invoke("Suicide", lifeTime);
    }

    private void Suicide()
    {
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
