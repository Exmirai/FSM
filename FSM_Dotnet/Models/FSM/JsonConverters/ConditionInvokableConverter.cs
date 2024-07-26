using FSM_Dotnet.Interfaces;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

//TODO: https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/converters-how-to?pivots=dotnet-8-0 Фабрику 

namespace FSM_Dotnet.Models.FSM.JsonConverters
{
    public class ConditionInvokableConverter : JsonConverter<IInvokable<IEntityWithState, bool>>
    {
        private readonly IFSMRuntime _runtime;
        public ConditionInvokableConverter(IFSMRuntime runtime)
        {
            _runtime = runtime;
        }

        public override IInvokable<IEntityWithState, bool>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var sourceCode = reader.GetString();

            if (string.IsNullOrEmpty(sourceCode) )
            {
                return null;
            }

            return _runtime.Compile<IEntityWithState, bool>(sourceCode);
        }

        public override void Write(Utf8JsonWriter writer, IInvokable<IEntityWithState, bool> value, JsonSerializerOptions options)
        {
            writer.WriteStringValue("not supported");
        }
    }

    public class ActionInvokableConverter : JsonConverter<IInvokable<IEntityWithState>>
    {
        private readonly IFSMRuntime _runtime;
        public ActionInvokableConverter(IFSMRuntime runtime)
        {
            _runtime = runtime;
        }

        public override IInvokable<IEntityWithState>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var sourceCode = reader.GetString();

            if (string.IsNullOrEmpty(sourceCode))
            {
                return null;
            }

            return _runtime.Compile<IEntityWithState>(sourceCode);
        }

        public override void Write(Utf8JsonWriter writer, IInvokable<IEntityWithState> value, JsonSerializerOptions options)
        {
            writer.WriteStringValue("not supported");
        }
    }
}
