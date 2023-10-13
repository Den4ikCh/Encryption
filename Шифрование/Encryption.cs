using System;
class Encryption
{
    static void Main()
    {
        Console.WriteLine("Выберите тип шифрования:\n1)Шифр Цезаря со сдвигом на N букв вправо;\n2)Переворот алфавита (вместо \"а\" пишется \"я\", вместо \"б\" - \"ю\" и т.д.);\n3)Шифр цезаря с ключевым словом.");
        int type = int.Parse(Console.ReadLine()); //записываем в переменную тип шифровки
        Console.WriteLine("Выберите язык:\n1)Английский;\n2)Русский.");
        int l = int.Parse(Console.ReadLine()); //записываем в переменную язык
        string language = null;
        if (l == 1)
            language = "Английский";
        else
            language = "Русский";

        Console.WriteLine("Введите слово, которое нужно зашифровать/расшифровтаь.");
        string word = Console.ReadLine(); //записываем в переменную шифруемое слово

        if (type == 1)
        {
            Console.WriteLine("Введите число, на которое будут сдвигаться буквы вправо по алфавиту.");
            int n = int.Parse(Console.ReadLine());
            Console.WriteLine(EncryptionTypes.Ceaser(word, language, n)); //вызываем метод статического класса, который возвращает строку, зашифрованную по алгоритму Цезаря
        }
        else if (type == 2)
        {
            Console.WriteLine(EncryptionTypes.AlfabetRotation(word, language)); //вызываем метод который поменяет все буквы в слове на равноудалённые от средней
        }
        else if (type == 3)
        {
            Console.WriteLine("Введите ключевое слово, по которому будет осуществляться сдвиг.");
            string key = Console.ReadLine();
            Console.WriteLine(EncryptionTypes.WithKey(word, language, key)); //вызываем метод, который зашифрует слово по алгоритму Цезаря с ключевым словом
        }
        else
            Console.WriteLine("Ошибка");
    }
}