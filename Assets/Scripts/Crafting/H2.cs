using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class H2 : BaseElement
{

public void OnMouseDown()
    {
        AddToPot();
    }

    protected override void Start()
    {
        base.Start();
        Button btn = GetComponent<Button>();
        if (btn != null)
        btn.onClick.AddListener(OnMouseDown);
    }

    void Update()
    {
        
    }
}
