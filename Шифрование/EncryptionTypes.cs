using System;
static class EncryptionTypes
{
    public static string WithKey(string str_tmp, string language, string turns) //метод принимает в себя значения: шифруемое слово, язык, число сдвигов по алфавиту
    {
        char? firstLetter = null;
        char? lastLetter = null;
        if (language == "Английский") //назначаем переменные, обозначающие первую и последнюю буквы алфавита
        {
            firstLetter = 'a';
            lastLetter = 'z';
        }
        else
        {
            firstLetter = 'а';
            lastLetter = 'я';         //
        }
        str_tmp = str_tmp.ToLower(); //меняем все буквы слова на строчные, так как далее мы будем использовать кодировку символа в Unicode, а заглавная буква "А" идёт после строчной "я"
        int index = 0;
        int j = 0;
        int turn = 0;
        char[] c = new char[str_tmp.Length]; //создаём результирующий массив символов, который составит зашифрованное слово
        for (int i = 0; i < str_tmp.Length; i++) //проходим все буквы слова
        {
            turn = (int)turns[j] - (int)firstLetter + 1; //Записываем в переменную временное значение смещения буквы вправо. Т.к. символы записываются в кодировке Unicode, их можно
                                                         //перевести в целочисленную переменную, данная строка возвращает кодировку символа в Unicode, из которой вычли кодировку 
                                                         //первой буквы алфавита, таким образом получился индекс вооброжаемого массива, состоящего из букв алфавит, соответственно 
                                                         //для получения номера буквы в алфавите, мы плюсуем 1
            
            if (!IsLetterInBounds(str_tmp[i], firstLetter, lastLetter) && str_tmp[i] != 'ё')  //проверяем каждую букву шифруемого слова, на предмет расхождения с языком, указанным пользователем
                return "Ошибка";

            if (!IsLetterInBounds(turns[j], firstLetter, lastLetter))  //проверяем каждую букву ключевого слова, на предмет расхождения с языком, указанным пользователем
                return "Ошибка";

            if (j < turns.Length - 1) //увеличиваем индекс буквы в ключе
                j++;
            else                      //если букв в ключевом слове меньше, чем в шифруемом, возвращаемся к перой букве ключа
                j = 0;

            index = ((int)str_tmp[i] + turn - (int)firstLetter) % ((int)lastLetter - (int)firstLetter + 1) + (int)firstLetter; //Добавляем к букве шифруемого слова число, на которое согласно ключа нужно её сдвинуть
                                                                                                                               //Т.к. если буква была близка к концу алфавита, плюсуя к ней число, мы можем получить
                                                                                                                               //символ в кодировке Unicode, не являющийся буквой алфавита, поэтому нужно переносить  
                                                                                                                               //индекс на начало алфавита. Чтобы это сделать, вычитаем первую букву алфавита,
                                                                                                                               //получаем индекс буквы в массиве из всех букв алфавита и ищем остаток от деления на длину массива 
                                                                                                                               //(последняя буква минус первая, плюс 1, т.к. получился индекс последнего элемента, а нужна его длина)
                                                                                                                               //так как это был индекс массива, добавляем кодировку первой буквы, чтобы получить кодировку результирующей буквы

            if (str_tmp[i] == 'ё')
                index = ((int)'е' + turn - (int)firstLetter) % ((int)lastLetter - (int)firstLetter + 1) + (int)firstLetter;  //так как буква "ё" в кодировке Unicode стоит после алфавита, меняем её на "е", букву нельзя заменить в слове, т.к. символы строки 
                                                                                                                             //являются onlyread переменными

            c[i] = (char)index; //записываем букву в результирующий массив
        }
        return new string(c); //возвращаем слово
    }

    public static string Ceaser(string str_tmp, string language, int turn) //метод принимает в себя значения: шифруемое слово, язык, ключ, по которому будет происводиться шифрование
    {
        char? firstLetter = null;
        char? lastLetter = null;
        if (language == "Английский")
        {
            firstLetter = 'a';
            lastLetter = 'z';
        }
        else
        {
            firstLetter = 'а';
            lastLetter = 'я';
        }

        str_tmp.ToLower(); //описано в строке 18
        int index = 0;
        char[] c = new char[str_tmp.Length]; //описано в строке 22

        for (int i = 0; i < str_tmp.Length; i++)
        {
            index = ((int)str_tmp[i] + turn - (int)firstLetter) % ((int)lastLetter - (int)firstLetter + 1) + (int)firstLetter; //плюсуем к букве число, на которое нужно было сдвинуть буквы слова

            if (str_tmp[i] == 'ё')
                index = ((int)'е' + turn - (int)firstLetter) % ((int)lastLetter - (int)firstLetter + 1) + (int)firstLetter; //описано в строке 42

            c[i] = (char)index;

            if (!IsLetterInBounds(str_tmp[i], firstLetter, lastLetter) && str_tmp[i] != 'ё')
                return "Ошибка";
        }
        return new string(c); //возвращаем слово
    }

    public static string AlfabetRotation(string str_tmp, string language) //метод принимает в себя значения: шифруемое слово и язык
    {
        char? firstLetter = null;
        char? lastLetter = null;
        if (language == "Английский")
        {
            firstLetter = 'a';
            lastLetter = 'z';
        }
        else
        {
            firstLetter = 'а';
            lastLetter = 'я';
        }
        str_tmp = str_tmp.ToLower(); //описано в строке 18
        float index = 0;
        char[] res_chars = new char[str_tmp.Length]; //описано в строке 22
        for (int i = 0; i < str_tmp.Length; i++)
        {
            index = ((float)(lastLetter - firstLetter) / 2 + (int)firstLetter) * 2 - (int)(str_tmp[i]); //Чтобы поменять букву на равноудалённую от центра алфавита, нужно представить алфавит
                                                                                                        //в виде массива и максимальный индекс массива поделить пополам, это будет "середина"
                                                                                                        //алфавита. Из середины массива вычесть индекс буквы и снова прибавить середину массива.
                                                                                                        //И снова нужно перевести индекс в кодировку Unicode, добавив первую букву алфавита.

            if (str_tmp[i] == 'ё')
                index = ((float)(lastLetter - firstLetter) / 2 + (int)firstLetter) * 2 - (int)'е'; //описано в строке 42

            if (!IsLetterInBounds(str_tmp[i], firstLetter, lastLetter) && str_tmp[i] != 'ё')
                return "Ошибка";

            res_chars[i] = (char)((int)index);
        }
        return new string(res_chars); //возвращаем слово
    }

    static bool IsLetterInBounds(char letter, char? first, char? last)
    {
        if (letter > last || letter < first) //если буква в кодировке больше "я" или меньше "а", то это не буква русского алфавита, так же для других языков
            return false;                     //поэтому возвращаем false, после чего выведется сообщение "Ошибка"
        return true;
    }
}