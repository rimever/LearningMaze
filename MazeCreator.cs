using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningMaze
{
    class MazeCreator
    {
        /// <summary>
        /// 迷路の作成
        /// 作成されるのは壁の情報だけで通路の分は入らない。
        /// そのため実際に作りたいサイズの半分の値を引数に
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public int[,] CreateRandomMaze(int x, int y)
        {
            // 三つ大きめに取っておく必要がある
            var maze = new int[x + 3, y + 3];
            for (int i = 0; i <= x + 1; i++)
            {
                for (int j = 0; j <= y + 1; j++)
                {
                    if (i == 0
                        || i == x + 1
                        || j == 0
                        || j == y + 1
                    )
                    {
                        maze[i, j] = 15;
                    }
                    else
                    {
                        maze[i, j] = 3;
                    }
                }
            }

            RecursionMaze(maze,x, y);
            return maze;
        }

        /// <summary>
        /// 迷路作成、再帰処理
        /// </summary>
        private void RecursionMaze(int[,] maze,int i, int j)
        {
            // 第二ビット（訪問の有無）をＯＮ（１）にする
            maze[i, j] = maze[i, j] | 4;
            while (maze[i + 1, j] == 3
                   // 上が未訪問
                   || maze[i, j - 1] == 3
                   // 左が未訪問
                   || maze[i - 1, j] == 3
                   // 下が未訪問
                   || maze[i, j + 1] == 3
            )
            {
                switch (new Random().Next(4))
                {
                    case 0:
                        // 右へ
                        if (maze[i + 1, j] == 3)
                        {
                            maze[i, j] = maze[i, j] & 0xd;
                            RecursionMaze(maze, i + 1, j);
                        }

                        break;
                    case 1:
                        // 上へ
                        if (maze[i, j - 1] == 3)
                        {
                            maze[i, j] = maze[i, j] & 0xe;
                            RecursionMaze(maze, i, j - 1);
                        }

                        break;
                    case 2:
                        // 左へ
                        if (maze[i - 1, j] == 3)
                        {
                            // 左隣の右の壁を外す
                            maze[i - 1, j] = maze[i - 1, j] & 0xd;
                            RecursionMaze(maze, i - 1, j);
                        }

                        break;
                    default:
                        // 下へ
                        if (maze[i, j + 1] == 3)
                        {
                            // 下隣の上の壁を外す
                            maze[i, j + 1] = maze[i, j + 1] & 0xe;
                            RecursionMaze(maze, i, j + 1);
                        }

                        break;
                }
            }
        }

    }
}
