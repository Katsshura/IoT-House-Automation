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
            return config.ExpirationType switch
            {
                ExpirationType.Minutes => DateTime.UtcNow.AddMinutes(config.ExpirationTime),
                ExpirationType.Hours => DateTime.UtcNow.AddHours(config.ExpirationTime),
                ExpirationType.Days => DateTime.UtcNow.AddDays(config.ExpirationTime),
                ExpirationType.Weeks => DateTime.UtcNow.AddDays(config.ExpirationTime * 7),
                ExpirationType.Months => DateTime.UtcNow.AddMonths(config.ExpirationTime),
                ExpirationType.Years => DateTime.UtcNow.AddYears(config.ExpirationTime),
                _ => throw new NotSupportedException("Expiration time for Jwt Token is invalid")
            };
        }
    }
}
