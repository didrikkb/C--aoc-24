class Day9(string data) : IDay
{
    public string A()
    {
        long res = 0;
        int[] input = Parse(data);
        int[] disk = new int[input.Sum()];

        disk = CreateDisk(disk, input);

        int l = 0;
        int r = disk.Length - 1;

        while (true)
        {
            while (disk[l] != -1)
            {
                l++;
            }
            while (disk[r] == -1)
            {
                r--;
            }
            if (l >= r)
            {
                break;
            }
            (disk[l], disk[r]) = (disk[r], disk[l]);
        }

        for (int i = 0; i < disk.Length && disk[i] != -1; i++)
        {
            res += i * disk[i];
        }

        return res.ToString();
    }

    public string B()
    {
        long res = 0;
        int[] input = Parse(data);
        int[] disk = new int[input.Sum()];

        disk = CreateDisk(disk, input);

        int r = disk.Length - 1;

        while (true)
        {
            while (r >= 0 && (disk[r] == -1))
            {
                r--;
            }

            (int fileSize, int id) = FileSize(disk, r);

            int l = 0;
            while (l < disk.Length && (disk[l] != -1 || FreeSize(disk, l) < fileSize))
            {
                l++;
            }

            if (r >= l && FreeSize(disk, l) >= fileSize)
            {
                for (int i = 0; i < fileSize; i++)
                {
                    (disk[l + i], disk[r - i]) = (disk[r - i], disk[l + i]);
                }
            }

            r -= fileSize;

            if (id == 0)
            {
                break;
            }
        }

        for (int i = 0; i < disk.Length; i++)
        {
            if (disk[i] != -1)
            {
                res += i * disk[i];
            }
        }

        return res.ToString();
    }

    private static (int, int) FileSize(int[] disk, int idx)
    {
        int id = disk[idx];
        int sidx = idx;

        while (idx >= 0 && disk[idx] == id)
        {
            idx--;
        }

        return (sidx - idx, id);
    }

    private static int FreeSize(int[] disk, int idx)
    {
        int sidx = idx;

        while (idx < disk.Length && disk[idx] == -1)
        {
            idx++;
        }

        return idx - sidx;
    }

    private static int[] CreateDisk(int[] disk, int[] input)
    {
        int pos = 0;
        bool free = false;
        int id = 0;

        foreach (int sector in input)
        {
            for (int i = 0; i < sector; i++)
            {
                if (free)
                {
                    disk[pos] = -1;
                }
                else
                {
                    disk[pos] = id;
                }

                pos++;
            }

            if (!free)
            {
                id++;
            }

            free = !free;
        }

        return disk;
    }

    private static int[] Parse(string data)
    {
        return data
            .ToCharArray()
            .Where(x => x >= '0' && x <= '9')
            .Select(x => x - '0')
            .ToArray();
    }
}