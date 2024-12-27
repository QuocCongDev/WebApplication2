using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Collections;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
   
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
       
        [HttpPost("input/hardmodel")]
        public WeatherForecast CheckValidate(DomainRenewUpdateRequestHubDto obj)
        {
            
            var check = ValidateDomainName(obj);
            return new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = "test",
                IsValid = check,
            };
        }
        [HttpPost("input/easymodel")]
        public WeatherForecast CheckValidate2(ModelAbc obj)
        {

            var check = ValidateDomainName(obj);
            return new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = "testeasy",
                IsValid = check,
            };
        }

        private readonly List<string> SPECIAL_PROPERTIES = ["domainname","domainnames","domainnameml","domainamemls"];

        private bool ValidateDomainName(object obj)
        {
            bool isValid = true;
            var myClassType  = obj.GetType();
            PropertyInfo[] propertyInfo = myClassType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            // Begin : Get all properties is primitive type and validate .
            var getNaturalProperty = propertyInfo.Where(x => SPECIAL_PROPERTIES.Any(prop => prop == x.Name.ToLower()) && (x.PropertyType == typeof(string) || x.PropertyType == typeof(List<string>)));
            if(getNaturalProperty.Any())
            {
                foreach (var property in getNaturalProperty)
                {
                   if(property.PropertyType == typeof(string))
                    {
                        var str = property.GetValue(obj, null)?.ToString() ?? string.Empty;
                        var arrs = str.Split(',');
                        if (arrs.Any(x => !regexMatch(x)))
                        {
                            isValid = false;
                            break;
                        }
                    }

                   if(property.PropertyType == typeof(List<string>))
                    {
                        List<string> arrs = property.GetValue(obj, null) as List<string> ??  [];
                        if(arrs.Any(x => !regexMatch(x)))
                        {
                            isValid = false;
                            break;
                        }
                    }
                }

            }
            if (!isValid) return isValid;
            //End : validate primitive type.
            //Begin : validate refference type.
            var objectProp = propertyInfo.Where(x => !SPECIAL_PROPERTIES.Any(prop => prop == x.Name.ToLower()) && x.PropertyType != typeof(List<string>) && ( x.PropertyType.IsClass || x.PropertyType.IsGenericType));
            if (objectProp.Any())
            {
                foreach (var property in objectProp)
                {
                    object oop = property.GetValue(obj, null);
                    if (oop == null) { continue; }

                    if (isList(oop)){
                        IList collection = (IList)oop;
                        foreach(var co in collection)
                        {
                            isValid = ValidateDomainName(co);
                            if (!isValid) break;
                        }
                        if (!isValid) break;
                    }
                    else
                    {
                       isValid = ValidateDomainName(oop);
                       if (!isValid) break;
                    }

                }
            }

            //End : validate reffrence type.
            return isValid;
        }

        // pattern default
        private  string rDomainPattern = "^(?!\\s)+[0-9０-９a-zａ-ｚA-ZＡ-Ｚぁ-んァ-ヶｦ-ﾟ一-龍-.*\n・ヽヾゝゞゞ々-ー]*$";
        
        /// <summary>
        /// regex match
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool regexMatch(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return true;
            }
            return Regex.IsMatch(value, rDomainPattern, RegexOptions.ECMAScript);
        }

        /// <summary>
        /// Check object type is List 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private bool isList(object obj)
        {
            if (obj == null) return false;
            return obj is IList &&
                   obj.GetType().IsGenericType &&
                   obj.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>));
        }

    }
}
