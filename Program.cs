using System;
using System.Collections.Generic;

namespace TestLayers
{
    class Program
    {
        static private List<Material> materials = new List<Material>();

        static void Main(string[] args)
        {
            Console.WriteLine("Тестирование работы со слоями материалов");
            Console.WriteLine("Команды:");
            Console.WriteLine("\t[l, з] - загрузить материал");
            Console.WriteLine("\t[u, р] - выгрузить материал");
            Console.WriteLine("\t[q] - завершение работы");
            Console.Write(">>> ");

            char command;
            do
            {
                command = Console.ReadKey().KeyChar;
                Console.WriteLine();

                switch (command)
                {
                    case 'l':
                    case 'з':
                        // Console.WriteLine();
                        Load();
                        break;

                    case 'u':
                    case 'р':
                        // Console.WriteLine();
                        List<Material> unloaded = Unload();
                        break;
                    case 'q':
                        Console.WriteLine("Завершение работы приложения");
                        break;
                    default:
                        Console.WriteLine($"Неизвестная команда - {command}");
                        break;
                }

                Show();
                Console.Write(">>> ");
            } while (command != 'q');
        }

        static private void Load()
        {
            Console.Write("Наименование: ");
            string name = Console.ReadLine();
            Console.Write("         Вес: ");
            double weight;
            bool res = double.TryParse(Console.ReadLine(), out weight);
            if (name != "" && weight > 0)
            {
                materials.Add(new Material(name, weight));
            }
        }

        static private List<Material> Unload()
        {
            // Получаем количество слоев материала. Если материала нет, выдаем ошибку
            if (materials.Count == 0)
            {
                Console.WriteLine("Материал отсутствует, выгружать нечего");
                return null;
            }

            Console.Write("Вес выгружаемого материала: ");
            bool res = double.TryParse(Console.ReadLine(), out double weight);

            List<Material> unloaded = new List<Material>();
            if (res)
            {
                // Известен вес выгружаемого материала, начинаем выгружать

                // Пока остались слои материала и количество списываемого материала больше нуля
                while (materials.Count > 0 && weight > 0)
                {
                    List<Material> _materials = new List<Material>();
                    Material _material = materials[0]; // получаем первый слой материала

                    // Если вес материала в слое больше веса списываемого материала,
                    // то вес списываемого материала устанавливаем в ноль, а вес слоя уменьшаем на вес списываемого материала
                    if (_material.Weight > weight)
                    {
                        // Находим вес оставшегося материала на слое
                        _material.Weight = _material.Weight - weight;
                        materials[0] = _material;

                        // Добавляем в выгруженную часть слоя в список выгруженного материала
                        Material unload = _material;
                        unload.Weight = weight;
                        unloaded.Add(unload);
                        weight = 0;
                    }
                    else
                    {
                        // Если вес материала в слое меньше веса списываемого материала,
                        // то удаляем полностью слой и уменьшаем вес списываемого материала на вес, который был в слое
                        if (_material.Weight < weight)
                        {
                            weight -= _material.Weight;
                            unloaded.Add(materials[0]);
                            for (int i = 1; i < materials.Count; i++)
                            {
                                _materials.Add(materials[i]);
                            }
                            materials = _materials;

                        }
                        else
                        {
                            // Если вес материала в слое равен весу списываемого материала,
                            // то удаляем слой и устнавливаем вес списываемого материала равным нулю
                            if (_material.Weight == weight)
                            {
                                weight = 0;
                                unloaded.Add(materials[0]);
                                for (int i=1; i<materials.Count; i++)
                                {
                                    _materials.Add(materials[i]);
                                }
                                materials = _materials;
                            }
                        }
                    }
                }

                // Если вес списываемого материала больше нуля, а количество слоев равно нулю, 
                // то выдаем сообщение, что материал уже закончился, списывать больше нечего!
                if (materials.Count == 0 && weight > 0)
                {
                    Console.WriteLine($"Весь материал из бункера отгружен, не хватило {weight} тонн");
                }
            }

            return unloaded;
        }

        static private void Show()
        {
            if (materials != null && materials.Count > 0)
            {
                for (int i=0; i<materials.Count; i++)
                {
                    Console.WriteLine($"{i+1} - [{materials[i].Name}] : {materials[i].Weight}");
                }
            }
        }
    }
}
