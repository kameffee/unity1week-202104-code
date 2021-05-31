using UnityEngine;

namespace kameffee.unity1week202104.View
{
    public class PlayerInput : IPlayerInput
    {
        public float GetHorizontal() => Input.GetAxis("Horizontal");

        public float GetVertical() => Input.GetAxis("Vertical");

        public bool GetAction() => Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.E);

        public bool Jump() => Input.GetButtonDown("Jump");
    }
}