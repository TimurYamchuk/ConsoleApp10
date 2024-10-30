using ConsoleApp10;

class MainClass
{
    public static void Main()
    {
        try
        {
            ClassMenu menu = new ClassMenu();
            menu.Menu();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}
