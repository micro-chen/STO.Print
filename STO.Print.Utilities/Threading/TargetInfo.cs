using System;

namespace STO.Print.Utilities.Threading
{
    /// <summary>
    /// Used internally to call delegate in Thread Pool
    /// </summary>
    internal class TargetInfo
    {
        public readonly Delegate Target;
        public readonly object[] Arguments;

        /// <summary>
        /// Initializes a new instance of the <see cref="TargetInfo"/> class.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="args">The args.</param>
        public TargetInfo(Delegate target, params object[] args)
        {
            Target = target;
            Arguments = args;
        }

    }
}
