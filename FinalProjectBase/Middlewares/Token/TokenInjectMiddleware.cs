namespace FinalProjectBase.Middlewares.Token
{
	public class TokenInjectMiddleware
	{
		private readonly RequestDelegate _next;

		public TokenInjectMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext httpContext)
		{
			var token = httpContext.Request.Cookies["JWT"];

			if (token is not null)
			{
				httpContext.Request.Headers.Append("Authorization", "Bearer " + token);
			}

			// Calling next Middleware
			await _next(httpContext);
		}
	}
}
