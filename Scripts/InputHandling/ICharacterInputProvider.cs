using UnityEngine;

namespace HamitEfe.SCC
{
    public interface ICharacterInputProvider
    {
        public Vector3 Look { get; }
        public Vector3 Movement { get; }
        public bool IsJumping { get; }

    }
}