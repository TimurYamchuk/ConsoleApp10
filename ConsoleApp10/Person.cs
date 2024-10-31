using System;

namespace ConsoleApp10
{
    class MainClass
    {
        public static void Main()
        {
            try
            {
                AcademyGroup group = new AcademyGroup();

                group.Add(new Student("Иван", "Иванов", 20, "123-456-7890", 4.2, 101));
                group.Add(new Student("Мария", "Петрова", 22, "098-765-4321", 3.8, 101));
                group.Add(new Student("Сергей", "Сидоров", 21, "111-222-3333", 4.5, 101));

                Console.WriteLine("Перебор студентов с помощью foreach:");
                foreach (Student student in group)
                {
                    student.Print();
                }

                Console.WriteLine("\nКлонирование группы студентов...");
                AcademyGroup clonedGroup = (AcademyGroup)group.Clone();

                Console.WriteLine("\nКлонированная группа студентов:");
                clonedGroup.Print();

                Console.WriteLine("\nДобавление нового студента в клонированную группу.");
                clonedGroup.Add(new Student("Алексей", "Кузнецов", 23, "444-555-6666", 3.9, 102));

                Console.WriteLine("\nОригинальная группа студентов:");
                group.Print();

                Console.WriteLine("\nКлонированная группа студентов после добавления нового студента:");
                clonedGroup.Print();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}
