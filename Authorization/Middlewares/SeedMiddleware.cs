using Authorization.Business.Interfaces;

namespace Authorization.Middlewares
{
    public class SeedMiddleware
    {
        private readonly RequestDelegate _next;
        public SeedMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ISeedService seedService)
        {
            try
            {
                var users = await seedService.UsersAsync();

                if (users.Count > 0) 
                { 
                    await _next(context);
                    return;
                }

                await seedService.SeedAsync();

                await _next(context);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
