using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Commands.MoveAround
{
    public partial class MoveAround
    {
        public class CommandHandler
        {
            public void Handle(Command command)
            {
                float horizInput = Input.GetAxis(command.horizontalInputName) * command.movementSpeed;
                float vertInput = Input.GetAxis(command.verticalInputName) * command.movementSpeed;

                Vector3 forwardMovement = command.transform.forward * vertInput;
                Vector3 rightMovement = command.transform.right * horizInput;

                command.characterController.SimpleMove(forwardMovement + rightMovement);
            }
        }
    }
}
