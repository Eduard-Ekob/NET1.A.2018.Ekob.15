using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day15
{
    public class CustomQueue<T> : IEnumerable<T>
    {
        private T[] queueArray;
        private int head;
        private int tail;
        private int length;
        private int queueSize;
        private Object locker = new object();

        public bool IsEmpty => length == 0;

        public bool IsFull => length == queueSize;

        public int Count => queueSize;

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        private int Version { get; set; }

        public CustomQueue(int queueSize)
        {
            if (queueSize <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    $"{nameof(queueSize)}: Size of queueArray should be greater than zero");
            }

            queueArray = new T[queueSize];
            this.queueSize = queueSize;
            head = queueSize - 1;
        }


        public void Enqueue(T Add)
        {
            lock (locker)
            {
                head = NextPosition(head);
                queueArray[head] = Add;
                if (IsFull)
                {
                    tail = NextPosition(tail);
                }

                else
                {
                    length++;
                }
            }
        }

        public T Dequeue()
        {
            lock (locker)
            {
                if (IsEmpty)
                {
                    throw new InvalidOperationException("Queue is empty");
                }

                T dequeued = queueArray[tail];
                tail = NextPosition(tail);
                length--;
                return dequeued;
            }
        }

        /// <summary>
        /// Method for return head of queueArray
        /// </summary>
        /// <returns>An object head of queueArray</returns>
        public T Peek()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("Queue is empty");
            }

            return queueArray[head];
        }

        private int NextPosition(int position) => (position + 1) % queueSize;


        /// <summary>
        /// Returns an enumerator that iterates through the queue
        /// </summary>
        /// <returns>Instance of structure for iterating</returns>
        public IEnumerator<T> GetEnumerator() => new CustomIterator(this);

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();

        /// <summary>
        /// Struct that contains CustomIterator
        /// </summary>
        public struct CustomIterator : IEnumerator<T>
        {
            private readonly CustomQueue<T> queue;
            private int currentIndex;

            public CustomIterator(CustomQueue<T> collection)
            {
                currentIndex = -1;
                queue = collection;
            }

            /// <summary>
            /// Current position enumerator
            /// </summary>
            public T Current
            {
                get
                {
                    if (currentIndex == -1 || currentIndex == queue.Count)
                    {
                        throw new InvalidOperationException();
                    }

                    return queue.queueArray[currentIndex];
                }
            }

            object IEnumerator.Current => Current;

            /// <summary>
            /// Reset queue enumerator
            /// </summary>
            public void Reset() => currentIndex = -1;

            
            /// <summary>
            /// Next step enumerator
            /// </summary>
            /// <returns>True if next element is present </returns>
            public bool MoveNext() => ++currentIndex < queue.Count;

            public void Dispose() { }

        }
    }

}
