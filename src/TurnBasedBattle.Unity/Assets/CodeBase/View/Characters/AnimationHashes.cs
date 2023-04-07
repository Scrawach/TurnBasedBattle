using UnityEngine;

namespace CodeBase.View.Characters
{
    public static class AnimationHashes
    {
        public static readonly int IdleHash = Animator.StringToHash("Idle");
        public static readonly int WalkHash = Animator.StringToHash("Walk");
        public static readonly int RunHash = Animator.StringToHash("Run");
        
        public static readonly int HitHash = Animator.StringToHash("Hit");
        public static readonly int DieHash = Animator.StringToHash("Die");
        public static readonly int DieRecoveryHash = Animator.StringToHash("DieRecovery");
        
        public static readonly int Attack01Hash = Animator.StringToHash("Attack01");
        public static readonly int Attack02Hash = Animator.StringToHash("Attack02");

        public static readonly int DefendHash = Animator.StringToHash("Defend");
        public static readonly int DizzyHash = Animator.StringToHash("Dizzy");
    }
}