using Shared.Commands;

namespace Publisher.Domain.Commands
{
    public class CreateMessageCommand : ICommand
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
    }
}
