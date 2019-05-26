using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Registration.Issue.Function.Extensions;

[assembly: FunctionsStartup(typeof(NewKens.Cognitive.Serverless.CandidateManagement.Function.Startup))]
namespace NewKens.Cognitive.Serverless.CandidateManagement.Function
{
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
}
