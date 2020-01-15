using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using TechProgWin;
public class MultiLevelDock
{
    List<Dock<ITransport, IEngine>> dockStages;

    private const int countPlaces = 20;
   
    /// <summary>
    /// Ширина окна отрисовки
    /// </summary>
    private int pictureWidth;
    /// <summary>
    /// Высота окна отрисовки
    /// </summary>
    private int pictureHeight;

    public MultiLevelDock(int countStages, int pictureWidth, int pictureHeight)
    {
        dockStages = new List<Dock<ITransport, IEngine>>();
        this.pictureWidth = pictureWidth;
        this.pictureHeight = pictureHeight;
        for (int i = 0; i < countStages; ++i)
        {
            dockStages.Add(new Dock<ITransport, IEngine>(countPlaces, pictureWidth,
           pictureHeight));
        }
    }

    public Dock<ITransport, IEngine> this[int index]
    {
        get
        {
            if (index > -1 && index < dockStages.Count)
            {
                return dockStages[index];
            }
            return null;
        }
    }

    public ITransport this[int ind, int key]
    {
        get
        {
            if (ind > -1 && ind < dockStages.Count)
            {
                return dockStages[ind].GetPlaneByKey(key);
            }
            return null;
        }
    }

    /// <summary>
    /// Сохранение информации по автомобилям на парковках в файл
    /// </summary>
    /// <param name="filename">Путь и имя файла</param>
    /// <returns></returns>
    public bool SaveData(string filename)
    {
        if (File.Exists(filename))
        {
            File.Delete(filename);
        }
        
        using (StreamWriter sw = new StreamWriter(filename))
        {
            //Записываем количество уровней
            sw.WriteLine("CountLeveles:" + dockStages.Count);
            foreach (var level in dockStages)
            {
                //Начинаем уровень
                sw.WriteLine("Level");

                for (int i = 0; i < countPlaces; i++)
                {
                    try
                    {
                        var plane = level[i];
                        if (plane != null)
                        {
                            //если место не пустое
                            //Записываем тип самолета
                            if (plane.GetType().Name == "Plane")
                            {
                                sw.Write(i + ":Plane:");
                            }
                            if (plane.GetType().Name == "Seaplane")
                            {
                                sw.Write(i + ":Seaplane:");
                            }
                            //Записываемые параметры
                            sw.Write(plane + Environment.NewLine);
                        }
                    }
                    finally { }
                }
            }
        }
        return true;
    }

    /// <summary>
    /// Загрузка иформации по автомобилям в доке из файла
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    public bool LoadData(string filename)
    {
        if (!File.Exists(filename))
        {
            throw new FileNotFoundException();
        }

        using (StreamReader sr = new StreamReader(filename))
        {
            int counter = -1;
            ITransport plane = null;
            string line = sr.ReadLine();
            if (line.Contains("CountLeveles"))
            {
                //считываем количество уровней
                int count = Convert.ToInt32(line.Split(':')[1]);
                if (dockStages != null)
                {
                    dockStages.Clear();
                }
                dockStages = new List<Dock<ITransport,IEngine>>(count);
            }
            else
            {
                //если нет такой записи, то это не те данные
                throw new Exception("Wrong file format");
            }

            while ((line = sr.ReadLine()) != null)
            {
                //идем по считанным записям
                if (line.Contains("Level"))
                {
                    //начинаем новый уровень
                    counter++;
                    dockStages.Add(new Dock<ITransport,IEngine>(countPlaces,
    pictureWidth, pictureHeight));
                    continue;
                }
                string parametr = line.Split(':')[2];
                if (line.Contains("Plane"))
                {
                    Console.WriteLine(parametr);
                    plane = new Plane(parametr);
                }
                else if (line.Contains("Seaplane"))
                {
                    Console.WriteLine(parametr);
                    plane = new Seaplane(parametr);
                }

                dockStages[counter][Convert.ToInt32(line.Split(':')[0])] = plane;

            }
        }
        return true;
    }

    public bool SaveLevel(string filename, int levelnumber)
    {
        if (File.Exists(filename))
        {
            File.Delete(filename);
        }
        var level = dockStages[levelnumber];
        using (StreamWriter sw = new StreamWriter(filename))
        {
            for (int i = 0; i < countPlaces; i++)
            {
                var plane = level[i];
                if (plane != null)
                {
                    //если место не пустое
                    //Записываем тип самолета
                    if (plane.GetType().Name == "Plane")
                    {
                        sw.Write(i + ":Plane:");
                    }
                    if (plane.GetType().Name == "Seaplane")
                    {
                        sw.Write(i + ":Seaplane:");
                    }
                    //Записываемые параметры
                    sw.Write(plane + Environment.NewLine);
                }
            }
        }
        return true;
    }

    public bool LoadLevel(string filename, int levelnumber)
    {
        if (!File.Exists(filename))
        {
            return false;
        }
        dockStages[levelnumber].ClearLevel();
        using (StreamReader sr = new StreamReader(filename))
        {
            ITransport plane = null;
            string line;
           
            while ((line = sr.ReadLine()) != null)
            {
                string parametr = line.Split(':')[2];
                if (line.Contains("Plane"))
                {
                    Console.WriteLine(parametr);
                    plane = new Plane(parametr);
                }
                else if (line.Contains("Seaplane"))
                {
                    Console.WriteLine(parametr);
                    plane = new Seaplane(parametr);
                }

                dockStages[levelnumber][Convert.ToInt32(line.Split(':')[0])] = plane;

            }
        }
        return true;
    }
}



