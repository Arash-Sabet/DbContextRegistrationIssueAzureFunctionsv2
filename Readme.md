# The purpose of this repository
The purpose of this repository is to demonstrate reproducing [this issue posted on github](https://github.com/Azure/azure-functions-core-tools/issues/1341#issuecomment-495969840).

## Error mesage when running the project

The following error message shows up when running the project:

> [2019-05-26 1:53:14 PM] Building host: startup suppressed:False, configuration suppressed: False
> [2019-05-26 1:53:14 PM] A host error has occurred
> [2019-05-26 1:53:14 PM] Registration.Issue.Function: Method Microsoft.Extensions.DependencyInjection.EntityFrameworkServiceCollectionExtensions.AddDbContext: type argument 'Registration.Issue.Function.AccountManagementDbContext' violates the constraint of type parameter 'TContext'.

## How to reproduce the problem
Simply run the project in Visual Studio 2019 on azure storage emulator. 

## Possible root cause

Adding ```AddDbContext``` to the service collection may or might be contributing in this issue. For e.g. if this line of code ```.AddDatabase(configuration)``` is commented out from ```Configure``` method below,
and the project is run again, the quoted problem above will disappear.

```csharp
public class Startup : FunctionsStartup
	{
		public override void Configure(IFunctionsHostBuilder builder)
		{
			var configuration = new ConfigurationBuilder().AddFunctionConfiguration("local.settings.json");

			builder
				.Services
				.AddDatabase(configuration)
				.AddNewKensAuthentication(configuration);
		}
	}
```
