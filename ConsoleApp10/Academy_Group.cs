using System;
using System.Collections;
using System.IO;

namespace ConsoleApp10
{
    public class AcademyGroup : ICloneable
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
                    students.Sort(); // Используем IComparable
                    break;
                case 2:
                    Console.WriteLine("\nСортировка по фамилии:");
                    students.Sort(new Student.SortBySurname());
                    break;
                case 3:
                    Console.WriteLine("\nСортировка по возрасту:");
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
                    file.WriteLine($"Имя - {student.Name}\nФамилия - {student.Surname}\nТелефон - {student.Phone}\nВозраст - {student.Age}\nСредний балл - {student.Average}\nГруппа - {student.NumberOfGroup}\n");
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
            Console.WriteLine("Введите значение для поиска:");
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

        // Реализация интерфейса ICloneable
        public object Clone()
        {
            AcademyGroup clonedGroup = new AcademyGroup();
            foreach (Student student in students)
            {
                clonedGroup.Add(new Student(student.Name, student.Surname, student.Age, student.Phone, student.Average, student.NumberOfGroup));
            }
            return clonedGroup;
        }
    }
}
