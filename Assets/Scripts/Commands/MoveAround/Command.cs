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
        public class Command
        {
            public string horizontalInputName { get; set; }
            public string verticalInputName { get; set; }
            public float movementSpeed { get; set; }
            public CharacterController characterController { get; set; }

            public Transform transform { get; set; }
        }
    }
}
