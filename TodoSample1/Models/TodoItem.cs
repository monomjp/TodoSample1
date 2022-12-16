using MessagePack;

namespace TodoSample1.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Todo { get; set; }
        public string UserId { get; set; }
    }
}
