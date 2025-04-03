using UnityEngine;

namespace Eu4ng.Manager.Singleton
{
    public abstract class GameSubsystem<T> : MonoSingleton<T> where T : MonoSingleton<T>
    {
        public override bool IsSubsystem => true;
    }
}
