namespace ConsoleView.Messages;

static class StringExtension
{
    public static string ConjugateForNumerus(this string verb, int number)
    {
        return number == 1 ? verb+"s" : verb;
    }

    public static string DeclinateForNumerus(this string substantive, int number)
    {
        return number == 1 ? substantive : substantive + "s";
    }
}