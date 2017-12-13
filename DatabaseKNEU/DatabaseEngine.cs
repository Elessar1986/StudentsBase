using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseKNEU;
using System.Collections.ObjectModel;
using System.Threading;
using System.Text.RegularExpressions;

namespace DatabaseEngineNS
{
    [Serializable]
    public class DatabaseEngine
    {

        public List<FacultyClass> Facultys { get; set; }
        //List<string> MenuString { get; set; }
        string[] kursi = { "1 курс", "2 курс", "3 курс", "4 курс", "5 курс", "6 курс" };
        string[] Main = { "Создать", "Удалить", "Редактировать", "Найти","Показать студентов...","Показать преподавателей...","Выйти" ,"test"};
        string[] Add = { "Факультет", "Кафедру", "Группу", "Студента","Преподавателя","Отмена" };
        string[] ShowStud = { "...по курсам", "...по факультету", "... по кафедре (все курсы)", "...по кафедре (определенный курс)"};
        string[] ShowTeach = { "...по факультету", "... по кафедре", "Отмена" };
        string[] Find = { "Студента","Преподавателя","Отмена" };

        delegate void Functions();
        Dictionary<int, Functions> AddChoise;
        Dictionary<int, Functions> DelChoise;
        Dictionary<int, Functions> EditChoise;
        Dictionary<int, Functions> FindChoise;
        Dictionary<int, Functions> ShowStudents;
        Dictionary<int, Functions> ShowTeacher;

        myMenuClass menu;

        public void RunBase()
        {
            while (true)
            {
                try {
                    myMenuClass menu = new myMenuClass();

                    switch (menu.DrowMenuTitle(Main.ToList(), "Основное меню"))
                    {
                        case 0:
                            AddChoise[menu.DrowMenu(Add.ToList())]();
                            break;
                        case 1:
                            DelChoise[menu.DrowMenu(Add.ToList())]();
                            break;
                        case 2:
                            EditChoise[menu.DrowMenu(Add.ToList())]();
                            break;
                        case 3:
                            FindChoise[menu.DrowMenu(Find.ToList())]();
                            break;
                        case 4:
                            ShowStudents[menu.DrowMenu(ShowStud.ToList())]();
                            break;
                        case 5:
                            ShowTeacher[menu.DrowMenu(ShowTeach.ToList())]();
                            break;
                        case 6:
                            SerializeAll.SaveAs(Facultys);
                            Environment.Exit(0);
                            break;
                        case 7:
                            ShowAllFac();
                            Console.ReadKey();
                            break;
                        default:
                            break;
                    }
                }
                catch(MyException e)
                {
                    Console.WriteLine("ERROR: " + e.Message);
                    Console.ReadKey();
                }
            }
           
        }

        public DatabaseEngine()
        {
            
            AddChoise = new Dictionary<int, Functions>();
            AddChoise.Add(0, AddFaculty);
            AddChoise.Add(1, AddCathedra);
            AddChoise.Add(2, AddGroup);
            AddChoise.Add(3, AddStudent);
            AddChoise.Add(4, AddTeacher);
            AddChoise.Add(5, CancelMenu);

            DelChoise = new Dictionary<int, Functions>();
            DelChoise.Add(0, DelFaculty);
            DelChoise.Add(1, DelCathedra);
            DelChoise.Add(2, DelGroup);
            DelChoise.Add(3, DelStudent);
            DelChoise.Add(4, DelTeacher);
            DelChoise.Add(5, CancelMenu);

            EditChoise = new Dictionary<int, Functions>();
            EditChoise.Add(0, EditFaculty);
            EditChoise.Add(1, EditCathedra);
            EditChoise.Add(2, EditGroup);
            EditChoise.Add(3, EditStudent);
            EditChoise.Add(4, EditTeacher);
            EditChoise.Add(5, CancelMenu);

            FindChoise = new Dictionary<int, Functions>();
            FindChoise.Add(0, FindStud);
            FindChoise.Add(1, FindTeach);
            FindChoise.Add(2, CancelMenu);

            ShowStudents = new Dictionary<int, Functions>();
            ShowStudents.Add(0, ShowStudsOrderByKurs);
            ShowStudents.Add(1, ShowStudsOrderByFac);
            ShowStudents.Add(2, ShowStudsOrderByCath);
            ShowStudents.Add(3, ShowStudsOrderByCathAndKurs);

            ShowTeacher = new Dictionary<int, Functions>();
            ShowTeacher.Add(0, ShowTeachOrderByFac);
            ShowTeacher.Add(1, ShowTeachOrderByCath);
            ShowTeacher.Add(2, CancelMenu);


            try
            {
                Facultys = SerializeAll.DeserializeAll<List<FacultyClass>>();
            }
            catch(Exception e)
            {
                Console.WriteLine("Error serialize");
                Thread.Sleep(1000);
                Facultys = new List<FacultyClass>();
            }

            menu = new myMenuClass();
            //StatusBar sBar = new StatusBar();
        }

        private void ShowTeachOrderByCath()
        {
            int fac = ChoiseFaculty();
            int cath = ChoiseCathedra(fac);
            var teachers = new List<TeacherClass>();
            teachers.AddRange(Facultys[fac].Cathedras[cath].Teachers.OrderBy(x => x.Lastname));
            teachers.ForEach(x => x.Print());
            Console.ReadKey();
            
        }

        private void ShowTeachOrderByFac()
        {
            int fac = ChoiseFaculty();
            var teachers = new List<TeacherClass>();
            foreach (var i in Facultys[fac].Cathedras)
            {
                teachers.AddRange(i.Teachers);
            }
            List<TeacherClass> t = new List<TeacherClass>();
            t.AddRange(teachers.OrderBy(x => x.Lastname));
            t.ForEach(x => x.Print());
            Console.ReadKey();
        }

        private void FindTeach()
        {
            Console.Write("Введите фамилию или ее часть:");
            string find = CheckString(Console.ReadLine());
            Console.Clear();
            var result = new List<TeacherClass>();
            foreach (var i in Facultys)
            {
                foreach (var j in i.Cathedras)
                {
                    result.AddRange(j.Teachers.Where(x => Regex.IsMatch(x.Lastname,find,RegexOptions.IgnoreCase)));
                }
            }
            result.ForEach(x => x.Print());
            Console.ReadKey();
        }

        private void FindStud()
        {
            Console.Write("Введите фамилию или ее часть:");
            string find = CheckString(Console.ReadLine());
            Console.Clear();
            var result = new List<StudentClass>();
            foreach (var i in Facultys)
            {
                foreach (var j in i.Kurs)
                {
                    foreach (var k in j.Groups)
                    {
                        result.AddRange(k.Students.Where(x => Regex.IsMatch(x.Lastname, find, RegexOptions.IgnoreCase)));
                    }
                }
            }
            result.ForEach(x => x.PrintAll());
            Console.ReadKey();
        }

        private void EditTeacher()
        {
            int fac = ChoiseFaculty();
            int cath = ChoiseCathedra(fac);
            if (Facultys[fac].Cathedras[cath].Teachers.Count == 0) throw new MyException("Данная кафедра не содержит преподавателей!");
            var teach = Facultys[fac].Cathedras[cath].Teachers.Select(x => x.Name + " " + x.Lastname).ToList();
            myMenuClass menu = new myMenuClass(1, 1);
            int TeachNum = menu.DrowMenuTitle(teach, "Выберите преподователя:");
            var TEACH = Facultys[fac].Cathedras[cath].Teachers[TeachNum];
            if (menu.YesNoChoise("Желаете изменить личные данные?"))
            {
                TEACH.Print();
                Console.WriteLine("Введите новые данные : ");
                Console.Write("Фамилия: ");
                string LastnameTemp = CheckString(Console.ReadLine());
                Console.Write("Имя: ");
                string NameTemp = CheckString(Console.ReadLine());
                Console.Write("Отчество: ");
                string MidenameTemp = CheckString(Console.ReadLine());
                Console.Write("Возраст: ");
                int ageTemp = Convert.ToInt32(CheckStringOfNum(Console.ReadLine()));
                if (menu.YesNoChoise())
                {
                    Facultys[fac].Cathedras[cath].Teachers[TeachNum].Lastname = LastnameTemp;
                    Facultys[fac].Cathedras[cath].Teachers[TeachNum].Name = NameTemp;
                    Facultys[fac].Cathedras[cath].Teachers[TeachNum].Middlename = MidenameTemp;
                    Facultys[fac].Cathedras[cath].Teachers[TeachNum].Age = ageTemp;
                }
            }
            if(menu.YesNoChoise("Желаете изменить кафедру преподавателя?"))
            {
                int changeCath = ChoiseCathedra(fac);
                if(changeCath != cath)
                {
                    Facultys[fac].Cathedras[changeCath].Teachers.Add(TEACH);
                    Facultys[fac].Cathedras[cath].Teachers.RemoveAt(TeachNum);
                }
            }

        }

        private void EditStudent()
        {
            int fac = ChoiseFaculty();
            int kurs = ChoiseKurs();
            int group = ChoiseGroup(fac, kurs);
            if (Facultys[fac].Kurs[kurs].Groups[group].Students.Count == 0) throw new MyException("В этой группе нет студентов!");
            var studs = Facultys[fac].Kurs[kurs].Groups[group].Students.Select(x => x.Name + " " + x.Lastname + " " + x.Cathedra).ToList();
            myMenuClass menu = new myMenuClass(1, 1);
            int StudNum = menu.DrowMenuTitle(studs, "Выберите студента:");
            var STUD = Facultys[fac].Kurs[kurs].Groups[group].Students[StudNum];
            STUD.PrintCath();
            Console.WriteLine("Введите новые данные : ");
            Console.Write("Фамилия: ");
            string LastnameTemp = CheckString(Console.ReadLine());
            Console.Write("Имя: ");
            string NameTemp = CheckString(Console.ReadLine());
            Console.Write("Отчество: ");
            string MidenameTemp = CheckString(Console.ReadLine());
            Console.Write("Возраст: ");
            int ageTemp = Convert.ToInt32(CheckStringOfNum(Console.ReadLine()));
            if (menu.YesNoChoise())
            {
                Facultys[fac].Kurs[kurs].Groups[group].Students[StudNum].Lastname = LastnameTemp;
                Facultys[fac].Kurs[kurs].Groups[group].Students[StudNum].Name = NameTemp;
                Facultys[fac].Kurs[kurs].Groups[group].Students[StudNum].Middlename = MidenameTemp;
                Facultys[fac].Kurs[kurs].Groups[group].Students[StudNum].Age = ageTemp;
            }

        }

        private void EditGroup()
        {
            int fac = ChoiseFaculty();
            int kurs = ChoiseKurs();
            int group = ChoiseGroup(fac, kurs);
            Console.WriteLine("Текущее имя : " + Facultys[fac].Kurs[kurs].Groups[group].GroupName);
            Console.Write("Введите новое имя : ");
            string temp = CheckString(Console.ReadLine());
            if (menu.YesNoChoise()) Facultys[fac].Kurs[kurs].Groups[group].GroupName = temp;
        }

        private void EditCathedra()
        {
            int fac = ChoiseFaculty();
            int cath = ChoiseCathedra(fac);
            Console.WriteLine("Текущее имя : " + Facultys[fac].Cathedras[cath].CathedraName);
            Console.Write("Введите новое имя : ");
            string temp = CheckString(Console.ReadLine());
            if (menu.YesNoChoise()) Facultys[fac].Cathedras[cath].CathedraName = temp;
        }

        private void EditFaculty()
        {
            int fac = ChoiseFaculty();
            Console.WriteLine("Текущее имя : " + Facultys[fac].FacultyName);
            Console.Write("Введите новое имя : ");
            string temp = CheckString(Console.ReadLine());
            if(menu.YesNoChoise()) Facultys[fac].FacultyName = temp;
        }

        public void CancelMenu() { }

        private string CheckString(string checkStr)
        {
            if (!Regex.IsMatch(checkStr, @"[\w\s]") || (checkStr.Count() == 0)) throw new MyException("Строка содержит недопустимые символы!");
            else
                return checkStr;
        }

        private string CheckStringOfNum(string checkStr)
        {
            if (Regex.IsMatch(checkStr, @"[\D]")) throw new MyException("Строка содержит недопустимые символы!");
            else
                return checkStr;
        }


        public void AddStudent()
        {
            StudentClass s = new StudentClass();
            int fac = ChoiseFaculty();
            int cath = ChoiseCathedra(fac);
            s.Kurs = ChoiseKurs();
            int group = ChoiseGroup(fac, s.Kurs);
            s.Faculty = Facultys[fac].FacultyName;
            s.Cathedra = Facultys[fac].Cathedras[cath].CathedraName;
            s.Group = Facultys[fac].Kurs[s.Kurs].Groups[group].GroupName;    
            Console.WriteLine("Введите имя:");
            s.Name = CheckString(Console.ReadLine());
            Console.Clear();
            Console.WriteLine("Введите отчество:");
            s.Middlename = CheckString(Console.ReadLine());
            Console.Clear();
            Console.WriteLine("Введите фамилию:");
            s.Lastname = CheckString(Console.ReadLine());
            Console.Clear();
            Console.WriteLine("Введите возраст:");
            s.Age = Convert.ToInt32(CheckStringOfNum(Console.ReadLine()));
            Facultys[fac].Kurs[s.Kurs].Groups[group].Students.Add(s);
        }

        public void AddFaculty()
        {
            Console.WriteLine("Введите название факультета:");
            string temp = CheckString(Console.ReadLine());
            Facultys.Add(new FacultyClass(temp)); 
        }

        public int ChoiseFaculty()
        {
            if (Facultys.Count == 0) throw new MyException("Нет ни одного факультета!");
            myMenuClass menu = new myMenuClass(1,1);
            var menuStr = Facultys.Select(x => x.FacultyName).ToList();      
            return menu.DrowMenuTitle(menuStr,"Выберите факультет:");
        }

        public void AddCathedra()
        {
            
            int NumOfFaculty = ChoiseFaculty();
            Console.WriteLine("Введите название кафедры:");
            string temp = CheckString(Console.ReadLine());
            Facultys[NumOfFaculty].Cathedras.Add(new CathedraClass(temp, Facultys[NumOfFaculty].FacultyName));
        }

        public void AddGroup()
        {
            if (Facultys.Count == 0) throw new MyException("Нет ни одного факультета!");
            int NumOfFaculty = ChoiseFaculty();
            myMenuClass menu = new myMenuClass(1, 1);
            int KursNum = menu.DrowMenuTitle(kursi.ToList(), "Выберите курс:");
            Console.WriteLine("Введите название группы:");
            string temp = CheckString(Console.ReadLine());
            Facultys[NumOfFaculty].Kurs[KursNum].Groups.Add(new GroupClass(temp, Facultys[NumOfFaculty].FacultyName,KursNum));
        }

        private int ChoiseCathedra(int FacultyNum)
        {
            if (Facultys[FacultyNum].Cathedras.Count == 0) throw new MyException("Нет ни одной кафедры");
            myMenuClass menu = new myMenuClass(1, 1);
            var menuStr = Facultys[FacultyNum].Cathedras.Select(x => x.CathedraName).ToList();
            return menu.DrowMenuTitle(menuStr, "Выберите кафедру:");
        }

        private int ChoiseGroup(int FacultyNum,int kursNum)
        {
            if (Facultys[FacultyNum].Kurs[kursNum].Groups.Count == 0) throw new MyException("Нет ни одной группы");
            myMenuClass menu = new myMenuClass(1, 1);
            var menuStr = Facultys[FacultyNum].Kurs[kursNum].Groups.Select(x => x.GroupName).ToList();
            return menu.DrowMenuTitle(menuStr, "Выберите группу:");
        }

        private int ChoiseKurs()
        {          
            myMenuClass menu = new myMenuClass(1, 1);
            return menu.DrowMenuTitle(kursi.ToList(), "Выберите курс:");
        }

        public void AddTeacher()
        {
            TeacherClass t = new TeacherClass();
            int fac = ChoiseFaculty();
            int cath = ChoiseCathedra(fac);
            t.Faculty = Facultys[fac].FacultyName;
            t.Cathedra = Facultys[fac].Cathedras[cath].CathedraName;            
            Console.WriteLine("Введите имя:");
            t.Name = CheckString(Console.ReadLine());
            Console.Clear();
            Console.WriteLine("Введите отчество:");
            t.Middlename = CheckString(Console.ReadLine());
            Console.Clear();
            Console.WriteLine("Введите фамилию:");
            t.Lastname = CheckString(Console.ReadLine());
            Console.Clear();
            Console.WriteLine("Введите возраст:");
            t.Age = Convert.ToInt32(CheckStringOfNum(Console.ReadLine()));
            Facultys[fac].Cathedras[cath].Teachers.Add(t);
        }

        public void DelFaculty()
        {
            int fac = ChoiseFaculty();
            if (menu.YesNoChoise("ВНИМАНИЕ! Все данные внутри факультета будут также удалены! Вы уверенны?")) Facultys.RemoveAt(fac);
        }

        public void DelGroup()
        {
            int fac = ChoiseFaculty();
            int kurs = ChoiseKurs();
            int gr = ChoiseGroup(fac, kurs);
            if (menu.YesNoChoise("ВНИМАНИЕ! Все данные внутри группы будут также удалены! Вы уверенны?"))Facultys[fac].Kurs[kurs].Groups.RemoveAt(gr);
        }

        public void DelStudent()
        {
            int fac = ChoiseFaculty();
            int kurs = ChoiseKurs();
            int group = ChoiseGroup(fac, kurs);
            if (Facultys[fac].Kurs[kurs].Groups[group].Students.Count == 0) throw new MyException("В этой группе нет студентов!");
            var studs = Facultys[fac].Kurs[kurs].Groups[group].Students.Select(x => x.Name + " " + x.Lastname + " " + x.Cathedra).ToList();
            myMenuClass menu = new myMenuClass(1, 1);
            int StudNum = menu.DrowMenuTitle(studs, "Выберите студента:");
            if (menu.YesNoChoise())
                Facultys[fac].Kurs[kurs].Groups[group].Students.RemoveAt(StudNum);

        }

        public void DelTeacher()
        {
            int fac = ChoiseFaculty();
            int cath = ChoiseCathedra(fac);
            if (Facultys[fac].Cathedras[cath].Teachers.Count == 0) throw new MyException("Данная кафедра не содержит преподавателей!");
            var teach = Facultys[fac].Cathedras[cath].Teachers.Select(x => x.Name + " " + x.Lastname).ToList();
            myMenuClass menu = new myMenuClass(1, 1);
            int TeachNum = menu.DrowMenuTitle(teach, "Выберите преподователя:");
            if (menu.YesNoChoise()) Facultys[fac].Cathedras[cath].Teachers.RemoveAt(TeachNum);
        }

        public void DelCathedra()
        {
            int fac = ChoiseFaculty();
            int cath = ChoiseCathedra(fac);
            if (menu.YesNoChoise("ВНИМАНИЕ!!! Все данные внутри кафедры будут также удалены! Вы уверенны?"))
            {
                foreach (var i in Facultys[fac].Kurs)
                {
                    foreach (var j in i.Groups)
                    {
                        j.Students.RemoveAll(x => x.Cathedra == Facultys[fac].Cathedras[cath].CathedraName);
                    }
                }

                Facultys[fac].Cathedras.RemoveAt(cath);
            }
        }

        public void ShowStudsOrderByCathAndKurs()
        {
            var StudList =new List<StudentClass>();
            int fac = ChoiseFaculty();
            int cath = ChoiseCathedra(fac);
            int kur = ChoiseKurs();
            foreach (var item in Facultys[fac].Kurs[kur].Groups)
            {
                StudList.AddRange(item.Students.Where(x => x.Cathedra == Facultys[fac].Cathedras[cath].CathedraName).OrderBy(x => x.Lastname));
            }
            StudList.ForEach(x => x.Print());
            Console.ReadKey();
        }

        public void ShowStudsOrderByCath()
        {
            var StudList = new List<StudentClass>();
            int fac = ChoiseFaculty();
            int cath = ChoiseCathedra(fac);
            foreach (var i in Facultys[fac].Kurs)
            {
                foreach (var j in i.Groups)
                {
                    StudList.AddRange(j.Students.Where(x => x.Cathedra == Facultys[fac].Cathedras[cath].CathedraName));
                }
            }
            var OrderedStudList = StudList.OrderBy(x => x.Lastname);
            foreach (var i in OrderedStudList)
            {
                i.Print();
            }
            Console.ReadKey();
        }

        public void ShowStudsOrderByFac()
        {
            int fac = ChoiseFaculty();
            for (int i = 0; i < Facultys[fac].Kurs.Count; i++)
            {
                Console.WriteLine(Facultys[fac].Kurs[i].Kurs + " курс:");
                for (int j = 0; j < Facultys[fac].Kurs[i].Groups.Count; j++)
                {
                    Console.WriteLine("\t Группа:" + Facultys[fac].Kurs[i].Groups[j].GroupName);
                    foreach (var stud in Facultys[fac].Kurs[i].Groups[j].Students)
                    {
                        Console.Write("\t\t");
                        stud.PrintCath();
                    }

                }

            }
            Console.ReadKey();
        }

        public void ShowStudsOrderByKurs()
        {
            var StudList = new List<StudentClass>();
            int kur = ChoiseKurs();
            foreach (var i in Facultys)
            {
                foreach (var j in i.Kurs[kur].Groups)
                {
                    StudList.AddRange(j.Students);
                }
            }
            StudList.ForEach(x => x.PrintAll());
            Console.ReadKey();
        }

        public void ShowAllFac()
        {
            for (int i = 0; i < Facultys.Count; i++)
            {
                Console.WriteLine(Facultys[i].FacultyName);
                for (int j = 0; j < Facultys[i].Cathedras.Count; j++)
                {
                    Console.WriteLine($"\t{Facultys[i].Cathedras[j].CathedraName}");

                }
                for (int j = 0; j < Facultys[i].Kurs.Count; j++)
                {
                    Console.WriteLine("\t" + Facultys[i].Kurs[j].Kurs.ToString() + "kurs :");
                    for (int k = 0; k < Facultys[i].Kurs[j].Groups.Count; k++)
                    {
                        Console.WriteLine("\t\t"+ Facultys[i].Kurs[j].Groups[k].GroupName);
                    }
                }

            }
        }

    }
}
