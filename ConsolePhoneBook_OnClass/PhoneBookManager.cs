using ClassLibrary0916;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePhoneBook_OnClass
{
	
	class PhoneBookManager
	{
		const int MAX_CNT = 100;
		int curCnt = 0;
		PhoneInfo[] infoStorage = new PhoneInfo[MAX_CNT];
		

		//PhoneInfo phoneInfo = new PhoneInfo();
		static PhoneBookManager instance;
		private PhoneBookManager(){}

		public static PhoneBookManager CreateInstance() // 생성자를 만들어 주는거기 떄문에 반환타입은 생성자의 타입인 클래스명
		{
			//SingleTone1 instance; // 이게 전역으로 가야 메서드가 끝날때 사라지지 않는다
			if (instance == null)
				instance = new PhoneBookManager(); // 클래스 내부이기 때문에 private이여도 new할수있음

			return instance; // 이미 인스턴스가 생성 되어 있으면 if문을 건너 뛰고 이미 생성된 인스턴스를 반환
		}

		public void SaveDate()
		{
			BinaryFormatter serializer = new BinaryFormatter();

			FileStream fs = new FileStream("phonebook.dat", FileMode.Create);
			PhoneInfo[] saveFile = new PhoneInfo[curCnt];
			Array.Copy(infoStorage, saveFile, curCnt);
			serializer.Serialize(fs, saveFile);
			fs.Close();
			Console.WriteLine("전화번호부 저장완료");
		}

		public void ReadData()
		{
			BinaryFormatter serializer = new BinaryFormatter();

			if (File.Exists("phonebook.dat"))
			{
				FileStream rs = new FileStream("phonebook.dat", FileMode.Open);
				PhoneInfo[] loadFile = (PhoneInfo[])serializer.Deserialize(rs);
				curCnt = loadFile.Length;
				Array.Copy(loadFile, infoStorage, curCnt);
				//infoStorage = loadFile;

				Console.WriteLine("전화번호부를 성공적으로 불러왔습니다.");
				//foreach (Person1 item in arr)
				//{
				//	Console.WriteLine(item.Name);
				//}
				rs.Close();
			}
		}
		public void ShowMenu()
		{
			Console.WriteLine("---------------------------- 주소록 ----------------------------");
			Console.WriteLine("1. 입력  |  2. 목록  | 3. 검색  | 4.삭제  |  5. 정렬  |  6. 종료");
			Console.WriteLine("----------------------------------------------------------------");
			Console.Write("선택 : ");
		}

		public void InputData()
		{
			int choice;


			Console.WriteLine("1. 일반  2. 대학  3. 회사");
			Console.Write("선택 >>  ");
			choice = Utility.ConvertInt(Console.ReadLine());
			if (choice < 1 || choice > 3)
			{
				throw new Exception("1~3까지의 숫자만 입력해주세요");
				//Console.WriteLine("1~3까지의 숫자만 입력해주세요.");

			}
			PhoneInfo info = null;
			switch (choice)
			{
				case 1:
					info = InputFriendInfo();
					break;
				case 2:
					info = InputUnivInfo();
					break;
				case 3:
					info = InputCompanyInfo();
					break;
			}
			if (info != null)
			{
				infoStorage[curCnt++] = info;
				Console.WriteLine("성공적으로 등록이 완료됐습니다.\n");
			}

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
		private string[] InputCommonInfo()
		{
			try
			{
				Console.Write("이름 : ");
				string name = Console.ReadLine().Trim().Replace(" ", ""); //Trim() : 공백제거, Replace() : 공백이나 문자 제거
				if (string.IsNullOrEmpty(name)) // if (name == "") or if (name.Length < 1) or if (name.Equals(""))
				{
					throw new Exception("이름은 필수입력입니다");
					//Console.WriteLine("이름은 필수입력입니다");
					//return null;
				}
				else
				{
					int dataIdx = SearhName(name);
					if (dataIdx > -1)
					{
						throw new Exception("이미 등록된 이름입니다. 다른 이름으로 입력하세요");
						//Console.WriteLine("이미 등록된 이름입니다. 다른 이름으로 입력하세요.");
						//return null;
					}
				}

				Console.Write("전화번호 : ");
				string phoneNum = Console.ReadLine().Trim().Replace(" ", "");
				if (string.IsNullOrEmpty(phoneNum))
				{
					Console.WriteLine("전화번호는 필수입력입니다");
					return null;
				}

				Console.Write("생일 : ");
				string birth = Console.ReadLine().Trim();

				string[] arr = new string[3];
				arr[0] = name;
				arr[1] = phoneNum;
				arr[2] = birth;

				return arr;
			}
			catch(Exception err)
			{
				throw err;
			}
		}

		private PhoneInfo InputFriendInfo()
		{
			string[] cominfo = InputCommonInfo();
			if(cominfo == null || cominfo.Length != 3)
				return null;
			if (cominfo[2].Length < 1)
			{
				return new PhoneInfo(cominfo[0], cominfo[1]);
			}
			else
				return new PhoneInfo(cominfo[0], cominfo[1], cominfo[2]);
			
		}
		
		private PhoneInfo InputUnivInfo()
		{
			string[] cominfo = InputCommonInfo();
			if (cominfo == null || cominfo.Length != 3)
				return null;
			Console.Write("학과 : ");
			string major = Console.ReadLine().Trim().Replace(" ", "");
			if (string.IsNullOrEmpty(major))
			{
				Console.WriteLine("학과입력은 필수입력입니다");
				return null;
			}
			Console.Write("학년 : ");
			int year = Utility.ConvertInt(Console.ReadLine().Trim().Replace(" ", ""));
			if (year == 0)
			{
				Console.WriteLine("학년은 필수입력입니다");
				return null;
			}
			//if (cominfo[2].Length < 1)
			//{
				return new PhoneUnivInfo(cominfo[0], cominfo[1], cominfo[2], major, year);
			//}
			//else
			//	return new PhoneInfo(cominfo[0], cominfo[1], cominfo[2]);
		}

		private PhoneInfo InputCompanyInfo()
		{
			string[] cominfo = InputCommonInfo();
			if (cominfo == null || cominfo.Length != 3)
				return null;
			Console.Write("회사 : ");
			string company = Console.ReadLine().Trim().Replace(" ", "");
			if (string.IsNullOrEmpty(company))
			{
				Console.WriteLine("회사입력은 필수입력입니다");
				return null;
			}
			return new PhoneCompanyInfo(cominfo[0], cominfo[1], cominfo[2], company);
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
		public void SortData()
		{
			Console.WriteLine("1. 이름 오름차순  |  2. 이름 내림차순  |  3. 전화번호 오름차순  |  4. 전화번호 내림차순");
			Console.Write("선택 >>  ");
			int choice;

			choice = Utility.ConvertInt(Console.ReadLine());
			if (choice < 1 || choice > 3)
			{
				Console.WriteLine("1~3까지의 숫자만 입력해주세요.");
				return;
			}
			PhoneInfo[] infoStorage_Copy = new PhoneInfo[curCnt];
			Array.Copy(infoStorage, infoStorage_Copy, curCnt);
			switch(choice)
			{
				case 1:
					Array.Sort(infoStorage_Copy, new PhoneInfoNameComparer());
					break;
				case 2:
					Array.Sort(infoStorage_Copy, new PhoneInfoNameComparer());
					Array.Reverse(infoStorage_Copy);
					break;
				case 3:
					Array.Sort(infoStorage_Copy);
					break;
				case 4:
					Array.Sort(infoStorage_Copy);
					Array.Reverse(infoStorage_Copy);
					break;
			}

			for(int i = 0; i<curCnt; i++)
			{
				Console.WriteLine(infoStorage_Copy[i].ToString());
			}
		}

	
	}
}
