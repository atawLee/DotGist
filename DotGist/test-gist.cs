using System;
using System.Threading;


Console.CursorVisible = false;
Console.Clear();


const int screenWidth = 120;
const int screenHeight = 30;
const double thetaSpacing = 0.07;
const double phiSpacing = 0.02;
const double R1 = 0.6;  // 도넛의 작은 원 반지름 더 축소
const double R2 = 1.2;  // 도넛의 중심까지의 거리 더 축소
const double K2 = 10;   // 카메라와 도넛 사이의 거리 더 증가
const double K1 = screenWidth * K2 * 3 / (8 * (R1 + R2));


double A = 1, B = 1;  // X축과 Z축 회전각


while (true)
{
    char[] output = new char[screenWidth * screenHeight];
    double[] zbuffer = new double[screenWidth * screenHeight];


    // 배경 문자로 초기화
    for (int i = 0; i < screenWidth * screenHeight; i++)
    {
        output[i] = ' ';
        zbuffer[i] = 0;
    }


    // 도넛 생성
    for (double theta = 0; theta < 2 * Math.PI; theta += thetaSpacing)
    {
        for (double phi = 0; phi < 2 * Math.PI; phi += phiSpacing)
        {
            // 3D 도넛 좌표 계산
            double costheta = Math.Cos(theta);
            double sintheta = Math.Sin(theta);
            double cosphi = Math.Cos(phi);
            double sinphi = Math.Sin(phi);


            // 회전 전의 도넛 좌표
            double circlex = R2 + R1 * costheta;
            double circley = R1 * sintheta;


            // X축 회전 (A 각도)
            double cosA = Math.Cos(A);
            double sinA = Math.Sin(A);
            double x = circlex * (Math.Cos(B) * cosphi + sinA * Math.Sin(B) * sinphi)
                     - circley * cosA * Math.Sin(B);
            double y = circlex * (Math.Sin(B) * cosphi - sinA * Math.Cos(B) * sinphi)
                     + circley * cosA * Math.Cos(B);
            double z = K2 + cosA * circlex * sinphi + circley * sinA;
            double ooz = 1 / z;  // one over z


            // 2D 화면 좌표로 투영
            int xp = (int)(screenWidth / 2 + K1 * ooz * x);
            int yp = (int)(screenHeight / 2 - K1 * ooz * y);


            // 화면 범위 내에 있는지 확인
            if (xp >= 0 && xp < screenWidth && yp >= 0 && yp < screenHeight)
            {
                int idx = xp + screenWidth * yp;


                // z-buffer 테스트 (더 가까운 픽셀만 그리기)
                if (ooz > zbuffer[idx])
                {
                    zbuffer[idx] = ooz;


                    // 조명 계산 (Lambert 조명 모델)
                    double nx = costheta * (Math.Cos(B) * cosphi + sinA * Math.Sin(B) * sinphi)
                              - sintheta * cosA * Math.Sin(B);
                    double ny = costheta * (Math.Sin(B) * cosphi - sinA * Math.Cos(B) * sinphi)
                              + sintheta * cosA * Math.Cos(B);
                    double nz = cosA * costheta * sinphi + sintheta * sinA;


                    // 광원은 카메라 뒤쪽에서 비춤
                    double luminance = nx * 0 + ny * 0.447214 + nz * (-0.894427);


                    // 조명에 따른 문자 선택
                    if (luminance > 0)
                    {
                        int luminance_index = (int)(luminance * 8);
                        output[idx] = ".,-~:;=!*#$@"[Math.Min(luminance_index, 11)];
                    }
                    else
                    {
                        output[idx] = '.';
                    }
                }
            }
        }
    }


    // 화면에 출력
    Console.SetCursorPosition(0, 0);
    for (int j = 0; j < screenHeight; j++)
    {
        for (int i = 0; i < screenWidth; i++)
        {
            Console.Write(output[i + screenWidth * j]);
        }
        Console.WriteLine();
    }


    // 회전각 업데이트
    A += 0.04;
    B += 0.02;


    // 프레임 레이트 조절
    Thread.Sleep(30);
}