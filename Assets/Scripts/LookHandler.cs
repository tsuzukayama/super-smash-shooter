using Assets.Scripts.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class LookHandler : MonoBehaviour
    {
        [SerializeField] private string mouseXInputName, mouseYInputName;
        [SerializeField] private float mouseSensitivity;        

        [SerializeField] private Transform playerBody;

        private bool showMenu = false;
        private LookAround.CommandHandler lookAround;
        private float xAxisClamp;

        private void Awake()
        {            
            xAxisClamp = 0.0f;
            lookAround = new LookAround.CommandHandler();            
        }


        private void LockCursor()
        {
            if (!showMenu)
                Cursor.lockState = CursorLockMode.Locked;
            else Cursor.lockState = CursorLockMode.None;
        }

        private void Update()
        {
            LockCursor();
            if (Input.GetKeyDown(KeyCode.M))
            {
                showMenu = !showMenu;
            }            
            lookAround.Handle(new LookAround.Command
            {
                mouseSensitivity = mouseSensitivity,
                mouseXInputName = mouseXInputName,
                mouseYInputName = mouseYInputName,
                playerBody = playerBody,
                transform = transform
            });
        }

    }
}
