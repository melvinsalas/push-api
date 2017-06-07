namespace TokenApi.Models
{
	public class MessageItem
	{
		public long Id { get; set; }
		public string Email { get; set; }
        public string Title { get; set; }
		public string Body { get; set; }
		public string Activity { get; set; }
		public string ActivityId { get; set; }
	}
}