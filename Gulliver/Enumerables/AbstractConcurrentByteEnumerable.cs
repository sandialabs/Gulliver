using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Gulliver.Enumerables
{
    /// <inheritdoc />
    /// <summary>
    ///     <see langword="abstract" /> <see cref="IConcurrentByteEnumerable" /> implementation
    /// </summary>
    public abstract class AbstractConcurrentByteEnumerable : IConcurrentByteEnumerable
    {
        /// <inheritdoc />
        protected AbstractConcurrentByteEnumerable([NotNull] IByteEnumerable left,
                                                   [NotNull] IByteEnumerable right)
        {
            this.Left = left ?? throw new ArgumentNullException(nameof(left));
            this.Right = right ?? throw new ArgumentNullException(nameof(right));

            if (left.IsLittleEndian ^ right.IsLittleEndian)
            {
                throw new ArgumentException($"the endianness of {nameof(left)} and {nameof(right)} must agree");
            }
        }

        /// <inheritdoc />
        public IByteEnumerable Left { get; set; }

        /// <inheritdoc />
        public IByteEnumerable Right { get; set; }

        /// <inheritdoc />
        public int Count => Math.Max(this.Left.Count, this.Right.Count);

        /// <inheritdoc />
        public bool IsLittleEndian => this.Left.IsLittleEndian; // endianness must match so checking left is sufficient

        /// <inheritdoc />
        public bool IsMutuallyTrimmed => this.IsLeftTrimmed && this.IsRightTrimmed;

        /// <inheritdoc />
        public bool IsLeftTrimmed => this.Left.IsTrimmed;

        /// <inheritdoc />
        public bool IsRightTrimmed => this.Right.IsTrimmed;

        /// <inheritdoc />
        public int MaxCount => this.Count;

        /// <inheritdoc />
        public int MinCount => Math.Min(this.Left.Count, this.Right.Count);

        #region From Interface IConcurrentByteEnumerable

        /// <inheritdoc />
        public IEnumerator<(byte leftByte, byte rightByte)> GetReverseEnumerator()
        {
            return this.ReverseEnumerable()
                       .GetEnumerator();
        }

        /// <inheritdoc />
        public abstract IEnumerable<(byte leftByte, byte rightByte)> ReverseEnumerable();

        /// <inheritdoc />
        public IEnumerable<(byte leftByte, byte rightByte)> GetMsbToLsbEnumerable()
        {
            return this.IsLittleEndian
                       ? this.ReverseEnumerable()
                       : this;
        }

        /// <inheritdoc />
        public IEnumerable<(byte leftByte, byte rightByte)> GetLsbToMsbEnumerable()
        {
            return this.IsLittleEndian
                       ? this
                       : this.ReverseEnumerable();
        }

        #endregion

        #region From Interface IEnumerable

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region From Interface IEnumerable<(byte leftByte, byte rightByte)>

        /// <inheritdoc />
        public abstract IEnumerator<(byte leftByte, byte rightByte)> GetEnumerator();

        #endregion
    }
}
