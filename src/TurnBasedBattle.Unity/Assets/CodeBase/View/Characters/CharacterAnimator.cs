using UnityEngine;

namespace CodeBase.View.Characters
{
    public class CharacterAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public int Play(int hash)
        {
            _animator.CrossFade(hash, 0.2f);
            return 0;
        }

        public AnimatorStateInfo GetNextStateInfo() =>
            _animator.GetNextAnimatorStateInfo(0);

        public AnimatorStateInfo GetCurrentStateInfo() =>
            _animator.GetCurrentAnimatorStateInfo(0);
    }
}