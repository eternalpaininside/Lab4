using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlTypes;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab4
{
    internal class Result
    {
        static Random random = new Random();

        //интерфейс
        static void PrintMenu(string[] menu)
        {
            int index = 1;
            foreach (string item in menu)
            {
                Console.WriteLine($"{index++}) {item}");
            }
        }

        static int ReadInt(string message = "Введите целое число ", string errorMessage = "Ошибка ввода целого числа!", string errorMessageLine = "Ваше число слишком большое/малое" ,int min = int.MinValue, int max = int.MaxValue)
        {
            bool isConvert;
            int number;
            do
            {
                Console.Write(message);
                isConvert = int.TryParse(Console.ReadLine(), out number);
                if (!isConvert)
                    Console.WriteLine(errorMessage);
                else if (number < min || number > max)
                {
                    Console.WriteLine(errorMessageLine);
                    isConvert = false;
                }
            } while (!isConvert);
            return number;
        }

        // бизнес-логика
        static int[] CreateArtificialArray(int len)
        {
            int[] arr = new int[len];
            Console.WriteLine("Вводите целые числа вашего массива: ");
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = ReadInt("");
            }
            return arr;
        }

        static int[] CreateRandomArray(int len)
        {
            int start = ReadInt("Введите нижнюю границу для создания случайных чисел: ");
            int end = ReadInt("Введите верхнюю границу для создания случайных чисел: ");
            if (start > end)
            {
                Console.WriteLine("Большее число станет верхним краем, а меньшее нижним. ");
                (start, end) = (end, start);
            }
            int[] arr = new int[len];
            for (int i = 0; i < len; i++)
            {
                arr[i] = random.Next(start, end);
            }
            return arr;
        }

        static void PrintArray(int[] arr, string message = "Вот ваш массив: ")
        {
            Console.Write(message);
            foreach (int item in arr)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{item} ");
            }
            Console.ResetColor();
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

        static int FindMax(int[] arr)
        {
            int max = arr[0];
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] > max)
                {
                    max = arr[i];
                }
            }
            return max;
        }

        static void InputArtificialElements(ref int[] arr, int len)
        {
            int[] temp = new int[len + arr.Length];
            int j = 0;
            Console.WriteLine("Вводите целые числа вашего массива: ");
            for (int i = 0; i < temp.Length; i++)
            {
                if (i < len)
                {
                    temp[i] = ReadInt("");
                }
                else
                {
                    temp[i] = arr[j++];
                }
            }
            Console.Clear();
            PrintArray(arr, "Изначальный массив: ");
            arr = temp;
        }
            
        static void InputRandomElements(ref int[] arr, int len)
        {
            int start = ReadInt("Введите нижнюю границу для создания случайных чисел: ");
            int end = ReadInt("Введите верхнюю границу для создания случайных чисел: ");
            if (start > end)
            {
                Console.WriteLine("Большее число станет верхним краем, а меньшее нижним. ");
                (start, end) = (end, start);
            }
            Console.Clear();
            PrintArray(arr, "Изначальный массив: ");
            int[] temp = new int[len + arr.Length];
            int j = 0;
            for (int i = 0; i < temp.Length; i++)
            {
                if (i < len)
                {
                    temp[i] = random.Next(start, end);
                }
                else
                {
                    temp[i] = arr[j++];
                }
            }
            arr = temp;
        }

        static int[] ReverseArray(int[] arr)
        {
            int left = 0;
            int right = arr.Length - 1;
            while (left < right)
            {
                (arr[left], arr[right]) = (arr[right], arr[left]);
                left++;
                right--;    
            }
            return arr;
        }

        static int FindFirstEven(int[] arr)
        {
            int firstEven = 1;
            int countNotEven = 0;
            for (int i = 0; firstEven % 2 != 0; i++)
            {
                if (countNotEven == arr.Length)
                    break;
                else
                {
                    firstEven = arr[i];
                    countNotEven++;

                }

            }
            return firstEven;
        }

        static int[] SortSimpleExhange(int[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                for (int j = 0; j < arr.Length - 1; j++)
                {
                    if (arr[j] >= arr[j + 1])
                    {
                        (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
                    }
                }
            }
            return arr;
        }

        static int[] SortHoara(int[] arr, int minIndexSubarray, int maxIndexSubarray)
        {
            if (minIndexSubarray  >= maxIndexSubarray)
            {
                return arr;
            }

            int pivotIndex = GetPivotIndex(arr, minIndexSubarray, maxIndexSubarray);
            SortHoara(arr, minIndexSubarray, pivotIndex - 1);
            SortHoara(arr, pivotIndex + 1, maxIndexSubarray);

            return arr;
        }

        static int GetPivotIndex(int[] arr, int minIndexSubarray, int pivotIndex)
        {
            int newPivotIndex = minIndexSubarray - 1;

            for (int i = minIndexSubarray; i < pivotIndex; i++)
            {
                if (arr[i] < arr[pivotIndex])
                {
                    newPivotIndex++;
                    (arr[newPivotIndex], arr[i]) = (arr[i], arr[newPivotIndex]);
                }
            } 
            newPivotIndex++;
            (arr[newPivotIndex], arr[pivotIndex]) = (arr[pivotIndex], arr[newPivotIndex]);
            return newPivotIndex;
        }

        static int[] SortInsertion(int[] arr, int step)
        {
            for (int i = step; i < arr.Length; i++)
            {
                int key = arr[i];
                int j;
                for (j = i; j >= step && arr[j - step] > key; j -= step)
                {
                    arr[j] = arr[j - step];
                }
                arr[j] = key;
            }
            return arr;
        }
       
        static int[] SortShell(int[] arr)
        {
            int len = arr.Length;
            for (int step = len/2; step > 0; step /= 2)
            {
               SortInsertion(arr, step);
            }
            return arr;
        }

        static int FindIndexMinElement(int[] arr, int startFind)
        {
            int min = startFind;
            for (int i = startFind + 1; i < arr.Length; i++)
            {
                if (arr[i] < arr[min])
                {
                    min = i;
                }
            }
            return min;
        }

        static int[] SortSelection(int[] arr)
        {
            int len = arr.Length - 1;
            for (int i = 0; i < len; i++)
            {
                int minIndex = FindIndexMinElement(arr, i);
                (arr[minIndex],arr[i]) = (arr[i], arr[minIndex]);
            }
            return arr;
        }




        static void Main(string[] args)
        {
            Console.Title = "Лабораторная работа #4, вариант 1";
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();

            string[] menu = { "Формирование нового массива", "Поиск максимального элемента",
                "Добавление элементов в начало массива", "Переворот массива", "Нахождение первого четного элемента", 
                "Сортировка массива", "Конец работы"};
            int choose = 0;
            int[] arr = new int[0];
            bool isNull = true;
            bool isExit = false;
            do
            {
                if (!isNull || isExit)
                    PrintArray(arr, "Ваш нынешний массив: ");
                isExit = false;
                PrintMenu(menu);
                choose = ReadInt("Введите номер команды: ", "Ошибка ввода номера команды!","Извините, но команды с таким номером нет.", min : 1 , max: menu.Length);
                switch (choose)
                {
                    case 1:
                        Console.Clear();
                        string[] insideFirstMenu = { "Создание массива с помощью ввода с клавиатуры",
                            "Создание массива с помощью генерирования случайных чисел", "Выход"};
                        PrintMenu(insideFirstMenu);
                        int choise1 = ReadInt(message:"Введите номер команды: ",errorMessageLine:"Команды с таким номером нет. " , max: insideFirstMenu.Length, min: 1);
                        switch (choise1)
                        {
                            case 1:
                                Console.Clear();
                                int len = ReadInt("Введите длину массива: ", "Ошибка ввода длины массива!", "Длина не может быть отрицательной!", 0);
                                arr = CreateArtificialArray(len);
                                Console.Clear();
                                PrintArray(arr);
                                isNull = false;
                                break;
                            case 2:
                                Console.Clear();
                                int lenn = ReadInt("Введите длину массива: ", "Ошибка ввода длины массива!", "Длина не может быть отрицтельной!", 0);
                                arr = CreateRandomArray(lenn);
                                Console.Clear();
                                PrintArray(arr);
                                isNull = false;
                                break;
                            case 3:
                                Console.Clear();
                                isExit = true;
                                break;
                        }
                        break;
                    case 2:
                        switch (isNull)
                        {
                            case true:
                                Console.WriteLine("Для начала необходимо создать массив.");
                                break;
                            case false:
                                Console.Clear();
                                Console.WriteLine($"Максимальный элемент вашего массива равен {FindMax(arr)}");
                                break;
                        }
                        break;
                    case 3:
                        switch (isNull)
                        {
                            case true:
                                Console.WriteLine("Для начала необходимо создать массив.");
                                break;
                            case false:
                                Console.Clear();
                                string[] insideSecondMenu = { "Добавление элементa(ов) вручную", "Добавление случайного элемента(ов)", "Выход" };
                                PrintMenu(insideSecondMenu);
                                int choise2 = ReadInt(message: "Введите номер команды: ", errorMessageLine: "Команды с таким номером нет.", max: insideSecondMenu.Length, min: 1);
                                switch (choise2)
                                {
                                    case 1:
                                        int number = ReadInt("Введите какое количество чисел вы хотите ввести: ", errorMessageLine: "Невозможно ввести такое количество чисел!", min: 1);
                                        InputArtificialElements(ref arr, number);
                                        PrintArray(arr, "Получившийся массив: ");
                                        break;
                                    case 2:
                                        int number1 = ReadInt("Введите какое количество чисел вы хотите ввести: ", errorMessageLine: "Невозможно ввести такое количество чисел!", min: 1);
                                        InputRandomElements(ref arr, number1);
                                        PrintArray(arr, "Получившийся массив: ");
                                        break;
                                    case 3:
                                        Console.Clear();
                                        isExit = true;
                                        break;
                                }
                                break;
                        }
                        break;
                    case 4:
                        switch (isNull)
                        {
                            case true:
                                Console.WriteLine("Для начала необходимо создать массив.");
                                break;
                            case false:
                                Console.Clear();
                                PrintArray(arr, "Изначальный массив: ");
                                PrintArray(ReverseArray(arr),"Перевернутый массив: " );
                                break;
                        }
                        break;
                    case 5:
                        switch (isNull)
                        {
                            case true:
                                Console.WriteLine("Для начала необходимо создать массив.");
                                break;
                            case false:
                                Console.Clear();
                                if (FindFirstEven(arr) % 2 != 0)
                                {
                                    Console.WriteLine("В вашем массиве нет четных чисел.");
                                }
                                else
                                {
                                    Console.WriteLine($"Первым четным элементом массива является: {FindFirstEven(arr)}");
                                }
                                break;
                        }
                        break;
                    case 6:
                        switch (isNull)
                        {
                            case true:
                                Console.WriteLine("Для начала необходимо создать массив.");
                                break;
                            case false:
                                Console.Clear();
                                PrintArray(arr, "Ваш нынешний массив: ");
                                string[] insideThirdMenu = {"Простой обмен (Bubblesort)","Сортировка Хоара (Quicksort)", 
                                    "Сортировка Шелла","Сортировка выбором", "Выход "};
                                PrintMenu(insideThirdMenu);
                                int choise3 = ReadInt(message: "Введите номер команды: ", errorMessageLine: "Команды с таким номером нет.", max: insideThirdMenu.Length, min: 1);
                                Console.Clear();
                                PrintArray(arr, "Изначальный массив: ");
                                switch (choise3)
                                {
                                    case 1:
                                        SortSimpleExhange(arr);
                                        PrintArray(arr, "Отсортированный массив: ");
                                        break;
                                    case 2:
                                        SortHoara(arr, 0, arr.Length - 1);
                                        PrintArray(arr, "Отсортированный массив: ");
                                        break;
                                    case 3:
                                        SortShell(arr);
                                        PrintArray(arr, "Отсортированный массив: ");
                                        break;
                                    case 4:
                                        SortSelection(arr);
                                        PrintArray(arr, "Отсортированный массив: ");
                                        break;
                                    case 5:
                                        Console.Clear();
                                        isExit = true;
                                        break;
                                }
                                break;
                        }
                        break;
                }
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Нажмите enter для продолжения...");
                Console.ReadLine();
                Console.Clear() ;
            }while (choose != menu.Length);
            Console.WriteLine("Завершаю работу...");


        }
    }
}
