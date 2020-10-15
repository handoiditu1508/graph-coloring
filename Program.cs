using System;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Coloring
{
	class Program
	{
		static void Main(string[] args)
		{
			//string filePath = @"..\..\..\input\";//visual studio
			string filePath = @".\input\";//visual studio code
			string fileName = "test1.txt";

			int n;
			int[][] data;
			int[] order;
			int[] level;
			int[] color;
			int maxColor = 0;

			#region readfile and process data
			StreamReader file = new StreamReader(Path.Combine(filePath, fileName));
			n = int.Parse(file.ReadLine());
			data = new int[n][];
			order = Enumerable.Range(0, n).ToArray();
			level = new int[n];
			color = new int[n];
			for (int i = 0; i < n; i++)
			{
				string row = file.ReadLine();
				data[i] = Array.ConvertAll<string, int>(row.Split(' '), s => int.Parse(s));
				int count = 0;
				for (int j = 0; j < n; j++)
				{
					count += data[i][j];
				}
				level[i] = count;
			}
			file.Close();

			order = order.OrderByDescending(num => level[num]).ToArray();
			#endregion

			for (int i = 0; i < n; i++)
			{
				int ii = order[i];
				if (color[ii] == 0)
				{
					maxColor++;
					color[ii] = maxColor;
					for (int j = 0; j < n; j++)
					{
						var jj = order[j];
						if (ii == jj || color[jj] != 0)
							continue;

						bool nearColorII = false;
						for (int z = 0; z < n; z++)
						{
							if (data[jj][z] == 1 && color[z] == color[ii])
							{
								nearColorII = true;
								break;
							}
						}

						if (!nearColorII)
							color[jj] = maxColor;
					}
				}
			}
			Console.WriteLine($"Max Color: {maxColor}");
			Console.WriteLine($"Color: {JsonSerializer.Serialize(color)}");
		}
	}
}
