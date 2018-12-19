using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Day15;

namespace CustomQueueTests
{
    [TestFixture]
    public class CustomQueueUnitTests
    {
        [TestCase(new int[] {143, 764, 492, 374, 1111})]
        public void CustomQueue_EnqueueDequeueTests_WithInt(int[] values)
        {
            var queue = new CustomQueue<int>(values.Length);
            QEnqueue(queue, values);
            for (int i = 0; i < values.Length; i++)
            {
                Assert.AreEqual(values[i], queue.Dequeue());
            }
        }

        [TestCase(new int[] {143, 764, 492, 374, 1111})]
        public void GetEnumeratorTests(int[] values)
        {
            var queue = new CustomQueue<int>(values.Length);
            QEnqueue(queue, values);
            int i = 0;
            foreach (var q in queue)
            {
                Assert.AreEqual(q, values[i++]);
            }
        }

        [TestCase("Hello", "My", "New", "World", "!")]
        public void CustomQueue_EnqueueDequeueTests_WithString(params string[] values)
        {
            var queue = new CustomQueue<string>(values.Length);
            QEnqueue(queue, values);
            int i = 0;
            foreach (var q in queue)
            {
                Assert.AreEqual(q, values[i++]);
            }
        }

        private void QEnqueue<T>(CustomQueue<T> queue, T[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                queue.Enqueue(values[i]);
            }
        }

        [Test]
        public void QueueCapacityInitializationZeroOrLess()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new CustomQueue<int>(0));
        }
    }
}
