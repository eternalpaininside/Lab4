using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba_4
{
    internal class Вариант1
    {
        static Random random = new Random();

        static void PrintMenu(string[] menu)
        {
            int index = 1;
            foreach (string item in menu)
            {
                Console.WriteLine($"{index++}) {item}");
            }
        }

        static int ReadInt(string message = "Введите целое число ", string errorMessage = "Ошибка ввода целого числа!", string errorMessageLine = "Ваше число слишком большое/малое", int min = int.MinValue, int max = int.MaxValue)
        {
            bool isConvert;
            int number;
            do
            {
                Console.Write(message);
                Console.ForegroundColor = ConsoleColor.Green;
                isConvert = int.TryParse(Console.ReadLine(), out number);
                Console.ForegroundColor = ConsoleColor.White;
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

        static void ColoringNumbers(int number, string text, bool oneTime = true)
        {
            if (oneTime)
            {
                Console.Write(text);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(number);
            }
            else
            {
                Console.Write(text);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(number);
            }
            Console.ForegroundColor = ConsoleColor.White;
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
                Console.ReadLine();
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
                ColoringNumbers(item, " ", false);
            }
            Console.WriteLine();
        }

        static int FindMax(int[] arr)
        {
            int max = arr[0];
            int maxIndex = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (max < arr[i] )
                {
                    maxIndex = i;
                    max = arr[i];
                }
            }
            ColoringNumbers(maxIndex + 1, "Номер максимального элемента: ");
            return max;
        }

        static void DeleteMaxElement(ref int[] arr)
        {
            Console.Clear();
            int[] temp = new int[arr.Length - 1];
            int max = FindMax(arr);
            int j = 0;
            bool isFirstMax = false;
            for (int i = 0; i < arr.Length; i++)
            {
                if (isFirstMax)
                {
                    temp[j++] = arr[i];
                }
                else if (max == arr[i])
                {
                    isFirstMax = true;
                }
                else if (max != arr[i])
                {
                    temp[j++] = arr[i];
                }
            }
            PrintArray(arr, "Изначальный массив: ");
            arr = temp;
            PrintArray(arr, "Массив после удаления максимального элемента: ");
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
                Console.ReadLine();
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
            int i;
            for (i = 0; firstEven % 2 != 0; i++)
            {
                if (countNotEven == arr.Length)
                    return firstEven;
                else
                {
                    firstEven = arr[i];
                    countNotEven++;

                }

            }
            ColoringNumbers(i - 1, "Количество сравнений для поиска первого четного: ");
            return firstEven;
        }

        static int[] SortSimpleExhange(int[] arr)
        {
            bool isSwapped = false;
            for (int i = 0; i < arr.Length - 1; i++)
            {
                for (int j = 0; j < arr.Length - 1; j++)
                {
                    if (arr[j] >= arr[j + 1])
                    {
                        (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
                        isSwapped = true;
                    }
                    if (!isSwapped)
                    {
                        return arr;
                    }
                }
            }
            return arr;
        }

        static int[] SortHoara(int[] arr, int minIndexSubarray, int maxIndexSubarray)
        {
            if (minIndexSubarray >= maxIndexSubarray)
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
            for (int step = len / 2; step > 0; step /= 2)
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
                (arr[minIndex], arr[i]) = (arr[i], arr[minIndex]);
            }
            return arr;
        }

       static int BinarySearch(int[] arr, int number)
        {
            int left = 0;
            int right = arr.Length - 1;
            while (left <= right)
            {
                int mid = (left + right)/ 2;
                if (arr[mid] == number)
                {
                    return mid;
                }
                else if (arr[mid] > number)
                {
                    right = mid - 1;
                }
                else if (arr[mid] < number)
                {
                    left = mid + 1;
                }
            }
            return -1;
        }

        static bool CheckSorted(int[] arr)
        {
            bool isSort = true;
            for (int i = 0;  i < arr.Length - 1; i++)
            {
                if (arr[i] > arr[i + 1])
                { 
                    isSort = false; 
                    break;
                }
            }
            return isSort;

        }

        static void Main(string[] args)
        {
            Console.Title = "Лабораторная работа #4, вариант 1";
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();

            string[] menu = { "Формирование нового массива", "Удаление максимального элемента",
                "Добавление элементов в начало массива", "Переворот массива", "Нахождение первого четного элемента",
                "Сортировка массива","Бинарный поиск" ,"Удаление массива","Конец работы"};
            int choose = 0;
            int[] arr = new int[0];
            bool isNull = true;
            bool isSorted = false;
            bool isUnit = false;
            do
            {
                if (!isNull)
                    PrintArray(arr, "Ваш нынешний массив: ");
                PrintMenu(menu);
                choose = ReadInt("Введите номер команды: ", "Ошибка ввода номера команды!", "Извините, но команды с таким номером нет.", 1, menu.Length);
                switch (choose)
                {
                    case 1:
                        Console.Clear();
                        string[] insideFirstMenu = { "Создание массива с помощью ввода с клавиатуры",
                            "Создание массива с помощью генерирования случайных чисел", "Выход"};
                        PrintMenu(insideFirstMenu);
                        int choise1 = ReadInt("Введите номер команды: ", errorMessageLine: "Команды с таким номером нет. ", max: insideFirstMenu.Length, min: 1);
                        switch (choise1)
                        {
                            case 1:
                                Console.Clear();
                                int len = ReadInt("Введите длину массива: ", "Ошибка ввода длины массива!", "Массив с такой длиной не может существовать!", 1);
                                arr = CreateArtificialArray(len);
                                Console.Clear();
                                PrintArray(arr);
                                isNull = false;
                                break;
                            case 2:
                                Console.Clear();
                                int lenn = ReadInt("Введите длину массива: ", "Ошибка ввода длины массива!", "Массив с такой длиной не может существовать!", 1);
                                arr = CreateRandomArray(lenn);
                                Console.Clear();
                                PrintArray(arr);
                                isNull = false;
                                break;
                        }
                        isUnit = (arr.Length == 1);
                        isSorted = CheckSorted(arr);
                        break;
                    case 2:
                        switch (isNull)
                        {
                            case true:
                                Console.WriteLine("Для начала необходимо создать массив.");
                                break;
                            case false:
                                switch (isUnit)
                                {
                                    case true:
                                        Console.Clear();
                                        Console.WriteLine("Ваш массив единичный, удалить массив: ");
                                        string[] insideSixthMenu = { "Удалить массив", "Оставить массив" };
                                        PrintMenu(insideSixthMenu);
                                        int isDelete = ReadInt("Введите номер команды: ", errorMessageLine: "Команды с таким номером нет.", min: 1, max: 2);
                                        switch (isDelete)
                                        {
                                            case 1:
                                                Console.Clear();
                                                Console.WriteLine("Массив успешно удален.");
                                                arr = new int[0];
                                                isNull = true;
                                                break;
                                        }
                                        break;
                                    case false:
                                        switch (isSorted)
                                        {
                                            case true:
                                                Console.Clear();
                                                ColoringNumbers(arr.Length, "Номер максимального элемента: ");
                                                ColoringNumbers(arr[arr.Length-1], "Максимальный элемент вашего массива: ");
                                                break;
                                            case false:
                                                Console.Clear();
                                                DeleteMaxElement(ref arr);
                                                break;
                                        }
                                        break;
                                }
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
                                string[] insideSecondMenu = { "Добавление элементa(ов) вручную", "Добавление случайного(ых) элемента(ов)", "Выход" };
                                PrintMenu(insideSecondMenu);
                                int choise2 = ReadInt("Введите номер команды: ", errorMessageLine: "Команды с таким номером нет.", max: insideSecondMenu.Length, min: 1);
                                Console.Clear();
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
                                }
                                isUnit = (arr.Length == 1);
                                isSorted = CheckSorted(arr);
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
                                PrintArray(ReverseArray(arr), "Перевернутый массив: ");
                                isSorted = CheckSorted(arr);
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
                                int firstEven = FindFirstEven(arr);
                                if (firstEven % 2 != 0)
                                {
                                    Console.WriteLine("В вашем массиве нет четных чисел.");
                                }
                                else
                                {
                                    ColoringNumbers(firstEven, "Первым четным элементом массива является: ");
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
                                switch (isUnit)
                                {
                                    case true:
                                        Console.Clear();
                                        Console.WriteLine("Единичный массив уже считается отсортированным, хотите продолжить?");
                                        string[] fifthInsideMenu = { "Хочу продолжить", "Хочу вернуться в основное меню " };
                                        PrintMenu(fifthInsideMenu);
                                        int choise6 = ReadInt("Введите номер команды ", "Ошибка ввода номера команды!", "Команды с таким номером нет. ", 1, fifthInsideMenu.Length);
                                        if (choise6 == 2)
                                            break;
                                        else
                                        {
                                            isSorted = false;
                                            goto case false;
                                        }
                                    case false:
                                        switch (isSorted)
                                        {
                                            case true:
                                                Console.Clear();
                                                Console.WriteLine("Ваш массив уже отсортирован, действительно хотите продолжить?");
                                                string[] forthInsideMenu = { "Хочу отсортировать еще раз ", "Хочу вернуться в основное меню " };
                                                PrintMenu(forthInsideMenu);
                                                int choise3 = ReadInt("Введите номер команды ", "Ошибка ввода номера команды!", "Команды с таким номером нет. ", 1, forthInsideMenu.Length);
                                                if (choise3 == 2)
                                                    break;
                                                else
                                                {
                                                    goto case false;
                                                }
                                            case false:
                                                Console.Clear();
                                                PrintArray(arr, "Ваш нынешний массив: ");
                                                string[] insideThirdMenu = {"Простой обмен (Bubblesort)","Сортировка Хоара (Quicksort)",
                                                        "Сортировка Шелла","Сортировка выбором", "Выход "};
                                                PrintMenu(insideThirdMenu);
                                                int choise4 = ReadInt(message: "Введите номер команды: ", errorMessageLine: "Команды с таким номером нет.", max: insideThirdMenu.Length, min: 1);
                                                if (choise4 == 5)
                                                {
                                                    Console.Clear();
                                                    break;
                                                }
                                                else
                                                {
                                                    Console.Clear();
                                                    PrintArray(arr, "Изначальный массив: ");
                                                    switch (choise4)
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
                                                    }
                                                    isSorted = true;
                                                    break;
                                                }
                                        }
                                        break;
                                    

                                }
                                break;
                                    
                        }
                        break;
                    case 7:
                        switch (isNull)
                        {
                            case true:
                                Console.WriteLine("Для начала необходимо создать массив.");
                                break;
                            case false:
                                switch (isSorted)
                                {
                                    case false:
                                        Console.WriteLine("Для начала необходимо отсортировать массив.");
                                        break;
                                    case true:
                                        Console.Clear();
                                        PrintArray(arr, "Ваш нынешний массив: ");
                                        int choise5 = ReadInt("Введите число, которое вы хотите найти: ");
                                        int indexChoise5 = BinarySearch(arr, choise5) + 1;
                                        switch (indexChoise5)
                                        {
                                            case 0:
                                                Console.WriteLine("Такого элемента нет в массиве.");
                                                break;
                                            default:
                                                ColoringNumbers(indexChoise5, "Номер элемента, который вы хотите найти, равен ");
                                                break;
                                        }
                                        break;

                                }
                                break;
                        }
                        break;
                    case 8:
                        switch (isNull)
                        {
                            case true:
                                Console.WriteLine("Вашего массива и так не существует, откуда его удалять?");
                                break;
                            case false:
                                Console.Clear();
                                arr = new int[0];
                                Console.WriteLine("Массив успешно удален.");
                                isNull = true;
                                break;
                        }
                        break;
                            
                }
                Console.WriteLine("Нажмите enter для продолжения...");
                Console.ReadLine();
                Console.Clear();
            } while (choose != menu.Length);
            Console.WriteLine("Завершаю работу...");


        }
    }
}
