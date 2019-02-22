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
        

        private float xAxisClamp;

        private void Awake()
        {
            LockCursor();
            xAxisClamp = 0.0f;
        }


        private void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            var a = new LookAround.CommandHandler();
            a.Execute(new LookAround.Command
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
