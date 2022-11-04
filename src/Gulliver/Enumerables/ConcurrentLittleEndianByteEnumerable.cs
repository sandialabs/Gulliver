using System.Collections.Generic;
using JetBrains.Annotations;

namespace Gulliver.Enumerables
{
    /// <summary>
    ///     an enumerable used to iterate concurrently over two <see cref="LittleEndianByteEnumerable" />
    /// </summary>
    public class ConcurrentLittleEndianByteEnumerable : AbstractConcurrentByteEnumerable
    {
        /// <inheritdoc />
        public ConcurrentLittleEndianByteEnumerable(
            [NotNull] LittleEndianByteEnumerable left,
            [NotNull] LittleEndianByteEnumerable right
        ) : base(left, right) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConcurrentLittleEndianByteEnumerable"/> class.
        /// </summary>
        /// <param name="left">bytes in little endian order for left enumerable</param>
        /// <param name="right">bytes in little endian order for right enumerable</param>
        /// <param name="trim"><see langword="true" /> if most significant zero bytes should be trimmed on instantiation</param>
        public ConcurrentLittleEndianByteEnumerable(
            [NotNull] IEnumerable<byte> left,
            [NotNull] IEnumerable<byte> right,
            bool trim = true
        ) : base(new LittleEndianByteEnumerable(left, trim), new LittleEndianByteEnumerable(right, trim)) { }

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
    }
}
