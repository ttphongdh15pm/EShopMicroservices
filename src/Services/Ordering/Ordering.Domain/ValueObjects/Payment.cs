﻿namespace Ordering.Domain.ValueObjects
{
    public record Payment
    {
        protected Payment() { }
        private Payment(string cardName, string cardNumber, string expiration, string cVV, int paymentMethod)
        {
            CardName = cardName;
            CardNumber = cardNumber;
            Expiration = expiration;
            CVV = cVV;
            PaymentMethod = paymentMethod;
        }

        public string CardName { get; } = default!;
        public string CardNumber { get; } = default!;
        public string Expiration { get; } = default!;
        public string CVV { get; } = default!;
        public int PaymentMethod { get; } = default!;

        public static Payment Of(string cardName, string cardNumber, string expiration, string cVV, int paymentMethod)
        {
            ArgumentException.ThrowIfNullOrEmpty(cardName, nameof(cardName));
            ArgumentException.ThrowIfNullOrEmpty(cardNumber, nameof(cardNumber));
            ArgumentException.ThrowIfNullOrEmpty(cVV, nameof(cVV));
            ArgumentOutOfRangeException.ThrowIfNotEqual(cVV.Length, 3);
            return new Payment(cardName, cardNumber, expiration, cVV, paymentMethod);
        }
    }
}