using Rubberduck.VBEditor.SafeComWrappers.Abstract;

namespace Rubberduck.VBEditor.SafeComWrappers
{
    public abstract class SafeComWrapper<T> : ISafeComWrapper<T>
        where T : class
    {
        protected SafeComWrapper(T target)
        {
            _target = target;
        }

        public abstract void Release();

        private readonly T _target;
        public bool IsWrappingNullReference { get { return _target == null; } }
        object INullObjectWrapper.Target { get { return _target; } }
        public T Target { get { return _target; } }

        /// <summary>
        /// <c>true</c> when wrapping a <c>null</c> reference and <see cref="other"/> is either <c>null</c> or wrapping a <c>null</c> reference.
        /// </summary>
        protected bool IsEqualIfNull(ISafeComWrapper<T> other)
        {
            return (other == null || other.IsWrappingNullReference) && IsWrappingNullReference;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ISafeComWrapper<T>);
        }

        public abstract bool Equals(ISafeComWrapper<T> other);

        public static bool operator ==(SafeComWrapper<T> a, SafeComWrapper<T> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            {
                return true;
            }
            return !ReferenceEquals(a, null) && a.Equals(b);
        }

        public static bool operator !=(SafeComWrapper<T> a, SafeComWrapper<T> b)
        {
            return !(a == b);
        }
   }
}