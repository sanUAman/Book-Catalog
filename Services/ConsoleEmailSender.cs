namespace RazorPagesBook.Services
{
    public class ConsoleEmailSender : IEmailSender
    {
        public Task SendAsync(string to, string sub, string html, string? plain = null)
        {
            Console.WriteLine($"[EMAIL to={to}] {sub}\n{plain ?? html}");
            return Task.CompletedTask;
        }
    }
}
