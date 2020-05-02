using System;
using System.Collections.Generic;
using System.Text;
using IoT.House.Automation.Microservices.Auth.Application.Entities.Config;
using IoT.House.Automation.Microservices.Auth.Application.Entities.Enums;

namespace IoT.House.Automation.Microservices.Auth.Application.Services.Jwt
{
    public static class JwtExpirationExchanger
    {
        public static DateTime ExchangeExpirationTime(JwtConfig config)
        {
            switch (config.ExpirationType)
            {
                case ExpirationType.Minutes:
                    return DateTime.UtcNow.AddMinutes(config.ExpirationTime);
                case ExpirationType.Hours:
                    return DateTime.UtcNow.AddHours(config.ExpirationTime);
                case ExpirationType.Days:
                    return DateTime.UtcNow.AddDays(config.ExpirationTime);
                case ExpirationType.Weeks:
                    return DateTime.UtcNow.AddDays(config.ExpirationTime * 7);
                case ExpirationType.Months:
                    return DateTime.UtcNow.AddMonths(config.ExpirationTime);
                case ExpirationType.Years:
                    return DateTime.UtcNow.AddYears(config.ExpirationTime);
                default:
                    throw new NotSupportedException("Expiration time for Jwt Token is invalid");
            }
        }
    }
}
