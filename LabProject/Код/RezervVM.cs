using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LabProject.SQL;
using LabProject.Tools;
using LabProject.ViewModel;
using LabProject.Таблицы;

namespace LabProject.Код
{
    class RezervVM : BaseVM
    {

        public Rezerv EditRez { get; set; }

        public CommandVM CreateRezerv { get; set; }

        public List<Users> Users { get; set; }
        public List<Equipment> Equis { get; set; }
        public Users SelectedUser
        {
            get => selectedUser;
            set
            {
                selectedUser = value;
                Signal();
            }
        }
        public Equipment SelectedEqui
        {
            get => selectedEqui;
            set
            {
                selectedEqui = value;
                Signal();
            }
        }

        


        private Users selectedUser;
        private Equipment selectedEqui;
        public RezervVM()
        {
            EditRez = new Rezerv();
            InitCommand();

            RowsCountVariants = new int[] { 2, 5, 10 };
            ViewRowsCount = 5;

            ViewBack = new CommandVM(() =>
            {
                if (SelectedIndex > 1)
                    SelectedIndex--;
            });

            ViewForward = new CommandVM(() =>
            {
                if (SelectedIndex < PageIndexes.Last())
                    SelectedIndex++;
            });
        }
        public RezervVM(Rezerv editRez)
        {
            EditRez = editRez;
            InitCommand();
            SelectedUser = Users.FirstOrDefault(s => s.ID == editRez.UserId);
            SelectedEqui = Equis.FirstOrDefault(s => s.ID == editRez.EquipmentId);

        }
        private void InitCommand()
        {
            Users = SqlModel.GetInstance().UsersView();
            Equis = SqlModel.GetInstance().EquisView();
            CreateRezerv = new CommandVM(() =>
            {              
                if (SelectedUser == null || SelectedEqui == null)
                {
                    System.Windows.MessageBox.Show("Введены не все данные");
                    return;
                }
                EditRez.UserId = SelectedUser.ID;
                EditRez.EquipmentId = SelectedEqui.ID;
                var model = SqlModel.GetInstance();
                if (EditRez.ID == 0)
                    model.Insert(EditRez);
                else
                    model.Update(EditRez);

                

            });
        }

        

        private List<Rezerv> rezs;
        private List<int> pageIndexes;
        private int selectedIndex;
        private int viewRowsCount;

        public List<Rezerv> Rezervs
        {
            get => rezs;
            set
            {
                rezs = value;
                Signal();
            }
        }
        public CommandVM ViewBack { get; set; }
        public CommandVM ViewForward { get; set; }
        public List<int> PageIndexes
        {
            get => pageIndexes;
            set
            {
                pageIndexes = value;
                Signal();
            }
        }
        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                selectedIndex = value;
                Rezervs = SqlModel.GetInstance().RezView();
                Signal();
            }
        }
        public int[] RowsCountVariants { get; set; }
        public int ViewRowsCount
        {
            get => viewRowsCount;
            set
            {
                viewRowsCount = value;
                InitPages();
                Signal();
            }
        }

        

        private void InitPages()
        {
            var sqlModel = SqlModel.GetInstance();
            int pageCount = (sqlModel.GetNumRows(typeof(Rezerv)) / ViewRowsCount) + 1;
            PageIndexes = new List<int>(Enumerable.Range(1, pageCount));
            SelectedIndex = 1;
        }
    }
    }

