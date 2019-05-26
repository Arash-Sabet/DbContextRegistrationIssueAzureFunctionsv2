using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Registration.Issue.Function.Extensions
{
	public static class ContainerExtensions
	{
		public static IConfigurationRoot AddFunctionConfiguration(this ConfigurationBuilder configurationBuilder, string settingsFilePath)
		{
			var localRoot = Environment.GetEnvironmentVariable("AzureWebJobsScriptRoot");
			var azureRoot = $"{Environment.GetEnvironmentVariable("HOME")}/site/wwwroot";
			var root = localRoot ?? azureRoot;

			return configurationBuilder
				.SetBasePath(root)
				.AddJsonFile(settingsFilePath)
				.Build();
		}

		public static AuthenticationBuilder AddNewKensAuthentication(this IServiceCollection services, IConfiguration configuration)
			=> services
				.AddAuthentication(options =>
				{
					options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
					options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				})
				.AddJwtBearer(options =>
				{
					var tokenProviderOptions = new TokenProviderOptions();
					configuration.GetSection("TokenProviderOptions").Bind(tokenProviderOptions);
					options.TokenValidationParameters = tokenProviderOptions.TokenValidationParameters;
				});


		public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
			=> services.AddDbContext<AccountManagementDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
	}
}
