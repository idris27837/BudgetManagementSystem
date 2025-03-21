using System.Reflection;

namespace System.Linq.Expressions
{
    /// <summary>
    /// Class LambdaExpressionExtensions.
    /// </summary>
    public static class LambdaExpressionExtensions
    {
        /// <summary>
        /// Converts to propertyinfo.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>PropertyInfo.</returns>
        public static PropertyInfo ToPropertyInfo(this LambdaExpression expression)
        {
            var memberExpression = expression.Body as MemberExpression;
            return memberExpression.Member as PropertyInfo;
        }
    }
}
