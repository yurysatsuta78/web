namespace pro.Classes
{
    public class CarsForCarsViewModel
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string Price { get; set; }
        public string EngineVolume { get; set; }
        public string EngineType { get; set; }
        public string DriveUnit { get; set; }
        public string Transmission { get; set; }
        public byte[] Image { get; set; }

        public CarsForCarsViewModel(int id, string brand, string model, string year, string price, string enginevolume, string enginetype, string driveunit, string transmission, byte[] image)
        {
            Id = id;
            Brand = brand;
            Model = model;
            Year = year;
            Price = price;
            EngineVolume = enginevolume;
            EngineType = enginetype;
            DriveUnit = driveunit;
            Transmission = transmission;
            Image = image;
        }
    }
}
