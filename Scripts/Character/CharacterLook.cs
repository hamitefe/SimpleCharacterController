using System;
using UnityEngine;


namespace HamitEfe.SCC
{
    public class CharacterLook : MonoBehaviour
    {
        private ICharacterInputProvider inputProvider;

        public Vector3 rotation = Vector3.zero;
        public Vector3 sensitivity;

        public Transform cameraRoot;

        private void Awake()
        {
            inputProvider = GetComponent<ICharacterInputProvider>();
            this.rotation = transform.eulerAngles;
        }

        private void Update()
        {
            Vector3 look = Vector3.Scale(inputProvider.Look, sensitivity);
            rotation += look * Time.deltaTime;
            rotation.x = Mathf.Clamp(rotation.x, -89.9f, 89.9f);
            transform.localRotation = Quaternion.AngleAxis(rotation.y, Vector3.up);
            cameraRoot.localRotation = Quaternion.AngleAxis(rotation.x, Vector3.right);

        }
    }
}