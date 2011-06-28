using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.ComponentModel;

namespace ePortafolio.Helpers
{
    public static class CastHelper
    {
        public static T ExplicitCast<T>(T Destination, object Source)
        {
            var PropertyInfoDestination = Destination.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var PropertyInfoSource = Source.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo propertyInfoDestination in PropertyInfoDestination)
            {
                var propertyInfoSource = PropertyInfoSource.SingleOrDefault(x => x.Name == propertyInfoDestination.Name);

                if (propertyInfoSource != null && propertyInfoSource.CanRead && propertyInfoDestination.CanWrite)
                {
                    if (propertyInfoSource.PropertyType.FullName != propertyInfoDestination.PropertyType.FullName)
                    {

                        if (GetUnderlyingType(propertyInfoSource.PropertyType).FullName == GetUnderlyingType(propertyInfoDestination.PropertyType).FullName)
                        {
                            var Value = propertyInfoSource.GetValue(Source, null);

                            if (IsNullableType(propertyInfoDestination.PropertyType) || Value!=null)
                            {
                                propertyInfoDestination.SetValue(Destination, Value, null);
                                continue;
                            }
                        }

                        try
                        {
                            var ValueSource = propertyInfoSource.GetValue(Source, null);
                            if (ValueSource != null)
                            {
                                var NewInstance = propertyInfoDestination.PropertyType.GetConstructor(System.Type.EmptyTypes).Invoke(null);
                                var ValueCast = ExplicitCast(NewInstance, ValueSource);
                                propertyInfoDestination.SetValue(Destination, ValueCast, null);
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }

                    if (propertyInfoSource.PropertyType.FullName == propertyInfoDestination.PropertyType.FullName)
                    {
                        var Value = propertyInfoSource.GetValue(Source, null);
                        propertyInfoDestination.SetValue(Destination, Value, null);
                    }
                }
            }
            return Destination;
        }
        
        private static bool IsNullableType(Type theType)
        {
            return (theType.IsGenericType && theType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)));
        }

        private static Type GetUnderlyingType(Type theType)
        {
            return IsNullableType(theType) ? new NullableConverter(theType).UnderlyingType : theType;
        }
    }
}