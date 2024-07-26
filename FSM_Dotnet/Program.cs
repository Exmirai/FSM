using FSM_Dotnet.FSM_ExtenalActions;
using FSM_Dotnet.Models;
using FSM_Dotnet.Models.FSM;
using FSM_Dotnet.Models.FSM.CSharpRuntime;
using FSM_Dotnet.Models.FSM.JsonConverters;
using FSM_Dotnet.Validation;
using System.Diagnostics;
using System.Text.Json;

namespace FSM_Dotnet
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var runtime = new CSharpRuntime();

            var jsonSerializerOptions = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Converters =
                    {
                        new ConditionInvokableConverter(runtime),
                        new ActionInvokableConverter(runtime),
                    }
                };

            var state = new State(["initial"]);

            var entity = new ExampleEntity()
            {
                Id = Guid.NewGuid(),
                Name = "test",
                SecName = "sectest",
                State = state,
            };

            var fileStream = File.OpenRead("C:\\Users\\UniqueUlysees\\Desktop\\Work\\FSM_Dotnet\\exampleentitydescription.json");


            var description = await JsonSerializer.DeserializeAsync<StateMachineDescription<ExampleEntity>>(fileStream, jsonSerializerOptions);

            var validatorsProvider = new EntityValidatorsProvider<ExampleEntity>();

            var stateMachine = new StateMachine<ExampleEntity>(description, validatorsProvider, runtime, entity);

            var testAction = new StateMachineModifyAction<ExampleEntity>(ent =>
            {
                ent.Name = "replaced";
                return ent;
            });

            stateMachine.QueueAction(testAction);
          

            Console.ReadKey();
        }
    }

}
