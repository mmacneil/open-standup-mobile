using System.Collections;

namespace OpenStandup.SharedKernel.Extensions
{
    public static class CollectionExtensions
    {
        /// <summary>
        ///   Checks whether or not collection is null or empty. Assumes colleciton can be safely enumerated multiple times.
        /// </summary>
        /// <param name = "this"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this IEnumerable @this)
        {
            return @this == null || @this.GetEnumerator().MoveNext() == false;
        }
    }
}
