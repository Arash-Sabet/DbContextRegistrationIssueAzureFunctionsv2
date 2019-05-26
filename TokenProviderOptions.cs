using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Registration.Issue.Function
{
	internal class TokenProviderOptions
	{
		public TokenProviderOptions()
			=> Expiration = SecondsToMidNight();

		public string Path { get; set; }

		public string Issuer { get; set; }

		public string ValidIssuer { get; set; }

		public string ValidAudience { get; set; }

		public string Audience { get; set; }

		public string SecretKey { get; set; }

		public TimeSpan Expiration { get; set; }

		public SymmetricSecurityKey SigningKey => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

		public SigningCredentials SigningCredentials => new SigningCredentials(SigningKey, SecurityAlgorithms.HmacSha256);

		public TokenValidationParameters TokenValidationParameters => new TokenValidationParameters
		{
			ValidateAudience = true,
			ValidAudience = ValidAudience,
			ValidateLifetime = true,
			ClockSkew = TimeSpan.Zero,
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = SigningKey,
			ValidateIssuer = true,
			ValidIssuer = ValidIssuer,
		};

		private TimeSpan SecondsToMidNight()
			=> TimeSpan.FromSeconds((int)DateTime.Today.AddDays(1).Subtract(DateTime.Now).TotalSeconds);
	}
}
