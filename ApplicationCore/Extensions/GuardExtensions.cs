using ApplicationCore.Entities.BasketAggregate;
using ApplicationCore.Exceptions;

namespace ApplicationCore.Extensions
{
    public static class GuardExtensions
    {
        public static T AssertNotNull<T>(this T value, string paramName)
        {
            if (value is null)
                throw new ArgumentNullException(paramName);

            return (T)value;
        }

        public static T AssertOutOfRange<T>(this T value, string paramName, T rangeFrom, T rangeTo)
            where T : IComparable, IComparable<T>
        {
            if (rangeFrom.CompareTo(rangeTo) > 0)
            {
                throw new ArgumentException($"{nameof(rangeFrom)} should be less or equal than {nameof(rangeTo)}", paramName);
            }

            if (value.CompareTo(rangeFrom) < 0 || value.CompareTo(rangeTo) > 0)
            {
                throw new ArgumentOutOfRangeException(paramName, $"Input {paramName} was out of range");
            }

            return value;
        }

        public static string AssertNotEmpty(this string? value, string paramName)
        {
            var notNullValue = value.AssertNotNull(paramName);

            if (notNullValue == string.Empty)
                throw new ArgumentException($"Required input {paramName} was empty.", paramName);

            return notNullValue!;
        }

        public static Guid AssertNotEmpty(this Guid value, string paramName)
        {
            if (value == Guid.Empty)
                throw new ArgumentException($"Required input {paramName} was empty.", paramName);

            return value;
        }

        public static T AssertNegativeOrZero<T>(this T value, string paramName)
            where T : struct, IComparable
        {
            if (value.CompareTo(default(T)) <= 0)
            {
                throw new ArgumentException($"Required input {paramName} cannot be zero or negative.", paramName);
            }

            return value;
        }

        public static T AssertZero<T>(this T value, string paramName)
            where T : struct
        {
            // is possible to write Equals(value, default(T)
            if (EqualityComparer<T>.Default.Equals(value, default(T)))
            {
                throw new ArgumentException($"Required input {paramName} cannot be zero.", paramName);
            }

            return value;
        }

        public static void EmptyBasketOnCheckout(this IReadOnlyCollection<BasketItem> basketItems)
        {
            if (!basketItems.Any())
                throw new EmptyBasketOnCheckoutException();
        }
    }
}
