using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public Canvas GuiCanvas;
    public Animator MusicPlayBarAnimator;
    public Animator CloseBtnAnimator;

    bool showGui = true;
    // Start is called before the first frame update
    void Start()
    {
        SwitchGui(true);
    }

    float lastDownTime;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleGui();
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time - lastDownTime < 0.5f)
            {
                ToggleGui();
            }

            lastDownTime = Time.time;
        }
    }


    void SwitchGui(bool state)
    {
        CloseBtnAnimator.SetBool("ShowGui", state);
        MusicPlayBarAnimator.SetBool("ShowGui", state);
    }
    void ToggleGui()
    {
        CloseBtnAnimator.SetBool("ShowGui", !showGui);
        MusicPlayBarAnimator.SetBool("ShowGui", !showGui);

        showGui = !showGui;
    }

    public void QuickApp()
    {
        Application.Quit();
    }
}
