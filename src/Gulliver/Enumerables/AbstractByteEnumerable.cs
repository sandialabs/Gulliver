using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Gulliver.Enumerables
{
    /// <summary>
    ///     <see langword="abstract" /> <see cref="IByteEnumerable" /> implementation
    /// </summary>
    [PublicAPI]
    public abstract class AbstractByteEnumerable : IByteEnumerable
    {
        /// <inheritdoc />
        protected AbstractByteEnumerable(byte[] bytes, bool isTrimmed, bool isLittleEndian)
        {
            this.Bytes = bytes ?? throw new ArgumentNullException(nameof(bytes));
            this.IsLittleEndian = isLittleEndian;
            this.IsTrimmed = isTrimmed;
        }

        /// <summary>
        ///     Gets bytes
        /// </summary>
        /// <value>
        /// Bytes
        /// </value>
        internal byte[] Bytes { get; }

        /// <inheritdoc />
        public int Count => this.Bytes.Length;

        /// <inheritdoc />
        public bool IsLittleEndian { get; }

        /// <inheritdoc />
        public bool IsTrimmed { get; }

        #region From Interface IByteEnumerable

        /// <inheritdoc />
        public IEnumerator<byte> GetReverseEnumerator()
        {
            for (var i = this.Bytes.Length - 1; i >= 0; i--)
            {
                yield return this.Bytes[i];
            }
        }

        /// <inheritdoc />
        public IEnumerable<byte> ReverseEnumerable()
        {
            using (var enumerator = this.GetReverseEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    yield return enumerator.Current;
                }
            }
        }

        #endregion

        #region From Interface IEnumerable

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region From Interface IEnumerable<byte>

        /// <inheritdoc />
        public IEnumerator<byte> GetEnumerator()
        {
            for (var i = 0; i < this.Bytes.Length; i++)
            {
                yield return this.Bytes[i];
            }
        }

        #endregion
    }
}
