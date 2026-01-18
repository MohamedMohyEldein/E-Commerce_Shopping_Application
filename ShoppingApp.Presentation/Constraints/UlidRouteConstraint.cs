namespace ShoppingApp.Presentation.Constraints
{
    public class UlidRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (values.ContainsKey(routeKey))
            {
                var stringValue = values[routeKey]!.ToString();
                if (string.IsNullOrWhiteSpace(stringValue))
                {
                    return false;
                }

                if (stringValue.Length != 26)
                {
                    return false;
                }

                return Ulid.TryParse(stringValue, out _);
            }

            return false;
        }
    }
}

