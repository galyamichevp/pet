using System.Collections;
using System.Collections.Generic;

namespace console.Enumerations.Example1
{
    public static partial class MyEnumerable
    {
        public static EmptyEnumerableStruct<T> EmptyStruct<T>() => new EmptyEnumerableStruct<T>();

        public static EmptyEnumerableDisposableStruct<T> EmptyDisposableStruct<T>() => new EmptyEnumerableDisposableStruct<T>();

        public static IEnumerable<T> EmptyClass<T>() => EmptyEnumerableClass<T>.Instance;

        public struct EmptyEnumerableStruct<T>
        {
            public EmptyEnumerableStruct<T> GetEnumerator() => this;

            public T Current => default(T);

            public bool MoveNext() => false;
        }

        public struct EmptyEnumerableDisposableStruct<T> : IEnumerable<T>, IEnumerator<T>
        {
            public T Current => default(T);

            object IEnumerator.Current => default(T);

            public void Dispose()
            {
                // throw new System.NotImplementedException();
            }

            public IEnumerator<T> GetEnumerator() => this;

            public bool MoveNext() => false;

            public void Reset()
            {
                // throw new System.NotImplementedException();
            }

            IEnumerator IEnumerable.GetEnumerator() => this;
        }

        sealed class EmptyEnumerableClass<T> : IEnumerable<T>, IEnumerator<T>
        {
            static readonly EmptyEnumerableClass<T> instance = new EmptyEnumerableClass<T>();
            private EmptyEnumerableClass() { /* nothing to do */ }
            public static EmptyEnumerableClass<T> Instance => instance;

            public IEnumerator<T> GetEnumerator() => this;
            IEnumerator IEnumerable.GetEnumerator() => this;

            public T Current => default(T);
            object IEnumerator.Current => default(T);

            public bool MoveNext() => false;
            public void Reset() { /* nothing to do */ }
            public void Dispose() { /* nothing to do */ }
        }

        public static IEnumerable<T> EmptyYieldBreak<T>()
        {
            yield break;
        }
    }
}