using FSM_Dotnet.Interfaces;
using FSM_Dotnet.Validation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FSM_Dotnet.Validation
{
    public class EntityValidatorsProvider<Entity> where Entity : IEntityWithState
    {
        private readonly Dictionary<PropertyInfo, EntityPropertyValidator<Entity>> _propertyValidators;
        public EntityValidatorsProvider()
        {
            _propertyValidators = new Dictionary<PropertyInfo, EntityPropertyValidator<Entity>>();
        }

        public async Task<bool> ValidateEntity(Entity entity)
        {
            foreach (var property in typeof(Entity).GetProperties())
            {
                if (_propertyValidators.TryGetValue(property, out var validator))
                {
                    var target = validator.Invokable;

                    if ( target == null)
                    {
                        continue;
                    }

                    var result = await target.Invoke(entity);

                    if ( result == false )
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public Task<bool> ValidateField(Entity entity, string field)
        {
            var propInfo = typeof(Entity).GetProperty(field);

            if ( propInfo == null)
            {
                throw new Exception(""); //TODO
            }

            if (!_propertyValidators.TryGetValue(propInfo, out var validator))
            {
                return Task.FromResult(true);
            }

            return validator.Invokable?.Invoke(entity) ?? Task.FromResult(true);
        }

    }
}
