using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Diagnostics;

namespace AutoMapperDemo
{

   


    class Wedding
    {
        public DateTime Date { get; set; }

        public Person Bride { get; set; }

        public Person Groom { get; set; }

        public string JimmysMom { get; set; }

    }

    class Person
    {

        public int Id { get; set; }
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
        public int BrideId { get; set; }

        public string BrideTitle { get; set; }

        public string BrideName { get; set; }

        public string BrideAddressLine1 { get; set; }

        public int GroomId { get; set; }

        public string GroomTitle { get; set; }

        public string GroomName { get; set; }

        public string GroomAddressLine1 { get; set; }

        public string Command { get; set; }


    }


    class Program
    {

        private static void WriteOutJson(string rawString)
        {

            rawString = rawString.Replace("\"", ""); //remove quotes
            rawString = rawString.Replace("{", "");
            rawString = rawString.Replace("}", "");
            //break each element into a new row
            var split1 = rawString.Split(',');
            foreach (var row in split1)
            {

                //break each property and separate the two into defined columns
                var split2 = row.Split(':');
                Console.WriteLine($"{split2[0],-20} {split2[1]}");
            }
            Console.WriteLine(); //skip a line after printing
        }

        static void Main(string[] args)
        {
            var config = new MapperConfiguration(cfg =>
            {
                var toUIMap = cfg.CreateMap<Wedding, WeddingViewModel>().ForMember(dest => dest.Command, opt => opt.Ignore());
                var toDALMap = toUIMap.ReverseMap();

                toDALMap.ForPath(d => d.JimmysMom, opt => opt.Ignore());



                // cfg.CreateMap<WeddingViewModel, Wedding>();
                //.ForPath(dest => dest.Bride.Id, opt => opt.MapFrom(src => src.BrideId))
                //.ForPath(dest => dest.Bride.Name, opt => opt.MapFrom(src => src.BrideName))
                //.ForPath(dest => dest.Bride.Title, opt => opt.MapFrom(src => src.BrideTitle))
                //.ForPath(dest => dest.Bride.Address.Line1, opt => opt.MapFrom(src => src.BrideAddressLine1))
                //.ForPath(dest => dest.Groom.Id, opt => opt.MapFrom(src => src.GroomId))
                //.ForPath(dest => dest.Groom.Name, opt => opt.MapFrom(src => src.GroomName))
                //.ForPath(dest => dest.Groom.Title, opt => opt.MapFrom(src => src.GroomTitle))
                //.ForPath(dest => dest.Groom.Address.Line1, opt => opt.MapFrom(src => src.GroomAddressLine1));
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
                Id = 1,
                Name = "Beyonce",
                Title = Title.Mrs,
                Address = new Address { Line1 = "123 Manhattan Ave" }
            };

            Person groom = new Person
            {
                Id = 2,
                Name = "Sean Carter",
                Address = new Address { Line1 = "123 Manhattan Ave" }
            };

            IMapper mapper = new Mapper(config);

            var wedding = new Wedding { Bride = bride, Groom = groom };


            var weddingViewModel = mapper.Map<WeddingViewModel>(wedding);


            Console.WriteLine("Writing out weddingViewModel");
            WriteOutJson(JsonConvert.SerializeObject(weddingViewModel));
            weddingViewModel.BrideTitle = "Mr";
            var mappedWedding = mapper.Map<Wedding>(weddingViewModel);
            Console.WriteLine("Writing out mappedWedding");
            WriteOutJson(JsonConvert.SerializeObject(mappedWedding));

            //Console.WriteLine(JsonConvert.SerializeObject(weddingViewModel));
            //weddingViewModel.BrideTitle = "Mr";

            //var mappedWedding = mapper.Map<Wedding>(weddingViewModel);
            //Console.WriteLine(JsonConvert.SerializeObject(mappedWedding));


            Console.WriteLine("End");
            Console.ReadLine();
        }
    }
}
