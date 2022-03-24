using System;

namespace MediaService.Services
{


    public class MainService : IMainService
    {
        private readonly IDataService _dataService;

        public MainService(IDataService dataService)
        {
            _dataService = dataService;
        }

        public void Invoke()
        {
            string choice;
            do
            {
                Console.WriteLine("1) Add Movie");
                Console.WriteLine("2) Display All Movies");
                Console.WriteLine("X) Quit");
                choice = Console.ReadLine();

                if (choice == "1")
                {
                    _dataService.Write();
                }
                else if (choice == "2")
                {
                    _dataService.Read();
                }
            } while (choice != "X");
        }
    }
}