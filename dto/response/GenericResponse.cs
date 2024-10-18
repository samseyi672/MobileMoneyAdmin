using System;
using System.ComponentModel;

namespace AccountAndTransactions.dto.response
{

    public class GenericResponse : ICloneable
    {
        public bool Success { get; set; }
        public EnumResponse Response { get; set; }
        public string ResponseMessage => Response.GetDescription();
        public string Message { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    public class PrimeAdminResponse : GenericResponse  // for the admin response
    {
        public object Data { set; get; }
    }
    public enum EnumResponse
    {
        [Description("No Transactions Found")]
        NoTransactionFound,
        [Description("No Customer Found")]
        CustomerError,
        [Description("No account Found")]
        AccountError,
        [Description("Error processing transaction")]
        Account,
        SystemError,
        [Description("successful")]
        Successful
    }
    public static class EnumExtensions
    {
        public static string GetDescription<T>(this T enumerationValue)
        {
            var type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException($"{nameof(enumerationValue)} must be of Enum type", nameof(enumerationValue));
            }
            var memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo.Length > 0)
            {
                var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            return enumerationValue.ToString();
        }
    }
}
