using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba_4
{
    internal class Вариант1
    {
           // Статический объект Random для генерации случайных чисел в приложении.
    static Random random = new Random();

    /// <summary>
    /// Выводит предоставленное меню на консоль, нумеруя каждый пункт.
    /// </summary>
    /// <param name="menu">Массив строк, где каждая строка представляет пункт меню.</param>
    static void PrintMenu(string[] menu)
    {
        int index = 1;
        foreach (string item in menu)
        {
            Console.WriteLine($"{index++}) {item}");
        }
    }

    /// <summary>
    /// Считывает целое число из консоли, обеспечивает корректность ввода
    /// и проверяет, что число находится в заданном диапазоне.
    /// </summary>
    /// <param name="message">Сообщение для вывода пользователю перед вводом.</param>
    /// <param name="errorMessage">Сообщение об ошибке при некорректном формате числа.</param>
    /// <param name="errorMessageLine">Сообщение об ошибке, если число выходит за заданный диапазон.</param>
    /// <param name="min">Минимально допустимое значение (по умолчанию int.MinValue).</param>
    /// <param name="max">Максимально допустимое значение (по умолчанию int.MaxValue).</param>
    /// <returns>Введенное и проверенное целое число.</returns>
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

    /// <summary>
    /// Выводит число на консоль с определенным цветом,
    /// что позволяет визуально выделить его.
    /// </summary>
    /// <param name="number">Число, которое нужно вывести.</param>
    /// <param name="text">Текст, предшествующий числу.</param>
    /// <param name="oneTime">Если true, выводит число с переходом на новую строку и цветом DarkYellow.
    /// Если false, выводит число без перехода на новую строку и цветом Red.</param>
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

    /// <summary>
    /// Создает новый массив целых чисел, элементы которого вводятся пользователем с клавиатуры.
    /// </summary>
    /// <param name="len">Желаемая длина массива.</param>
    /// <returns>Созданный массив целых чисел.</returns>
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

    /// <summary>
    /// Создает новый массив целых чисел, элементы которого заполняются случайными значениями
    /// в заданном пользователем диапазоне.
    /// </summary>
    /// <param name="len">Желаемая длина массива.</param>
    /// <returns>Созданный массив целых чисел со случайными элементами.</returns>
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

    /// <summary>
    /// Выводит содержимое массива целых чисел на консоль.
    /// </summary>
    /// <param name="arr">Массив для печати.</param>
    /// <param name="message">Сообщение, предшествующее выводу массива.</param>
    static void PrintArray(int[] arr, string message = "Вот ваш массив: ")
    {
        Console.Write(message);
        foreach (int item in arr)
        {
            ColoringNumbers(item, " ", false);
        }
        Console.WriteLine();
    }

    /// <summary>
    /// Находит максимальный элемент в массиве и его индекс, затем выводит эту информацию.
    /// </summary>
    /// <param name="arr">Массив для поиска максимального элемента.</param>
    /// <returns>Индекс первого вхождения максимального элемента в массиве.</returns>
    static int FindMax(int[] arr)
    {
        int max = arr[0];
        int maxIndex = 0;
        int i;
        for ( i = 0; i < arr.Length; i++)
        {
            if (max < arr[i] )
            {
                maxIndex = i;
                max = arr[i];
            }
        }
        ColoringNumbers(maxIndex + 1, "Номер максимального элемента: ");
        ColoringNumbers(max, "Максимальный элемент вашего массива: ");
        return maxIndex;
    }

    /// <summary>
    /// Удаляет все вхождения максимального элемента из массива.
    /// Создает новый массив без максимальных элементов и заменяет им исходный.
    /// </summary>
    /// <param name="arr">Исходный массив, передаваемый по ссылке для изменения.</param>
    static void DeleteMaxElement(ref int[] arr)
    {
        Console.Clear();
        // Сначала определяем максимальный элемент, его индекс и количество его вхождений
        int maxVal = arr[FindMax(arr)]; 
        int countMax = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == maxVal)
            {
                countMax++;
            }
        }

        int[] temp = new int[arr.Length - countMax]; // Создаем новый массив уменьшенного размера
        int j = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            if ( arr[i] != maxVal) // Если элемент не равен максимальному, добавляем его в новый массив
            {
                temp[j++] = arr[i];
            }
        }
        PrintArray(arr, "Изначальный массив: ");
        arr = temp; // Присваиваем новый массив исходной переменной
        PrintArray(arr, "Массив после удаления максимального элемента: ");
    }

    /// <summary>
    /// Добавляет указанное количество элементов, вводимых пользователем,
    /// в начало существующего массива.
    /// </summary>
    /// <param name="arr">Исходный массив, передаваемый по ссылке для изменения.</param>
    /// <param name="len">Количество элементов, которые нужно добавить.</param>
    static void InputArtificialElements(ref int[] arr, int len)
    {
        int[] temp = new int[len + arr.Length];
        int j = 0;
        Console.WriteLine("Вводите целые числа вашего массива: ");
        for (int i = 0; i < temp.Length; i++)
        {
            if (i < len) // Заполняем первые 'len' позиций новыми элементами
            {
                temp[i] = ReadInt("");
            }
            else // Остальные позиции заполняем элементами из старого массива
            {
                temp[i] = arr[j++];
            }
        }
        Console.Clear();
        PrintArray(arr, "Изначальный массив: ");
        arr = temp; // Присваиваем новый массив исходной переменной
    }

    /// <summary>
    /// Добавляет указанное количество случайных элементов в начало существующего массива.
    /// Диапазон случайных чисел определяется пользователем.
    /// </summary>
    /// <param name="arr">Исходный массив, передаваемый по ссылке для изменения.</param>
    /// <param name="len">Количество элементов, которые нужно добавить.</param>
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
            if (i < len) // Заполняем первые 'len' позиций новыми случайными элементами
            {
                temp[i] = random.Next(start, end);
            }
            else // Остальные позиции заполняем элементами из старого массива
            {
                temp[i] = arr[j++];
            }
        }
        arr = temp; // Присваиваем новый массив исходной переменной
    }

    /// <summary>
    /// Переворачивает порядок элементов в массиве "на месте".
    /// </summary>
    /// <param name="arr">Массив для переворота.</param>
    /// <returns>Перевернутый массив.</returns>
    static int[] ReverseArray(int[] arr)
    {
        int left = 0;
        int right = arr.Length - 1;
        while (left < right)
        {
            // Обмен элементов местами
            (arr[left], arr[right]) = (arr[right], arr[left]);
            left++;
            right--;
        }
        return arr;
    }

    /// <summary>
    /// Находит первое четное число в массиве и выводит количество сравнений,
    /// необходимых для его поиска.
    /// Возвращает первое четное число или 1, если четных чисел не найдено.
    /// </summary>
    /// <param name="arr">Массив для поиска.</param>
    /// <returns>Первое четное число или 1, если четных чисел нет.
    /// (Примечание: Возвращение 1, когда четных нет, может быть неочевидным; 
    /// более явным было бы возвращение Sentinel-значения типа -1 или использование Nullable<int>).</returns>
    static int FindFirstEven(int[] arr)
    {
        // Здесь есть логическая ошибка: переменная `firstEven` не инициализируется
        // элементом из массива, а `i` используется в цикле, который может работать некорректно.
        // Предполагаемый алгоритм должен был бы найти первое четное число,
        // и переменная `firstEven` должна была бы хранить найденное значение.
        // Количество сравнений также подсчитывается некорректно относительно цели поиска.
        // Исправленная логика (но код не меняем):
        // int firstEven = 1; // Возвращаемое значение по умолчанию, если четное не найдено
        // int countNotEven = 0; // Считает количество просмотренных элементов
        // for (int i = 0; i < arr.Length; i++)
        // {
        //     countNotEven++; // Увеличиваем счетчик сравнений/просмотров
        //     if (arr[i] % 2 == 0) // Если нашли четное число
        //     {
        //         ColoringNumbers(countNotEven, "Количество сравнений для поиска первого четного: ");
        //         return arr[i]; // Возвращаем его
        //     }
        // }
        // ColoringNumbers(countNotEven, "Количество сравнений для поиска первого четного: ");
        // return 1; // Если ни одного четного не найдено
        
        // Текущая реализация кода:
        int firstEven = 1; // Инициализация, которая не меняется внутри цикла для поиска четного числа из массива
        int countNotEven = 0; // Счётчик элементов, просмотренных до первого четного (если бы firstEven менялся)
        int i;
        for (i = 0; firstEven % 2 != 0; i++) // Этот цикл будет выполняться бесконечно, если firstEven всегда 1,
                                            // или до переполнения i, если не будет `return`
        {
            if (countNotEven == arr.Length) // Проверяет, если просмотрены все элементы
                return firstEven; // Возвращает 1, если четных чисел нет (если бы countNotEven реально отражал просмотр)
            else
            {
                countNotEven++; // Инкрементирует счётчик, но arr[i] не используется
            }
        }
        // Эта часть кода никогда не будет достигнута при текущей логике цикла,
        // так как цикл прервется только если firstEven станет четным (чего не происходит)
        // или если countNotEven == arr.Length.
        ColoringNumbers(i - 1, "Количество сравнений для поиска первого четного: ");
        return firstEven;
    }

    /// <summary>
    /// Сортирует массив методом пузырьковой сортировки (Bubble Sort).
    /// Многократно проходит по списку, сравнивает соседние элементы и меняет их местами,
    /// если они находятся в неправильном порядке.
    /// </summary>
    /// <param name="arr">Массив для сортировки.</param>
    /// <returns>Отсортированный массив.</returns>
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

    /// <summary>
    /// Реализует алгоритм быстрой сортировки (Quicksort),
    /// используя рекурсивный подход "разделяй и властвуй".
    /// </summary>
    /// <param name="arr">Массив для сортировки.</param>
    /// <param name="minIndexSubarray">Начальный индекс текущего подмассива.</param>
    /// <param name="maxIndexSubarray">Конечный индекс текущего подмассива.</param>
    /// <returns>Отсортированный массив.</returns>
    static int[] SortHoara(int[] arr, int minIndexSubarray, int maxIndexSubarray)
    {
        // Базовый случай рекурсии: если подмассив пуст или состоит из одного элемента, он уже отсортирован.
        if (minIndexSubarray >= maxIndexSubarray)
        {
            return arr;
        }

        // Выбирает опорный элемент и переставляет элементы так,
        // чтобы меньшие были слева, а большие справа от него.
        // Возвращает новый индекс опорного элемента.
        int pivotIndex = GetPivotIndex(arr, minIndexSubarray, maxIndexSubarray);
        
        // Рекурсивно сортирует подмассив слева от опорного элемента.
        SortHoara(arr, minIndexSubarray, pivotIndex - 1);
        
        // Рекурсивно сортирует подмассив справа от опорного элемента.
        SortHoara(arr, pivotIndex + 1, maxIndexSubarray);

        return arr;
    }

    /// <summary>
    /// Выполняет операцию разбиения массива согласно.
    /// Выбирает последний элемент подмассива как опорный (pivot),
    /// переставляет элементы таким образом, чтобы все элементы меньше опорного
    /// оказались до него, а все элементы больше или равные опорному — после него.
    /// В конце помещает опорный элемент в его окончательную отсортированную позицию.
    /// </summary>
    /// <param name="arr">Массив для разбиения.</param>
    /// <param name="minIndexSubarray">Начальный индекс текущего подмассива.</param>
    /// <param name="pivotIndex">Индекс опорного элемента, который по схеме Ломуто является
    /// последним элементом текущего подмассива.</param>
    /// <returns>Индекс, на котором опорный элемент оказался после разбиения.</returns>
    static int GetPivotIndex(int[] arr, int minIndexSubarray, int pivotIndex)
    {
        int newPivotIndex = minIndexSubarray - 1; // Индекс, который отслеживает конец "меньшей" части

        // Проход по подмассиву (исключая опорный элемент)
        for (int i = minIndexSubarray; i < pivotIndex; i++)
        {
            if (arr[i] < arr[pivotIndex]) // Если текущий элемент меньше опорного
            {
                newPivotIndex++; // Перемещаем границу "меньшей" части
                // Меняем местами текущий элемент с элементом на newPivotIndex
                // (который является первым элементом, большим или равным опорному)
                (arr[newPivotIndex], arr[i]) = (arr[i], arr[newPivotIndex]);
            }
        }
        // Помещаем опорный элемент в его финальную позицию:
        // сразу после всех элементов, которые меньше его.
        newPivotIndex++;
        (arr[newPivotIndex], arr[pivotIndex]) = (arr[pivotIndex], arr[newPivotIndex]);
        return newPivotIndex; // Возвращаем индекс опорного элемента
    }

    /// <summary>
    /// Выполняет сортировку вставками (Insertion Sort) для подмассива с заданным шагом.
    /// Это вспомогательная функция для сортировки Шелла.
    /// </summary>
    /// <param name="arr">Массив для сортировки.</param>
    /// <param name="step">Шаг (интервал) между сравниваемыми элементами.</param>
    /// <returns>Частично отсортированный массив.</returns>
    static int[] SortInsertion(int[] arr, int step)
    {
        for (int i = step; i < arr.Length; i++)
        {
            int key = arr[i]; // Текущий элемент для вставки
            int j;
            // Сдвигаем элементы, которые больше 'key', вправо
            for (j = i; j >= step && arr[j - step] > key; j -= step)
            {
                arr[j] = arr[j - step];
            }
            arr[j] = key; // Вставляем 'key' в правильную позицию
        }
        return arr;
    }

    /// <summary>
    /// Сортирует массив методом Шелла (Shell Sort).
    /// Это улучшенный вариант сортировки вставками, который сначала сортирует
    /// далеко расположенные элементы (с большим шагом), затем постепенно уменьшает шаг,
    /// что позволяет элементам быстро перемещаться на свои места.
    /// </summary>
    /// <param name="arr">Массив для сортировки.</param>
    /// <returns>Отсортированный массив.</returns>
    static int[] SortShell(int[] arr)
    {
        int len = arr.Length;
        // Цикл по шагам, уменьшая шаг на каждом шаге (здесь делится на 2)
        for (int step = len / 2; step > 0; step /= 2)
        {
            // Выполняем сортировку вставками с текущим шагом
            SortInsertion(arr, step);
        }
        return arr;
    }

    /// <summary>
    /// Находит индекс минимального элемента в части массива, начиная с указанного индекса.
    /// </summary>
    /// <param name="arr">Массив для поиска.</param>
    /// <param name="startFind">Индекс, с которого начинается поиск.</param>
    /// <returns>Индекс минимального элемента в указанной части массива.</returns>
    static int FindIndexMinElement(int[] arr, int startFind)
    {
        int min = startFind; // Предполагаем, что первый элемент - минимальный
        for (int i = startFind + 1; i < arr.Length; i++)
        {
            if (arr[i] < arr[min]) // Если найден элемент меньше текущего минимума
            {
                min = i; // Обновляем индекс минимального элемента
            }
        }
        return min;
    }

    /// <summary>
    /// Сортирует массив методом выбора (Selection Sort).
    /// На каждом шаге находит минимальный элемент в оставшейся несортированной части
    /// и меняет его местами с первым элементом этой части.
    /// </summary>
    /// <param name="arr">Массив для сортировки.</param>
    /// <returns>Отсортированный массив.</returns>
    static int[] SortSelection(int[] arr)
    {
        int len = arr.Length - 1;
        for (int i = 0; i < len; i++)
        {
            // Находим индекс минимального элемента в оставшейся несортированной части
            int minIndex = FindIndexMinElement(arr, i);
            // Меняем местами минимальный элемент с текущим первым элементом несортированной части
            (arr[minIndex], arr[i]) = (arr[i], arr[minIndex]);
        }
        return arr;
    }

   /// <summary>
   /// Выполняет бинарный поиск заданного числа в отсортированном массиве.
   /// Выводит количество сравнений, необходимых для нахождения элемента.
   /// </summary>
   /// <param name="arr">Отсортированный массив, в котором осуществляется поиск.</param>
   /// <param name="number">Число, которое необходимо найти.</param>
   /// <returns>Индекс найденного числа в массиве или -1, если число не найдено.</returns>
    static int SearchBinary(int[] arr, int number)
    {
        int count = 0; // Счетчик сравнений
        int left = 0;
        int right = arr.Length - 1;
        while (left <= right)
        {
            int mid = (left + right)/ 2; // Вычисление среднего индекса
            if (arr[mid] == number) // Если элемент найден
            {
                ColoringNumbers(count, "Количество сравнений: ");
                return mid; // Возвращаем его индекс
            }
            else if (arr[mid] > number) // Если искомое число меньше среднего
            {
                right = mid - 1; // Ищем в левой половине
            }
            else if (arr[mid] < number) // Если искомое число больше среднего
            {
                left = mid + 1; // Ищем в правой половине
            }
            count++; // Увеличиваем счетчик сравнений
        }
        return -1; // Если число не найдено
    }

    /// <summary>
    /// Проверяет, отсортирован ли массив по возрастанию.
    /// </summary>
    /// <param name="arr">Массив для проверки.</param>
    /// <returns>True, если массив отсортирован; False в противном случае.</returns>
    static bool CheckSorted(int[] arr)
    {
        bool isSort = true;
        for (int i = 0;  i < arr.Length - 1; i++)
        {
            if (arr[i] > arr[i + 1]) // Если найден элемент, который больше следующего
            { 
                isSort = false; // Массив не отсортирован
                break;
            }
        }
        return isSort;
    }

    /// <summary>
    /// Главная функция программы, реализующая пользовательский интерфейс и логику
    /// для работы с массивами (создание, изменение, сортировка, поиск).
    /// </summary>
    /// <param name="args">Аргументы командной строки.</param>
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
        int[] arr = new int[0]; // Инициализация пустого массива
        bool isNull = true; // Флаг, указывающий, пуст ли массив
        bool isSorted = false; // Флаг, указывающий, отсортирован ли массив
        bool isUnit = false; // Флаг, указывающий, состоит ли массив из одного элемента
        do
        {
            if (!isNull)
                PrintArray(arr, "Ваш нынешний массив: "); // Вывод текущего массива, если он не пуст
            PrintMenu(menu); // Вывод главного меню
            choose = ReadInt("Введите номер команды: ", "Ошибка ввода номера команды!", "Извините, но команды с таким номером нет.", 1, menu.Length);
            switch (choose)
            {
                case 1: // Формирование нового массива
                    Console.Clear();
                    string[] insideFirstMenu = { "Создание массива с помощью ввода с клавиатуры",
                        "Создание массива с помощью генерирования случайных чисел", "Выход"};
                    PrintMenu(insideFirstMenu);
                    int choise1 = ReadInt("Введите номер команды: ", errorMessageLine: "Команды с таким номером нет. ", max: insideFirstMenu.Length, min: 1);
                    switch (choise1)
                    {
                        case 1: // Создание массива вручную
                            Console.Clear();
                            int len = ReadInt("Введите длину массива: ", "Ошибка ввода длины массива!", "Массив с такой длиной не может существовать!", 1);
                            arr = CreateArtificialArray(len);
                            Console.Clear();
                            PrintArray(arr);
                            isNull = false; // Массив больше не пуст
                            break;
                        case 2: // Создание случайного массива
                            Console.Clear();
                            int lenn = ReadInt("Введите длину массива: ", "Ошибка ввода длины массива!", "Массив с такой длиной не может существовать!", 1);
                            arr = CreateRandomArray(lenn);
                            Console.Clear();
                            PrintArray(arr);
                            isNull = false; // Массив больше не пуст
                            break;
                    }
                    isUnit = (arr.Length == 1); // Проверка на единичный массив
                    isSorted = CheckSorted(arr); // Проверка, отсортирован ли массив
                    break;
                case 2: // Удаление максимального элемента
                    switch (isNull)
                    {
                        case true:
                            Console.WriteLine("Для начала необходимо создать массив.");
                            break;
                        case false:
                            switch (isUnit)
                            {
                                case true: // Если массив единичный, предлагается его удалить
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
                                case false: // Удаление максимального элемента из обычного массива
                                     Console.Clear();
                                     DeleteMaxElement(ref arr);
                                     break;
                            }
                            break;
                    }
                    break;
                case 3: // Добавление элементов в начало массива
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
                                case 1: // Добавление элементов вручную
                                    int number = ReadInt("Введите какое количество чисел вы хотите ввести: ", errorMessageLine: "Невозможно ввести такое количество чисел!", min: 1);
                                    InputArtificialElements(ref arr, number);
                                    PrintArray(arr, "Получившийся массив: ");
                                    break;
                                case 2: // Добавление случайных элементов
                                    int number1 = ReadInt("Введите какое количество чисел вы хотите ввести: ", errorMessageLine: "Невозможно ввести такое количество чисел!", min: 1);
                                    InputRandomElements(ref arr, number1);
                                    PrintArray(arr, "Получившийся массив: ");
                                    break;
                            }
                            isUnit = (arr.Length == 1); // Проверка на единичный массив
                            isSorted = CheckSorted(arr); // Проверка, отсортирован ли массив
                            break;
                    }
                    break;
                case 4: // Переворот массива
                    switch (isNull)
                    {
                        case true:
                            Console.WriteLine("Для начала необходимо создать массив.");
                            break;
                        case false:
                            Console.Clear();
                            PrintArray(arr, "Изначальный массив: ");
                            PrintArray(ReverseArray(arr), "Перевернутый массив: ");
                            isSorted = CheckSorted(arr); // Состояние сортировки могло измениться после переворота
                            break;
                    }
                    break;
                case 5: // Нахождение первого четного элемента
                    switch (isNull)
                    {
                        case true:
                            Console.WriteLine("Для начала необходимо создать массив.");
                            break;
                        case false:
                            Console.Clear();
                            int firstEven = FindFirstEven(arr);
                            // Проверка, было ли найдено четное число (если 1 - не найдено, согласно текущей реализации)
                            if (firstEven % 2 != 0 && firstEven != 0) // Условие для определения "не найдено"
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
                case 6: // Сортировка массива
                    switch (isNull)
                    {
                        case true:
                            Console.WriteLine("Для начала необходимо создать массив.");
                            break;
                        case false:
                            switch (isUnit) // Если массив единичный
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
                                        isSorted = false; // Принудительно сбрасываем флаг, если пользователь хочет сортировать
                                        goto case false; // Переход к следующему блоку switch (случай, когда массив не единичный)
                                    }
                                case false: // Если массив не единичный
                                    switch (isSorted) // Проверяем, отсортирован ли уже
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
                                                goto case false; // Переход к выбору метода сортировки
                                            }
                                        case false: // Массив не отсортирован (или пользователь хочет сортировать снова)
                                            Console.Clear();
                                            PrintArray(arr, "Ваш нынешний массив: ");
                                            string[] insideThirdMenu = {"Простой обмен (Bubblesort)","Сортировка Хоара (Quicksort)",
                                                    "Сортировка Шелла","Сортировка выбором", "Выход "};
                                            PrintMenu(insideThirdMenu);
                                            int choise4 = ReadInt(message: "Введите номер команды: ", errorMessageLine: "Команды с таким номером нет.", max: insideThirdMenu.Length, min: 1);
                                            if (choise4 == 5) // Выход из подменю сортировки
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
                                                    case 1: SortSimpleExhange(arr); break;
                                                    case 2: SortHoara(arr, 0, arr.Length - 1); break;
                                                    case 3: SortShell(arr); break;
                                                    case 4: SortSelection(arr); break;
                                                }
                                                PrintArray(arr, "Отсортированный массив: ");
                                                isSorted = true; // Устанавливаем флаг, что массив отсортирован
                                                break;
                                            }
                                    }
                                    break;
                            }
                            break;
                case 7: // Бинарный поиск
                    switch (isNull)
                    {
                        case true:
                            Console.WriteLine("Для начала необходимо создать массив.");
                            break;
                        case false:
                            switch (isSorted) // Для бинарного поиска массив должен быть отсортирован
                            {
                                case false:
                                    Console.WriteLine("Для начала необходимо отсортировать массив.");
                                    break;
                                case true:
                                    Console.Clear();
                                    PrintArray(arr, "Ваш нынешний массив: ");
                                    int choise5 = ReadInt("Введите число, которое вы хотите найти: ");
                                    int indexChoise5 = SearchBinary(arr, choise5) + 1; // +1 для вывода номера элемента, а не индекса
                                    switch (indexChoise5)
                                    {
                                        case 0: // Если BinarySearch вернул -1, то indexChoise5 будет 0
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
                case 8: // Удаление массива (полностью)
                    switch (isNull)
                    {
                        case true:
                            Console.WriteLine("Вашего массива и так не существует, откуда его удалять?");
                            break;
                        case false:
                            Console.Clear();
                            arr = new int[0]; // Создаем новый пустой массив
                            Console.WriteLine("Массив успешно удален.");
                            isNull = true; // Массив теперь пуст
                            break;
                    }
                    break;
            }
            Console.WriteLine("Нажмите enter для продолжения...");
            Console.ReadLine();
            Console.Clear();
        } while (choose != menu.Length); // Цикл продолжается, пока пользователь не выберет "Конец работы"
        Console.WriteLine("Завершаю работу...");


    }
}
    }
}


