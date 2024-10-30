using System;

namespace ConsoleApp10
{
    class Person
    {
        public string Name { get; set; } = "";
        public string Surname { get; set; } = "";
        public string Phone { get; set; } = "";
        public int Age { get; set; } = 0;

        public Person() { }

        public Person(string name, string surname, string phone, int age)
        {
            Name = name;
            Surname = surname;
            Phone = phone;
            Age = age;
        }

        public void Print()
        {
            Console.WriteLine($"Имя - {Name}\nФамилия - {Surname}\nТелефон - {Phone}\nВозраст - {Age}");
        }
    }
}
