#include "pch.h"
#include <stdio.h>
#include <winsock2.h>
#pragma comment ( lib, "ws2_32.lib" )

void main()
{
	printf("|=============================================|\n");
	printf("|                                             |\n");
	printf("|                  �ͻ���                     |\n");
	printf("|                                             |\n");
	printf("|=============================================|\n");

	int len = 1024;
	char Server_IP[20] = { '0' };
	char recvBuf[1024] = "\0";
	char sendBuf[1024] = "\0";
	char path[100] = { '0' };

	printf("����������IP:");
	gets_s(Server_IP);
	WORD wVersion = MAKEWORD(2, 0);
	WSADATA wsData;
	if (WSAStartup(wVersion, &wsData) != 0)
	{
		printf("��ʼ��ʧ��!\n");
	}

	SOCKET sockCli = socket(AF_INET, SOCK_STREAM, 0);

	sockaddr_in addClient;  
	addClient.sin_family = AF_INET;  //���socket addr_in�ṹ
	addClient.sin_addr.S_un.S_addr = inet_addr(Server_IP);  //���ʮ��������ʾ��IP��ַת��Ϊ�����ֽ����IP��ַ
	addClient.sin_port = htons(8000);

	if (connect(sockCli, (SOCKADDR*)&addClient, sizeof(SOCKADDR)) != 0)
		printf("���ӷ�����ʧ��!\n");

	else
	{
		printf("���ӳɹ�!\n");
		while (1)
		{
			if (strcmp(recvBuf, "exit") == 0)
			{
				break;
			}
			else//���ձ����ļ�
			{
				printf("���ͣ�");
				gets_s(sendBuf);
				send(sockCli, sendBuf, strlen(sendBuf) + 1, 0);
				recv(sockCli, recvBuf, len, 0);
				printf("���ܣ�%s\n", recvBuf);
				if (strcmp(sendBuf, "exit") == 0)
				{
					break;
				}
			}
		}
		closesocket(sockCli);
	}
}
