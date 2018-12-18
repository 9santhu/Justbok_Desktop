using JustbokApplication.Data;
using JustbokApplication.Models;
using JustbokApplication.Views;
using JustbokApplication.Views.Settings;
using System;

namespace JustbokApplication.ViewModel
{
    public class CategoryViewModel : BaseViewModel
    {
        private string _categorySearch = "";
        private string _descriptionSearch = "";

        public CategoryViewModel()
        {
            SortColumn = "CategoryName";
            Ascending = true;
            ItemCount = 10;
            RefreshItems();
        }

        public string CategorySearch { get => _categorySearch; set => _categorySearch = value; }
        public string DescriptionSearch { get => _descriptionSearch; set => _descriptionSearch = value; }

        public override void RefreshItems()
        {
            try
            {
                ShowLoader();
                Result result = new CategoryDao().GetCategories(CategorySearch,DescriptionSearch,SortColumn, Ascending ? "ASC" : "DESC", StartIndex, ItemCount);

                if (result != null)
                {
                    Items = result.Items;
                    TotalItems = result.ItemCount;
                }
                else
                {
                    Items = null;
                    TotalItems = 0;
                }
                NotifyPropertyChanged("Start");
                NotifyPropertyChanged("End");
                NotifyPropertyChanged("TotalItems");

                NotifyPropertyChanged("CategorySearch");
                NotifyPropertyChanged("DescriptionSearch");
                HideLoader();
            }
            catch (Exception)
            {
                HideLoader();
            }
        }

        public override void OpeningDialog(object obj)
        {
            ShowLoader();
            CategoryInformationById(Convert.ToInt32(obj));
            DialogContent = new AddEditCategoryView();
            IsDialogOpen = true;
            HideLoader();
        }

        public override void SaveContent(object obj)
        {
            try
            {
                ShowLoader();
                Category category = Item as Category;
                int ProducId = new CategoryDao().CategoryInsUpd(category, SessionManager.UserId);
                IsDialogOpen = false;
                HideLoader();
                DescriptionSearch = "";
                CategorySearch = "";
                StartIndex = 1;
                RefreshItems();
            }
            catch (Exception)
            {

            }
        }

        private void CategoryInformationById(int Id)
        {
            try
            {
                if (Id != 0)
                {
                    Item = new CategoryDao().CategoryById(Id);

                    if (Item == null)
                    {
                        Item = new Category();
                    }
                }
                else
                {
                    Item = new Category();
                }
            }
            catch (Exception ex)
            {
            }
        }

        public override void Delete(object obj)
        {
            try
            {
                ShowLoader();
                int ProductId = new CategoryDao().DeleteCategoryById((int)Id,SessionManager.UserId);
                IsDialogOpen = false;
                HideLoader();
                StartIndex = 1;
                RefreshItems();
            }
            catch (Exception)
            {

            }
        }

        public override void OpeningSubDialog(object obj)
        {
            throw new NotImplementedException();
        }

        public override void SaveSubContent(object obj)
        {
            throw new NotImplementedException();
        }

        public override void ClearValues()
        {
            DescriptionSearch = "";
            CategorySearch = "";
        }
    }
}
