public static class Helpers
{
    static public int ReadInt()
    {
        int val;
        while(!int.TryParse(Console.ReadLine(),out val)){
            Console.Write("Invalid input. Try again:");
        }
        return val;
    }
}