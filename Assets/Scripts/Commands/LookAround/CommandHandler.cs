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
            public void Execute(Command command)
            {
                float mouseX = Input.GetAxis(command.mouseXInputName) * command.mouseSensitivity * Time.deltaTime;
                float mouseY = Input.GetAxis(command.mouseYInputName) * command.mouseSensitivity * Time.deltaTime;

                command.transform.Rotate(Vector3.left * mouseY);
                command.playerBody.Rotate(Vector3.up * mouseX);
            }
        }
    }
}
