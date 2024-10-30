using System;

namespace ConsoleApp10
{
    class ClassMenu
    {
        private AcademyGroup academyGroup = new AcademyGroup();

        public void Menu()
        {
            while (true)
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Добавить студента");
                Console.WriteLine("2. Удалить студента");
                Console.WriteLine("3. Редактировать студента");
                Console.WriteLine("4. Показать всех студентов");
                Console.WriteLine("5. Сортировать студентов");
                Console.WriteLine("6. Сохранить студентов в файл");
                Console.WriteLine("7. Загрузить студентов из файла");
                Console.WriteLine("8. Найти студента");
                Console.WriteLine("9. Выход");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddStudent();
                        break;
                    case "2":
                        RemoveStudent();
                        break;
                    case "3":
                        EditStudent();
                        break;
                    case "4":
                        academyGroup.Print();
                        break;
                    case "5":
                        SortStudents();
                        break;
                    case "6":
                        academyGroup.Save();
                        break;
                    case "7":
                        academyGroup.Load();
                        break;
                    case "8":
                        SearchStudent();
                        break;
                    case "9":
                        return;
                    default:
                        Console.WriteLine("Некорректный выбор. Попробуйте снова.");
                        break;
                }
            }
        }

        private void AddStudent()
        {
            Console.Write("Введите имя: ");
            string name = Console.ReadLine();
            Console.Write("Введите фамилию: ");
            string surname = Console.ReadLine();
            Console.Write("Введите телефон: ");
            string phone = Console.ReadLine();
            Console.Write("Введите возраст: ");
            int age = int.Parse(Console.ReadLine());
            Console.Write("Введите средний балл: ");
            double average = double.Parse(Console.ReadLine());
            Console.Write("Введите номер группы: ");
            int group = int.Parse(Console.ReadLine());

            Student student = new Student(name, surname, age, phone, average, group);
            academyGroup.Add(student);
        }

        private void RemoveStudent()
        {
            Console.Write("Введите фамилию студента для удаления: ");
            string surname = Console.ReadLine();
            academyGroup.Remove(surname);
        }

        private void EditStudent()
        {
            Console.Write("Введите фамилию студента для редактирования: ");
            string surname = Console.ReadLine();
            Console.Write("Введите новое имя: ");
            string newName = Console.ReadLine();
            Console.Write("Введите новую фамилию: ");
            string newSurname = Console.ReadLine();
            Console.Write("Введите новый телефон: ");
            string newPhone = Console.ReadLine();
            Console.Write("Введите новый возраст: ");
            int newAge = int.Parse(Console.ReadLine());
            Console.Write("Введите новый средний балл: ");
            double newAverage = double.Parse(Console.ReadLine());
            Console.Write("Введите новый номер группы: ");
            int newGroup = int.Parse(Console.ReadLine());

            Student newStudent = new Student(newName, newSurname, newAge, newPhone, newAverage, newGroup);
            academyGroup.Edit(surname, newStudent);
        }

        private void SortStudents()
        {
            Console.WriteLine("Выберите критерий сортировки:");
            Console.WriteLine("1. По имени");
            Console.WriteLine("2. По фамилии");
            Console.WriteLine("3. По возрасту");
            int criteria = int.Parse(Console.ReadLine());
            academyGroup.Sort(criteria);
        }

        private void SearchStudent()
        {
            Console.WriteLine("Выберите критерий поиска:");
            Console.WriteLine("1. По имени");
            Console.WriteLine("2. По фамилии");
            Console.WriteLine("3. По телефону");
            Console.WriteLine("4. По возрасту");
            Console.WriteLine("5. По среднему баллу");
            Console.WriteLine("6. По номеру группы");
            int criterion = int.Parse(Console.ReadLine());
            academyGroup.Search(criterion);
        }
    }
}
