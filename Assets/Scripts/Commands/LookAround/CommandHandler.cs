using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Commands
{
    public partial class LookAround
    {
        public class CommandHandler
        {
            private float xAxisClamp = 0;
            public void Handle(Command command)
            {
                float mouseX = Input.GetAxis(command.mouseXInputName) * command.mouseSensitivity * Time.deltaTime;
                float mouseY = Input.GetAxis(command.mouseYInputName) * command.mouseSensitivity * Time.deltaTime;

                xAxisClamp += mouseY;

                if (xAxisClamp > 90.0f)
                {
                    xAxisClamp = 90.0f;
                    mouseY = 0.0f;
                    ClampXAxisRotationToValue(270.0f, command.transform);
                }
                else if (xAxisClamp < -90.0f)
                {
                    xAxisClamp = -90.0f;
                    mouseY = 0.0f;
                    ClampXAxisRotationToValue(90.0f, command.transform);
                }

                command.transform.Rotate(Vector3.left * mouseY);
                command.playerBody.Rotate(Vector3.up * mouseX);
            }
            private void ClampXAxisRotationToValue(float value, Transform transform)
            {
                Vector3 eulerRotation = transform.eulerAngles;
                eulerRotation.x = value;
                transform.eulerAngles = eulerRotation;
            }
        }
    }
}
