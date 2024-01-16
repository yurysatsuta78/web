using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using pro.Classes;
using pro.Filters;
using pro.Models;

namespace pro.Controllers
{
    public class MainController : Controller
    {
        //Создание объекта базы данных
        Datebase DB = new Datebase();


        //Страница авторизации
        #region страница авторизации
        public IActionResult Authorization()
        {
            return View();
        }
        #endregion


        //Страница с машинами + фильтр авторизации
        #region страница с машинами
        [TypeFilter(typeof(CustomAuthorizationFilter))]
        public IActionResult CarList() 
        {
            CarsViewModel model = new CarsViewModel();
            model.Cars = GetCarsFromDataBase();
            return View(model);
        }
        #endregion


        //Страница добавления новостей
        #region страница добавления новостей
        public IActionResult AddNew()
        {
            return View();
        }
        #endregion


        //Страница просмотра новостей
        #region страница просмотра новостей
        public IActionResult News() 
        {
            NewsViewModel model = new NewsViewModel();
            model.News = GetNewsFromDataBase();
            return View(model);
        }
        #endregion


        //Страница добавления автомобилей
        #region страница добавления автомобилей
        public IActionResult NewCar() 
        {
            return View();
        }
        #endregion


        //Метод добавления автомобиля
        #region добавление автомобиля в бд
        [HttpPost]
        public IActionResult AddCar(Car car) 
        {
            if (ModelState.IsValid)
            {
                #region проверка на наличие файла
                if (car.Image != null && car.Image.FileName.Length > 0) 
                {
                    #region проверка на форматы .jpg, .jpeg, .png
                    var extension = Path.GetExtension(car.Image.FileName);
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                    if (!allowedExtensions.Contains(extension))
                    {
                        ViewBag.ImageCheck = "Выберите изображение";
                        return View("NewCar");
                    }
                    else 
                    {
                        #region добавление автомобиля
                        #region настройка перед добавлением
                        byte[] imageBytes;
                        string addcar = String.Format("Insert Into Cars (Brand, Model, Year, Price, EngineVolume, EngineType, DriveUnit, Transmission, Image ) Values ( @Brand, @Model, @Year, @Price, @EngineVolume, @EngineType, @DriveUnit, @Transmission, @Image )");
                        SqlCommand cmd = new SqlCommand(addcar, DB.getConnection());
                        SqlParameter brandparam = new SqlParameter("@Brand", car.Brand);
                        SqlParameter modelparam = new SqlParameter("@Model", car.Model);
                        SqlParameter yearparam = new SqlParameter("@Year", car.Year);
                        SqlParameter priceparam = new SqlParameter("@Price", car.Price);
                        SqlParameter evparam = new SqlParameter("@EngineVolume", car.EngineVolume);
                        SqlParameter etparam = new SqlParameter("@EngineType", car.EngineType);
                        SqlParameter duparam = new SqlParameter("@DriveUnit", car.DriveUnit);
                        SqlParameter transmissionparam = new SqlParameter("@Transmission", car.Transmission);
                        cmd.Parameters.Add(brandparam);
                        cmd.Parameters.Add(modelparam);
                        cmd.Parameters.Add(yearparam);
                        cmd.Parameters.Add(priceparam);
                        cmd.Parameters.Add(evparam);
                        cmd.Parameters.Add(etparam);
                        cmd.Parameters.Add(duparam);
                        cmd.Parameters.Add(transmissionparam);
                        using (BinaryReader br = new BinaryReader(car.Image.OpenReadStream()))
                        {
                            imageBytes = br.ReadBytes((int)car.Image.Length);
                            var imgparam = new SqlParameter("@Image", System.Data.SqlDbType.VarBinary, (int)imageBytes.Length)
                            {
                                Value = imageBytes
                            };
                            cmd.Parameters.Add(imgparam);
                        }
                        #endregion
                        DB.openConnection();
                        cmd.ExecuteNonQuery();
                        DB.closeConnection();
                        ModelState.Clear();
                        return View("NewCar");
                        #endregion
                    }
                    #endregion
                }
                {
                    ViewBag.ImageCheck = "Выберите изображение";
                    return View("NewCar");
                }
                #endregion
            }
            else 
            {
                return View("NewCar");
            }
        }
        #endregion


        //Метод для получения новостей из базы данных
        #region получение новостей из бд
        private List<NewsForNewsViewModel> GetNewsFromDataBase()
        {
            NewsViewModel newsViewModel = new NewsViewModel();
            newsViewModel.News = new List<NewsForNewsViewModel>();
            string getnews = String.Format("Select * From News");
            SqlCommand cmd = new SqlCommand(getnews, DB.getConnection());
            DB.openConnection();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string title = reader.GetString(1);
                    string desc = reader.GetString(2);
                    DateTime date = reader.GetDateTime(3);
                    byte[] img = (byte[])reader["Image"];
                    NewsForNewsViewModel news = new NewsForNewsViewModel(id, title, desc, date, img);
                    newsViewModel.News.Add(news);
                }
            }
            return newsViewModel.News;
        }
        #endregion


        //Метод для получения автомобилей из базы данных
        #region получание автомобилей из бд
        private List<CarsForCarsViewModel> GetCarsFromDataBase() 
        {
            CarsViewModel carsViewModel = new CarsViewModel();
            carsViewModel.Cars = new List<CarsForCarsViewModel>();
            string getcars = String.Format("Select * From Cars");
            SqlCommand cmd = new SqlCommand(getcars, DB.getConnection());
            DB.openConnection();
            using (SqlDataReader reader = cmd.ExecuteReader()) 
            {
                while (reader.Read()) 
                {
                    int id = reader.GetInt32(0);
                    string brand = reader.GetString(1);
                    string model = reader.GetString(2);
                    string year = reader.GetString(3);
                    string price = reader.GetString(4);
                    string enginevolume = reader.GetString(5);
                    string enginetype = reader.GetString(6);
                    string driveunit = reader.GetString(7);
                    string transmission = reader.GetString(8);
                    byte[] img = (byte[])reader["Image"];
                    CarsForCarsViewModel car = new CarsForCarsViewModel(id, brand, model, year, price, enginevolume, enginetype, driveunit, transmission, img);
                    carsViewModel.Cars.Add(car);
                }
            }
            return carsViewModel.Cars;
        }
        #endregion


        //Метод добавления новости
        #region добавление новости в бд
        [HttpPost]
        public IActionResult SendNew(News news) 
        {
            if (ModelState.IsValid)
            {
                #region проверка на наличие файла
                if (news.Image != null && news.Image.FileName.Length > 0)
                {
                    #region проверка на форматы .jpg, .jpeg, .png
                    var extension = Path.GetExtension(news.Image.FileName);
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                    if (!allowedExtensions.Contains(extension))
                    {
                        ViewBag.ImageCheck = "Выберите изображение";
                        return View("AddNew");
                    }
                    else 
                    {
                        #region отправка новости
                        #region настройка перед отправкой
                        byte[] imageBytes;
                        news.Date = DateTime.Now;
                        string sendnewstr = String.Format("Insert Into News (Title, Description, Date, Image ) Values ( @Title, @Description, @Date, @Image )");
                        SqlCommand cmd = new SqlCommand(sendnewstr, DB.getConnection());
                        SqlParameter titleparam = new SqlParameter("@Title", news.Title);
                        SqlParameter descparam = new SqlParameter("@Description", news.Description);
                        SqlParameter dateparam = new SqlParameter("@Date", System.Data.SqlDbType.DateTime) 
                        {
                            Value = news.Date,
                        };
                        cmd.Parameters.Add(titleparam);
                        cmd.Parameters.Add(descparam);
                        cmd.Parameters.Add(dateparam);
                        using (BinaryReader br = new BinaryReader(news.Image.OpenReadStream()))
                        {
                            imageBytes = br.ReadBytes((int)news.Image.Length);
                            var imgparam = new SqlParameter("@Image", System.Data.SqlDbType.VarBinary, (int)imageBytes.Length)
                            {
                                Value = imageBytes
                            };
                            cmd.Parameters.Add(imgparam);
                        }
                        #endregion
                        DB.openConnection();
                        cmd.ExecuteNonQuery();
                        DB.closeConnection();
                        ModelState.Clear();
                        return View("AddNew");
                        #endregion
                    }
                    #endregion
                }
                else 
                {
                    ViewBag.ImageCheck = "Выберите изображение";
                    return View("AddNew");
                }
                #endregion

            }
            else 
            {
                return View("AddNew");
            }
        }
        #endregion


        //Метод регистрации
        #region регистрация
        [HttpPost]
        public IActionResult Reg(RegViewModel regViewModel)
        {
            if (ModelState.IsValid)
            {
                string reguser = String.Format("Insert Into Users Values ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')", regViewModel.Name, regViewModel.Surname, regViewModel.UserName, regViewModel.Password, regViewModel.PhoneNumber, 0);
                SqlCommand cmd = new SqlCommand(reguser, DB.getConnection());
                DB.openConnection();
                cmd.ExecuteNonQuery();
                DB.closeConnection();
                ModelState.Clear();
                return Redirect("Authorization");
            }
            else
            {
                return View("Authorization");
            }
        }
        #endregion


        //Метод авторизации
        #region авторизация
        [HttpPost]
        public IActionResult Log(LoginViewModel loginViewModel) 
        {
            if (ModelState.IsValid)
            {
                bool success = false;
                string checkuser = String.Format("Select * From Users Where UserName = '{0}' And Password collate Cyrillic_General_CS_AS = '{1}'", loginViewModel.AuthUserName, loginViewModel.AuthPassword);
                SqlCommand cmd = new SqlCommand(checkuser, DB.getConnection());
                DB.openConnection();
                using (SqlDataReader reader = cmd.ExecuteReader()) 
                {
                    success = reader.Read();
                    if (success)
                    {
                        CurrentUser.name = reader.GetString(1);
                        CurrentUser.surname = reader.GetString(2);
                        CurrentUser.username = reader.GetString(3);
                        CurrentUser.phonenumber = reader.GetString(5);
                        CurrentUser.admin = reader.GetInt32(6);
                        ViewBag.Success = "Успешная авторизация";
                        ViewBag.SuccessStyle = "successspantrue";
                        ModelState.Clear();
                    }
                    else 
                    {
                        ViewBag.Success = "Неверный логин или пароль";
                        ViewBag.SuccessStyle = "successspanfalse";
                    }
                }
                DB.closeConnection();
                return View("Authorization");
            }
            else
            {
                return View("Authorization");
            }
        }
        #endregion


        //Метод выхода из аккаунта
        #region выход из аккаунта
        public IActionResult LogOut() 
        {
            CurrentUser.ClearUserData();
            return RedirectToAction("Authorization", "Main");
        }
        #endregion


        //Метод удаления новости
        #region удаление новости
        public IActionResult DeleteNews([FromQuery] int id) 
        {
            string deletenews = String.Format("Delete From News Where Id = @newsid");
            SqlCommand cmd = new SqlCommand(deletenews, DB.getConnection());
            SqlParameter idparameter = new SqlParameter("@newsid", id);
            cmd.Parameters.Add(idparameter);
            DB.openConnection();
            cmd.ExecuteNonQuery();
            DB.closeConnection();
            return Redirect("News");
        }
        #endregion


        //Страница редактирования новостей
        #region страница редактирования новостей
        public IActionResult EditNews([FromQuery] int id) 
        {
            EditNewsModel model = new EditNewsModel();
            string getnews = String.Format("Select * From News Where Id = {0}", id);
            SqlCommand cmd = new SqlCommand(getnews, DB.getConnection());
            DB.openConnection();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    model.Id = reader.GetInt32(0);
                    model.Title = reader.GetString(1);
                    model.Description = reader.GetString(2);
                }
            }
            return View(model);
        }
        #endregion


        //Метод редактирования новостей
        #region метод редактирования новостей
        [HttpPost]
        public IActionResult EditNew(EditNewsModel editnews) 
        {
            if (ModelState.IsValid)
            {
                string edit = String.Format("Update News Set Title = @editedtitle, Description = @editeddesc Where Id = {0}", editnews.Id);
                SqlCommand cmd = new SqlCommand(edit, DB.getConnection());
                SqlParameter titleparam = new SqlParameter("@editedtitle", editnews.Title);
                SqlParameter descparam = new SqlParameter("@editeddesc", editnews.Description);
                cmd.Parameters.Add(titleparam);
                cmd.Parameters.Add(descparam);
                DB.openConnection();
                cmd.ExecuteNonQuery();
                DB.closeConnection();
                return Redirect("News");
            }
            else 
            {
                return View("EditNews");
            }
        }
        #endregion
    }
}
