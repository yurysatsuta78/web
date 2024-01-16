namespace pro.Classes
{
    public class NewsForNewsViewModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public byte[] Image {  get; set; }

        public NewsForNewsViewModel(int id, string title, string description, DateTime date, byte[] image)
        {
            ID = id;
            Title = title;
            Description = description;
            Date = date;
            Image = image;
        }
    }
}
