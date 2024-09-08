using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Button[] buttons; // Menüdeki butonlar
    private int selectedIndex = 0; // Seçili butonun indexi

    void Start()
    {
        if (buttons.Length > 0)
        {
            UpdateSelection(); // Başlangıçta ilk butonu seçili yap
        }
    }

    void Update()
    {
        if (Keyboard.current.upArrowKey.wasPressedThisFrame || Gamepad.current.dpad.up.wasPressedThisFrame)
        {
            MoveSelection(-1);
        }
        else if (Keyboard.current.downArrowKey.wasPressedThisFrame || Gamepad.current.dpad.down.wasPressedThisFrame)
        {
            MoveSelection(1);
        }

        if (Keyboard.current.enterKey.wasPressedThisFrame || Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            ActivateSelectedButton();
        }
    }

    void MoveSelection(int direction)
    {
        selectedIndex = Mathf.Clamp(selectedIndex + direction, 0, buttons.Length - 1);
        UpdateSelection();
    }

    void UpdateSelection()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            Button button = buttons[i];
            Animator animator = button.GetComponent<Animator>();

            if (animator != null)
            {
                if (i == selectedIndex)
                {
                    // Seçili butonun animasyonunu tetikle
                    animator.SetTrigger("Highlighted");
                }
                else
                {
                    // Seçili olmayan butonların animasyonunu durdur veya normal duruma getir
                    animator.SetTrigger("Normal");
                }
            }
        }
    }

    void ActivateSelectedButton()
    {
        buttons[selectedIndex].onClick.Invoke(); // Seçili butona tıklama
    }
}
