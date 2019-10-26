using System;
using System.Collections.Generic;

using TechProgWin;
public class MultiLevelDock
{
    List<Dock<ITransport>> dockStages;

    private const int countPlaces = 20;
   
    public MultiLevelDock(int countStages, int pictureWidth, int pictureHeight)
    {
        dockStages = new List<Dock<ITransport>>();
        for (int i = 0; i < countStages; ++i)
        {
            dockStages.Add(new Dock<ITransport>(countPlaces, pictureWidth,
           pictureHeight));
        }
    }

    public Dock<ITransport> this[int ind]
    {
        get
        {
            if (ind > -1 && ind < dockStages.Count)
            {
                return dockStages[ind];
            }
            return null;
        }
    }
}
