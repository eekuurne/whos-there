using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CourageSlider : MonoBehaviour
{
    Slider slider;
    Character player;
    
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        player = GameObject.Find("PlayerCharacter").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = (float)player.GetHealth()/5f;
    }
}
