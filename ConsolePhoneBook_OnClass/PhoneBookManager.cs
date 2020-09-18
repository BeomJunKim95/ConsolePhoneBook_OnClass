﻿using ClassLibrary0916;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePhoneBook_OnClass
{
	class PhoneBookManager
	{
		const int MAX_CNT = 100;
		PhoneInfo[] infoStorage = new PhoneInfo[MAX_CNT];
		int curCnt = 0;

		//PhoneInfo phoneInfo = new PhoneInfo();

		public PhoneBookManager()
		{

		}
		public void ShowMenu()
		{
			Console.WriteLine("---------------------- 주소록 ------------------------");
			Console.WriteLine("1. 입력  |  2. 목록  | 3. 검색  | 4.삭제  |  5. 종료");
			Console.WriteLine("------------------------------------------------------");
			Console.Write("선택 : ");
		}

		public void InputData()
		{
			string major = "";
			int year = 0;
			int choice = 0;
			string company = "";

			while (true)
			{
				Console.WriteLine("1. 일반  2. 대학  3. 회사");
				Console.Write("선택 >>  ");
				choice = Utility.ConvertInt(Console.ReadLine());
				if (choice > 3)
				{
					Console.WriteLine("1~3까지의 숫자만 입력해주세요.");
					return;
				}
				switch (choice)
				{
					case 1:
						break;

					case 2:
						Console.Write("학과 : ");
						major = Console.ReadLine().Trim().Replace(" ", "");
						if (string.IsNullOrEmpty(major))
						{
							Console.WriteLine("학과입력은 필수입력입니다");
							return;
						}
						Console.Write("학년 : ");
						year = Utility.ConvertInt(Console.ReadLine().Trim().Replace(" ", ""));
						if (year == 0)
						{
							Console.WriteLine("학년은 필수입력입니다");
							return;
						}
						break;

					case 3:
						Console.Write("회사 : ");
						company = Console.ReadLine().Trim().Replace(" ", "");
						if (string.IsNullOrEmpty(company))
						{
							Console.WriteLine("회사입력은 필수입력입니다");
							return;
						}
						break;
				}
			
					break;
			}

			Console.Write("이름 : ");
			string name = Console.ReadLine().Trim().Replace(" ", ""); //Trim() : 공백제거, Replace() : 공백이나 문자 제거
			if (string.IsNullOrEmpty(name)) // if (name == "") or if (name.Length < 1) or if (name.Equals(""))
			{
				Console.WriteLine("이름은 필수입력입니다");
				return;
			}
			else
			{
				int dataIdx = SearhName(name);
				if (dataIdx > -1)
				{
					Console.WriteLine("이미 등록된 이름입니다. 다른 이름으로 입력하세요.");
					return;
				}
			}

			Console.Write("전화번호 : ");
			string phoneNum = Console.ReadLine().Trim().Replace(" ", "");
			if (string.IsNullOrEmpty(phoneNum))
			{
				Console.WriteLine("전화번호는 필수입력입니다");
				return;
			}

			Console.Write("생일 : ");
			string birth = Console.ReadLine().Trim();

			if (choice == 1)
			{
				if (birth.Length < 1)
					infoStorage[curCnt++] = new PhoneInfo(name, phoneNum);
				else if (major.Length < 1 && year == 0)
					infoStorage[curCnt++] = new PhoneInfo(name, phoneNum, birth);
			}
			else if (choice == 2)
				infoStorage[curCnt++] = new PhoneUnivInfo(name, phoneNum, birth, major, year);
			else
				infoStorage[curCnt++] = new PhoneCompanyInfo(name, phoneNum, birth, company);

			Console.WriteLine("성공적으로 등록이 완료됐습니다.\n");

			#region 내코드
				//for (curCnt = 0; curCnt < infoStorage.Length; curCnt++)
				//{
				//	Console.Write("이름을 입력하세요 [필수] : ");
				//	string name = Console.ReadLine();
				//	Console.Write("전화번호를 입력하세요 [필수] : ");
				//	string phoneNumber = Console.ReadLine();
				//	Console.WriteLine("생일을 입력하세요(YYMMDD) [선택] : ");
				//	string birth = Console.ReadLine();

				//	if (name == "" || phoneNumber == "")
				//	{
				//		Console.WriteLine("이름과 번호를 필수로 입력해주세요\n");
				//		continue;
				//	}
				//	else if (birth == "")
				//		infoStorage[curCnt] = new PhoneInfo(name, phoneNumber);
				//	else
				//		infoStorage[curCnt] = new PhoneInfo(name, phoneNumber, birth);
				//}
				#endregion
		}
		public void ListData()
		{
			if (curCnt == 0)
			{
				Console.WriteLine("등록된 번호가 없습니다.\n");
				return; //밑으로 내려가지 않기위해
			}
			else
			{ 
				for (int i = 0; i < curCnt; i++)
				{
					//infoStorage[i].ShowPhoneInfo();
					Console.WriteLine(infoStorage[i].ToString());
				}
			}
		}
		public void SearchData()
		{
			Console.WriteLine("주소록 검색을 시작합니다......");
			int dataIdx = SearhName();
			if (dataIdx < 0)
			{
				Console.WriteLine("검색된 데이터가 없습니다");
			}
			else
			{
				infoStorage[dataIdx].ShowPhoneInfo();
			}
		}

		private int SearhName()
		{
			Console.Write("이름 : ");
			string name = Console.ReadLine().Trim().Replace(" ", "");
			
			#region 모두 찾기
			//int findCnt = 0;
			//for(int i =0;i<curCnt;i++)
			//{

			//	if(infoStorage[i].Name.ToLower() == name.ToLower())
			//	{
			//		infoStorage[i].ShowPhoneInfo();
			//		findCnt++;
			//		//break; //1명만찾고 나가기 
			//			   //break;문이 없으면 동명이인 다찾기
			//	}
			//	//else //이건 1명 찾을때마다 else문으로 넘어와서 리소스 소모가 큼 
			//	//	 //모두 검색을 하고 한번에 검색된 사람이 없는것을 찾을 수 없을까?

			//	//{
			//	//	Console.WriteLine("검색된 데이터가 없습니다");
			//	//}
			//}
			//if(findCnt <1)
			//{
			//	Console.WriteLine("검색된 데이터가 없습니다");
			//}
			//else
			//	Console.WriteLine($"총{findCnt}명이 검색되었습니다.\n");
			#endregion

			for (int i = 0; i < curCnt; i++)
			{
				if (infoStorage[i].Name.ToLower().CompareTo(name) == 0)
				{
					return i; // 검색결과를 찾으면 인덱스로 반환 받기 
				}
			}

			return -1; // 
		}

		private int SearhName(string name) //이름을 입력하는 코드를 지우기 위한 중복정의
		{
			
			#region 모두 찾기
			//int findCnt = 0;
			//for(int i =0;i<curCnt;i++)
			//{

			//	if(infoStorage[i].Name.ToLower() == name.ToLower())
			//	{
			//		infoStorage[i].ShowPhoneInfo();
			//		findCnt++;
			//		//break; //1명만찾고 나가기 
			//			   //break;문이 없으면 동명이인 다찾기
			//	}
			//	//else //이건 1명 찾을때마다 else문으로 넘어와서 리소스 소모가 큼 
			//	//	 //모두 검색을 하고 한번에 검색된 사람이 없는것을 찾을 수 없을까?

			//	//{
			//	//	Console.WriteLine("검색된 데이터가 없습니다");
			//	//}
			//}
			//if(findCnt <1)
			//{
			//	Console.WriteLine("검색된 데이터가 없습니다");
			//}
			//else
			//	Console.WriteLine($"총{findCnt}명이 검색되었습니다.\n");
			#endregion

			for (int i = 0; i < curCnt; i++)
			{
				if (infoStorage[i].Name.ToLower().CompareTo(name) == 0)
				{
					return i; // 검색결과를 찾으면 인덱스로 반환 받기 
				}
			}

			return -1; // 검색결과를 찾지못하면 -1반환 
		}

		public void DeleteData()
		{
			Console.WriteLine("주소록 삭제를 시작합니다......");

			int dataIdx = SearhName();
			if (dataIdx < 0)
			{
				Console.WriteLine("삭제할 데이터가 없습니다");
			}
			else
			{
				for (int i = dataIdx; i < curCnt; i++)
				{
					infoStorage[i] = infoStorage[i + 1];
				}
				curCnt--;
				Console.WriteLine("주소록 삭제가 완료되었습니다.\n");
				
			}
		}
	}
}