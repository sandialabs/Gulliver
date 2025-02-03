using System.Collections.Generic;

namespace Gulliver.Enumerables
{
    /// <summary>
    ///     an enumerable used to iterate concurrently over two <see cref="BigEndianByteEnumerable" />
    /// </summary>
    public class ConcurrentBigEndianByteEnumerable : AbstractConcurrentByteEnumerable
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ConcurrentBigEndianByteEnumerable"/> class.
        /// </summary>
        /// <param name="left">the left side operand</param>
        /// <param name="right">the right side operand</param>
        public ConcurrentBigEndianByteEnumerable(BigEndianByteEnumerable left, BigEndianByteEnumerable right)
            : base(left, right) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentBigEndianByteEnumerable"/> class.
        /// </summary>
        /// <param name="left">bytes in big endian order for left enumerable</param>
        /// <param name="right">bytes in big endian order for right enumerable</param>
        /// <param name="trim"><see langword="true" /> if most significant zero bytes should be trimmed on instantiation</param>
        public ConcurrentBigEndianByteEnumerable(IEnumerable<byte> left, IEnumerable<byte> right, bool trim = true)
            : base(new BigEndianByteEnumerable(left, trim), new BigEndianByteEnumerable(right, trim)) { }

        //#endregion

        /// <inheritdoc />
        public override IEnumerable<(byte leftByte, byte rightByte)> ReverseEnumerable()
        {
            var leftCount = Left.Count;
            var rightCount = Right.Count;
            var count = Count;

            using (var leftEnumerator = Left.GetReverseEnumerator()) // enumerate in reverse
            {
                using (var rightEnumerator = Right.GetReverseEnumerator())
                {
                    for (var i = count - 1; i >= 0; i--)
                    {
                        byte leftByte = 0x00;
                        byte rightByte = 0x00;

                        if (i + leftCount >= count)
                        {
                            leftEnumerator.MoveNext();
                            leftByte = leftEnumerator.Current;
                        }

                        if (i + rightCount >= count)
                        {
                            rightEnumerator.MoveNext();
                            rightByte = rightEnumerator.Current;
                        }

                        yield return (leftByte, rightByte);
                    }
                }
            }
        }

        /// <inheritdoc />
        public override IEnumerator<(byte leftByte, byte rightByte)> GetEnumerator()
        {
            var leftCount = Left.Count;
            var rightCount = Right.Count;
            var count = Count;

            using (var leftEnumerator = Left.GetEnumerator())
            {
                using (var rightEnumerator = Right.GetEnumerator())
                {
                    for (var i = 0; i < count; i++)
                    {
                        byte leftByte = 0x00;
                        byte rightByte = 0x00;

                        if (i + leftCount >= count)
                        {
                            leftEnumerator.MoveNext();
                            leftByte = leftEnumerator.Current;
                        }

                        if (i + rightCount >= count)
                        {
                            rightEnumerator.MoveNext();
                            rightByte = rightEnumerator.Current;
                        }

                        yield return (leftByte, rightByte);
                    }
                }
            }
        }
    }
}
