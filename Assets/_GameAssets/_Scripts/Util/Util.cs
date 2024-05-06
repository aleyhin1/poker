public static class Util 
{
    public static int Modulo(int number, int modulo)
    {
        if (number >= 0)
        {
            return number % modulo;
        }
        else
        {
            return modulo + number % modulo;
        }
    }
}
