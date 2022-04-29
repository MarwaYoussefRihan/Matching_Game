using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Add_12_Buttons : MonoBehaviour
{

    [SerializeField]
    private Transform puzzelField; 

    [SerializeField]
    private GameObject button;

    private void Awake()
    {
        for(int i = 0; i < 12 ; i++)
        {
            GameObject _button = Instantiate(button);
            _button.name = "" + i ;
            _button.transform.SetParent(puzzelField, false);
        }
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

