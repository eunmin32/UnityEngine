using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainUIController : MonoBehaviour
{
    int wallbrokenCount = 0;
    public Text wallBrokeCountText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void brokewall()
    {
        wallbrokenCount++;
        wallBrokeCountText.text = wallbrokenCount.ToString();
    }
}
