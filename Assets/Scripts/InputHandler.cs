using Assets.Scripts.Commands.MoveAround;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private string horizontalInputName;
        [SerializeField] private string verticalInputName;
        [SerializeField] private float movementSpeed;

        private CharacterController charController;

        private MoveAround.CommandHandler moveAround;

        private void Awake()
        {
            charController = GetComponent<CharacterController>();

            moveAround = new MoveAround.CommandHandler();
        }

        private void Update()
        {
            moveAround.Handle(new MoveAround.Command
            {
                characterController = charController,
                horizontalInputName = horizontalInputName,
                verticalInputName = verticalInputName,
                movementSpeed = movementSpeed,
                transform = transform
            });


        }
    }
}