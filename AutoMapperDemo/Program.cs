using AutoMapper;
using System;

namespace AutoMapperDemo
{

    class Wedding
    {
        public DateTime Date { get; set; }

        public Person Bride { get; set; }

        public Person Groom { get; set; }
    }

    class Person
    {
        public Title Title { get; set; }

        public string Name { get; set; }

        public Address Address { get; set; }
    }

    class Address
    {
        public string Line1 { get; set; }
    }

    enum Title
    {
        Other, Mr, Ms, Miss, Mrs, Dr
    }


    class WeddingViewModel
    {
        public DateTime Date { get; set; }
        //public int Id { get; set; }

        public string BrideTitle { get; set; }

        public string BrideName { get; set; }

        public string BrideAddressLine1 { get; set; }

        public string GroomTitle { get; set; }

        public string GroomName { get; set; }

        public string GroomAddressLine1 { get; set; }
    }


    class Program
    {

 
        static void Main(string[] args)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Wedding, WeddingViewModel>();
            });
         

            try
            {
                config.AssertConfigurationIsValid();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.ToString());
            }

            Person bride = new Person
            {
                Name = "Beyonce",
                Title = Title.Mrs,
                Address = new Address { Line1 = "123 Manhattan Ave" }
            };

            Person groom = new Person
            {
                Name = "Sean Carter",
                Title = Title.Mr,
                Address = new Address { Line1 = "123 Manhattan Ave" }
            };

            IMapper mapper = new Mapper(config);

            var wedding = new Wedding { Bride = bride, Groom = groom };


            var weddingViewModel = mapper.Map<WeddingViewModel>(wedding);


            Console.WriteLine("End");
            Console.ReadLine();
        }
    }
}
