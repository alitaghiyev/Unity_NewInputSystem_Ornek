using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, InputActions.IPlayerActions
{
    public Vector2 MovementValue { get; private set; }
    public Vector2 LookValue { get; private set; }
    public bool IsAttacking { get; private set; } //su anki  durumda saldiri butona basili tutuldugu surece calisir
    public event Action JumpEvent;

    public event Action<bool> CrouchEvent;


    #region Enable and disable action object
    private InputActions action;
    private void Start()
    {
        action = new InputActions();
        action.Player.SetCallbacks(this);
        action.Player.Enable();
    }
    private void OnDestroy()
    {
        action.Player.Disable();
    }
    #endregion


    //InputAction.CallbackContext classi, InputActions nesnesinin bir eylemi gerceklestirdiginde cagrilan bir classdir.
    // InputAction.CallbackContext nesnesinden bir Vector2 degeri okur ve bunu LookValue adli filed'a atar.
    //fare inputlari direkt olarak cinemachine virual kamerada kullanila bilir
    public void OnLook(InputAction.CallbackContext context)
    {
        LookValue = context.ReadValue<Vector2>();
    }


    // InputAction.CallbackContext nesnesinden bir Vector2 degeri okur ve bunu MovementValue adli filed'a atar
    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }


    public void OnAttack(InputAction.CallbackContext context)
    {
        IsAttacking = context.ReadValueAsButton(); //InputAction.CallbackContext nesnesinden buttonun degerini okur

        //NOT: su anki durumda bu iki kullanim ayni sonuclari verecektir.
        //eger eski input sistemde bulunan GetKetyDown GetKeyUp davranislarini temsil etmek isterseniz 
        //asagidaki yontemler kullanila bilir

        // if (context.performed) // eylem tetiklendiginde calisir
        // {
        //     IsAttacking = true;
        // }
        // else if (context.canceled)// eylem iptal edildiginde calisir
        // {
        //     IsAttacking = false;
        // }
    }


    // context.performed ozelligi, bir eylemin gerceklestirilip gerceklestirilmedigini gosterir.
    // JumpEvent eventi, butona tiklandiginda tetiklenir
    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        JumpEvent?.Invoke();
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        //eski sistemdeki GetKeyDown ve GetKeyUp metodlari yerine gecer
        if (context.performed) // eylem tetiklendiginde calisir
        {
            CrouchEvent?.Invoke(true);
        }
        else if (context.canceled)// eylem iptal edildiginde calisir
        {
            CrouchEvent?.Invoke(false);
        }
    }

}
