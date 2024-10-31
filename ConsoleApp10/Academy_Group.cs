using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp10
{
    public class Student : IComparable<Student>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public double Average { get; set; }
        public int NumberOfGroup { get; set; }

        public Student(string name, string surname, int age, string phone, double average, int numberOfGroup)
        {
            Name = name;
            Surname = surname;
            Age = age;
            Phone = phone;
            Average = average;
            NumberOfGroup = numberOfGroup;
        }

        public void Print()
        {
            Console.WriteLine($"Имя: {Name}, Фамилия: {Surname}, Возраст: {Age}, Телефон: {Phone}, Средний балл: {Average}, Номер группы: {NumberOfGroup}");
        }

        public int CompareTo(Student other)
        {
            return this.Name.CompareTo(other.Name);
        }

        public class SortBySurname : IComparer<Student>
        {
            public int Compare(Student x, Student y)
            {
                return x.Surname.CompareTo(y.Surname);
            }
        }

        public class SortByAge : IComparer<Student>
        {
            public int Compare(Student x, Student y)
            {
                return x.Age.CompareTo(y.Age);
            }
        }
    }

    public class AcademyGroup : ICloneable, IEnumerable<Student>
    {
        private ArrayList students = new ArrayList();

        public void Add(Student student)
        {
            students.Add(student);
            Console.WriteLine($"Студент {student.Name} {student.Surname} добавлен.");
        }

        public void Remove(string studentSurname)
        {
            for (int i = 0; i < students.Count; i++)
            {
                Student student = (Student)students[i];
                if (student.Surname.Equals(studentSurname, StringComparison.OrdinalIgnoreCase))
                {
                    students.RemoveAt(i);
                    Console.WriteLine($"Студент {studentSurname} удалён из группы.");
                    return;
                }
            }
            Console.WriteLine($"Студент {studentSurname} не найден в группе.");
        }

        public void Edit(string studentSurname, Student newStudent)
        {
            for (int i = 0; i < students.Count; i++)
            {
                Student student = (Student)students[i];
                if (student.Surname.Equals(studentSurname, StringComparison.OrdinalIgnoreCase))
                {
                    students[i] = newStudent;
                    Console.WriteLine($"Студент {studentSurname} редактирован на {newStudent.Surname}.");
                    return;
                }
            }
            Console.WriteLine($"Студент {studentSurname} не найден для редактирования.");
        }

        public void Print()
        {
            if (students.Count == 0)
            {
                Console.WriteLine("Нет студентов в группе.");
                return;
            }

            foreach (Student student in students)
            {
                student.Print();
                Console.WriteLine();
            }
        }

        public void Sort(int sortingCriteria)
        {
            switch (sortingCriteria)
            {
                case 1:
                    Console.WriteLine("Сортировка по имени:");
                    students.Sort();
                    break;
                case 2:
                    Console.WriteLine("Сортировка по фамилии:");
                    students.Sort(new Student.SortBySurname());
                    break;
                case 3:
                    Console.WriteLine("Сортировка по возрасту:");
                    students.Sort(new Student.SortByAge());
                    break;
                default:
                    Console.WriteLine("Нет такого критерия!");
                    return;
            }
            Print();
        }

        public void Save()
        {
            using (StreamWriter file = new StreamWriter("Group.txt"))
            {
                foreach (Student student in students)
                {
                    file.WriteLine($"Имя - {student.Name}");
                    file.WriteLine($"Фамилия - {student.Surname}");
                    file.WriteLine($"Телефон - {student.Phone}");
                    file.WriteLine($"Возраст - {student.Age}");
                    file.WriteLine($"Средний балл - {student.Average}");
                    file.WriteLine($"Группа - {student.NumberOfGroup}");
                    file.WriteLine();
                }
            }
            Console.WriteLine("Данные о студентах сохранены в файл.");
        }

        public void Load()
        {
            students.Clear();
            using (StreamReader file = new StreamReader("Group.txt"))
            {
                while (!file.EndOfStream)
                {
                    string name = file.ReadLine().Split('-')[1].Trim();
                    string surname = file.ReadLine().Split('-')[1].Trim();
                    string phone = file.ReadLine().Split('-')[1].Trim();
                    int age = int.Parse(file.ReadLine().Split('-')[1].Trim());
                    double average = double.Parse(file.ReadLine().Split('-')[1].Trim());
                    int group = int.Parse(file.ReadLine().Split('-')[1].Trim());
                    file.ReadLine(); // Пустая строка

                    students.Add(new Student(name, surname, age, phone, average, group));
                }
            }
            Console.WriteLine("Данные о студентах загружены из файла.");
        }

        public void Search(int criterionNumber)
        {
            Console.Write("Введите значение для поиска: ");
            string searchValue = Console.ReadLine();
            bool found = false;

            foreach (Student student in students)
            {
                bool matches = criterionNumber switch
                {
                    1 => student.Name.Equals(searchValue, StringComparison.OrdinalIgnoreCase),
                    2 => student.Surname.Equals(searchValue, StringComparison.OrdinalIgnoreCase),
                    3 => student.Phone.Equals(searchValue, StringComparison.OrdinalIgnoreCase),
                    4 => student.Age.ToString() == searchValue,
                    5 => student.Average.ToString() == searchValue,
                    6 => student.NumberOfGroup.ToString() == searchValue,
                    _ => false
                };

                if (matches)
                {
                    student.Print();
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("Такой студент не найден.");
            }
        }

        public object Clone()
        {
            AcademyGroup clonedGroup = new AcademyGroup();
            foreach (Student student in students)
            {
                clonedGroup.Add(new Student(student.Name, student.Surname, student.Age, student.Phone, student.Average, student.NumberOfGroup));
            }
            return clonedGroup;
        }

        public IEnumerator<Student> GetEnumerator()
        {
            foreach (Student student in students)
            {
                yield return student;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Создание группы и добавление студентов
            AcademyGroup originalGroup = new AcademyGroup();
            originalGroup.Add(new Student("Иван", "Иванов", 20, "123-456-7890", 4.2, 101));
            originalGroup.Add(new Student("Мария", "Петрова", 22, "098-765-4321", 3.8, 101));
            originalGroup.Add(new Student("Сергей", "Сидоров", 21, "111-222-3333", 4.5, 101));

            Console.WriteLine("Оригинальная группа студентов:");
            originalGroup.Print();

            // Клонирование группы
            AcademyGroup clonedGroup = (AcademyGroup)originalGroup.Clone();

            Console.WriteLine("\nКлонированная группа студентов:");
            clonedGroup.Print();

            // Проверка независимости клонированной группы
            Console.WriteLine("\nДобавление нового студента в клонированную группу.");
            clonedGroup.Add(new Student("Алексей", "Кузнецов", 23, "444-555-6666", 3.9, 102));

            Console.WriteLine("\nОригинальная группа после добавления студента в клонированную группу:");
            originalGroup.Print();

            Console.WriteLine("\nКлонированная группа после добавления нового студента:");
            clonedGroup.Print();
        }
    }
}
