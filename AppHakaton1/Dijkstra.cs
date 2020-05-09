public class Dijkstra
{
    private int minDistance(int[] dist,
                        bool[] sptSet, int leng)
    { 
        int min = int.MaxValue, min_index = -1;
  
        for (int i = 0; i < leng; i++)
            if (sptSet[i] == false && dist[i] <= min)
            { 
                min = dist[i];
                min_index = i;
            }

        return min_index;
    } 

    public int[] serch_ways(int start_pos, int[,] map)
    {
        int[] dist = new int[map.GetLength(0)];
        bool[] sptSet = new bool[map.GetLength(0)];
        for (int i = 0; i < map.GetLength(0); i++) 
        { 
            dist[i] = int.MaxValue; 
            sptSet[i] = false; 
        }
        dist[start_pos] = 0;
        for (int count = 0; count < map.GetLength(0) - 1; count++)
        {
            int u = minDistance(dist, sptSet, map.GetLength(0));
            sptSet[u] = true;
            for (int i = 0; i < map.GetLength(0); i++)
                if (!sptSet[i] && map[u, i] != 0 &&
                     dist[u] != int.MaxValue && dist[u] + map[u, i] < dist[i])
                    dist[i] = dist[u] + map[u, i];
        } 
        return dist;
    }
}