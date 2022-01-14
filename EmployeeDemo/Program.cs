using System;
using AutoMapper;
namespace AutoMapperDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Initialize the mapper
            var config = new MapperConfiguration(cfg =>
                    cfg.CreateMap<Employee, EmployeeDTO>()
                );

            //Creating the source object
            Employee emp = new Employee
            {
                Name = "James",
                Salary = 20000,
                Address = "London",
                Department = "IT"
            };

            //Using automapper
            IMapper mapper = new Mapper(config);
            var empDTO = mapper.Map<EmployeeDTO>(emp);
            //OR
            //var empDTO2 = mapper.Map<Employee, EmployeeDTO>(emp);

            Console.WriteLine("Name:" + empDTO.Name + ", Salary:" + empDTO.Salary + ", Address:" + empDTO.Address + ", Department:" + empDTO.Department);
            Console.ReadLine();
        }
    }

    public class Employee
    {
        public string Name { get; set; }
        public int Salary { get; set; }
        public string Address { get; set; }
        public string Department { get; set; }
    }

    public class EmployeeDTO
    {
        public string Name { get; set; }
        public int Salary { get; set; }
        public string Address { get; set; }
        public string Department { get; set; }
    }
}
