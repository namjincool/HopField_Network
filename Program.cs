using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;

public static class Globals
{
    public static void printArr(List<List<int>> v , int isSuccess)
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Console.Write((isSuccess == 1) ? (v[i][j] == 1 ? '#' : ' ') : 'F');
                //cout << v[i][j] << " ";
            }
            Console.Write("\n");
        }
    }

    internal static int Main()
    {
        // 프로젝트는 O,X를 판별할수 있는 코드로 작성을 하였습니다
        // 10 x 10
        List<List<int>> RIGHT = new List<List<int>>()
        {
            new List<int> {0, 0, 0, 0, 0, 0, 0,0,0, 0},
            new List<int> {0, 0, 0, 1, 1, 1, 1,0,0, 0},
            new List<int> {0, 0, 1, 0, 0, 0, 0,1,0, 0},
            new List<int> {0, 1, 0, 0, 0, 0, 0,0,1, 0},
            new List<int> {0, 1, 0, 0, 0, 0, 0,0,1, 0},
            new List<int> {0, 1, 0, 0, 0, 0, 0,0,1, 0},
            new List<int> {0, 1, 0, 0, 0, 0, 0,0,1, 0},
            new List<int> {0, 1, 0, 0, 0, 0, 0,0,1, 0},
            new List<int> {0, 0, 1, 0, 0, 0, 0,1,0, 0},
            new List<int> {0, 0, 0, 1, 1, 1, 1,0,0, 0}
        };

        List<List<int>> CANCEL = new List<List<int>>()
        {
            new List<int> {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            new List<int> {0, 1, 0, 0, 0, 0, 0, 0, 1, 0},
            new List<int> {0, 0, 1, 0, 0, 0, 0, 1, 0, 0},
            new List<int> {0, 0, 0, 1, 0, 0, 1, 0, 0, 0},
            new List<int> {0, 0, 0, 0, 1, 1, 0, 0, 0, 0},
            new List<int> {0, 0, 0, 0, 1, 1, 0, 0, 0, 0},
            new List<int> {0, 0, 0, 1, 0, 0, 1, 0, 0, 0},
            new List<int> {0, 0, 1, 0, 0, 0, 0, 1, 0, 0},
            new List<int> {0, 1, 0, 0, 0, 0, 0, 0, 1, 0},
            new List<int> {1, 0, 0, 0, 0, 0, 0, 0, 0, 1}
        };

        List<List<List<int>>> numV = new List<List<List<int>>>(2);
        numV.Add(new List<List<int>>(RIGHT));
        numV.Add(new List<List<int>>(CANCEL));
        //두 패턴을 저장하기위해 저장

        //가중치 행렬을 초기화 0으로 초기화
        List<List<int>> weightV = new List<List<int>>(); 
        int idx1, idx2;
        for (idx1 = 0; idx1 < 100; idx1++)
        {
            weightV.Add(new List<int>());
            for (idx2 = 0; idx2 < 100; idx2++)
            {
                weightV[idx1].Add(0);
            }
        }
        List<int> colV = new List<int>(new int[100]); // 패턴을 저장하기 위해 
        // 가중치 행렬 계산
        for (int v = 0; v < numV.Count; v++)
        {
                // 2차원 행렬을 1차원 행렬로 변환학습패턴에 양극화 연산을 적용해야함
                // 양극화 연산--> 1,-1을 1차원 배열 colV에 저장,
                // 중요한 특징을 중심으로 일치하는지 확인하기 위해 수행
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        colV[(10 * i) + j] = (numV[v][i][j] * 2) - 1; //colV 1차원 배열에 1 , -1 로 지정
                    }
                }
               
                for (int i = 0; i < 100; i++)
                {
                    for (int j = 0; j < 100; j++)
                    {
                        if (i != j) 
                        //단위행렬 연산과정부분을 대신함 
                        // (1,1) (2,2) ,(3,3) ... 부분 연산을 생략함으로
                        //단위 행렬 연산도 같이 되게 코딩을 하였습니다 
                        {
                            weightV[i][j] += (colV[i] * colV[j]); 
                            // W1 + W2 --> 두패턴을 저장하는 연결강도 
                            
                        }
                    }
                }  
        }

        List<List<int>> input_data = new List<List<int>>()
    {
            new List<int> {1, 0, 0, 0, 0, 0, 0,0,0, 0},
            new List<int> {0, 1, 0, 0, 0, 0, 0,0,0, 0},
            new List<int> {0, 0, 1, 0, 0, 0, 0,0,0, 0},
            new List<int> {0, 0, 0, 1, 0, 0, 0,0,0, 0},
            new List<int> {0, 0, 0, 0, 1, 0, 0,0,0, 0},
            new List<int> {0, 0, 0, 0, 1, 1, 0,0,0, 0},
            new List<int> {0, 0, 0, 0, 0, 0, 1,0,0, 0},
            new List<int> {0, 0, 0, 0, 0, 0, 0,1,0, 0},
            new List<int> {0, 0, 0, 0, 0, 0, 0,0,1, 0},
            new List<int> {0, 0, 0, 0, 0, 0, 0,0,0, 1}
    };
        
        Console.Write("************INPUT*****************");
        printArr(input_data,1);
        Console.Write("\n");
        // 입력 패턴에 대한 학습 패턴 연상
        for (int p = 0; p < numV.Count * numV[0].Count * numV[1].Count; p++) 
        {
            for (int a = 0; a < colV.Count; a++) // 100
            {
                int sum = 0;
                
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        sum += input_data[i][j] * weightV[a][(i * 10) + j]; 
                        //저장된 두패턴의 가중치값과 입력값을 곱한 후 더함 
                    }
                }
                // sum>0 --> 1 
                // sum =0 --> sum
                // sum<0 --> -1 
                if (sum > 0)
                {
                    input_data[a / 10][a % 10] = 1;
                }
                else if (sum < 0)
                {
                    input_data[a / 10][a % 10] = 0; 
                }
            }
            //활성화 함수 이후 복원된 값이 맞는지 확인 
            //입력 패턴과 저장된 패턴을비교하는과정 for each 문
            //이 부분이 다릅니다! 원래는 입력값이 복원이 될때까지 반복
            // but 저희는 숫자를 입력을 하기때문에 복원 값이 안나올수도 있으므로 
            // for문으로 끝나게 코딩을 수정을 하였습니다
            foreach (var v in numV)
            {
                int a = 0;
                int b = 0;
                for (a = 0; a < v.Count(); a++) 
                {
                    for (b = 0; b < v.Count(); b++)
                    {
                        if (v[a][b] != input_data[a][b])
                        {
                            break;
                        }
                    }
                }
                if (a == v.Count() && b == v.Count())
                {
                    Console.Write("*************OUTPUT****************");
                    printArr(input_data,1);
                    return 0;
                }
            }
        }
        Console.Write("실패");
        Console.Write("\n");
        printArr(input_data,0);
        Console.Write("\n");
        return 0;
    }
}