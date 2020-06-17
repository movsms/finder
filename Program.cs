using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        List<String> emails = new List<String>();
        List<String> numbers = new List<String>();
        List<String> dates = new List<String>();

        using (StreamReader sr = new StreamReader("file.txt"))
        {
            while (sr.Peek() >= 0)
            {
                string line = sr.ReadLine();
                string[] words = line.Split();
                foreach (string word in words)
                {
                    if (isEmail(word))
                    {
                        emails.Add(word);
                    }
                    else if (isDate(word))
                    {
                        dates.Add(word);
                    }
                    else if (isNumber(word))
                    {
                        numbers.Add(word);
                    }
                }
            }
        }

        foreach (string number in numbers)
        {
            Console.WriteLine(String.Format("Найден номер: {0}", number));
        }
        foreach (string date in dates)
        {
            Console.WriteLine(String.Format("Найдена дата: {0}", date));
        }
        foreach (string email in emails)
        {
            Console.WriteLine(String.Format("Найдена почта: {0}", email));
        }

        Console.ReadLine();

    }

    static bool isEmail(string word)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(word);
            return addr.Address == word;
        }
        catch
        {
            return false;
        }
    }

    static bool isNumber(string number)
    {
        return Regex.Match(number, @"\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d").Success;
    }

    static bool isDate(string date)
    {
        DateTime temp;
        if (DateTime.TryParse(date, out temp))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

