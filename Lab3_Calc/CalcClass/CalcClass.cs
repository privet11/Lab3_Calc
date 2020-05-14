public static class CalcClass
{
    /// <summary>
    /// Останнє повідомлення про помилку.
    /// Поле і властивість для нього
    /// </summary>
    public static string lastError { get; set; } = "";


    /// <summary>
    /// Функція складання числа а і b
    /// </summary>
    /// <param name="a">доданок</param>
    /// <param name="b">доданок</param>
    /// <returns>сума</returns>
    public static double Add(double a, double b)
    {
        return a + b;
    }

    /// <summary>
    /// функція віднімання чисел а і b
    /// </summary>
    /// <param name="a">зменшуване</param>
    /// <param name="b">від’ємне</param>
    /// <returns>різниця</returns>
    public static double Sub(double a, double b)
    {
        return a - b;
    }

    /// <summary>
    /// функція множення чисел а і b
    /// </summary>
    /// <param name="a">множник</param>
    /// <param name="b">множник</param>
    /// <returns>добуток</returns>
    public static double Mult(double a, double b)
    {
        return a * b;
    }

    /// <summary>
    /// функція знаходження частки
    /// </summary>
    /// <param name="a">ділене</param>
    /// <param name="b">дільник</param>
    /// <returns>частка</returns>
    public static double Div(double a, double b)
    {
        return a / b;
    }

    /// <summary>
    /// функція ділення по модулю
    /// </summary>
    /// <param name="a">ділене</param>
    /// <param name="b">дільник</param>
    /// <returns>остача</returns>
    public static double Mod(double a, double b)
    {
        return a % b;
    }

    /// <summary>
    /// унарний плюс
    /// </summary>
    /// <param name="a"></param>
    /// <returns>модуль</returns>
    public static double ABS(double a)
    {
        if (a < 0)
            return -a;
        return a;
    }

    /// <summary>
    /// унарний мінус
    /// </summary>
    /// <param name="a"></param>
    /// <returns>протилежне значення</returns>
    public static double IABS(double a)
    {
        return -a;
    }

}

