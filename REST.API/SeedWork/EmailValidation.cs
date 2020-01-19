using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace REST.API.SeedWork
{
    /// <summary>
    /// Email validation attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
        AllowMultiple = false)]
    public class EmailValidation : ValidationAttribute
    {
        public EmailValidation() : base("Email address is incorrect")
        {
        }

        public override bool IsValid(object value)
        {
            // Automatically pass if value is null. RequiredAttribute should be used to assert a value is not null.
            if (value == null)
                return true;

            if (!(value is string str))
                throw new InvalidCastException("Wrong type in email attribute - should be string");


            var emailAddress = str;

            if (string.IsNullOrEmpty(emailAddress))
                return false;

            try
            {
                var m = new MailAddress(emailAddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}