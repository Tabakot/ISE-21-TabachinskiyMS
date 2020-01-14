using System;
using System.Collections.Generic;

using TechProgWin;
public class MultiLevelDock
{
    List<Dock<ITransport, IEngine>> dockStages;

    private const int countPlaces = 20;
   
    public MultiLevelDock(int countStages, int pictureWidth, int pictureHeight)
    {
        dockStages = new List<Dock<ITransport, IEngine>>();
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
}
