using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleItem : MonoBehaviour, IPickupable
{
    public GameObject Backpack;
    public GameObject Backpack_IMG;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// This is where you will want to add your own implementation for your own systems.
    /// </summary>
    public void Pickup()
    {
        Backpack.SetActive(true);
        Backpack_IMG.SetActive(true);
        Destroy(this.gameObject);
    }
}
