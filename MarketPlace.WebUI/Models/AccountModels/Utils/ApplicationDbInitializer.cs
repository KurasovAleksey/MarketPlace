using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MarketPlace.WebUI.Models.AccountModels.Utils
{
    public class ApplicationDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            InitializeIdentityForEF(context);
            base.Seed(context);
        }

        public static void InitializeIdentityForEF(ApplicationDbContext db)
        {
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var roleManager = HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
            const string roleNameAdmin = "Admin";
            const string descriptionAdmin = "Управление полномочиями, модерация, рассмотр жалоб";
            const string roleNameUser = "User";
            const string descriptionUser = "Базовый уровень полномочий: право участия в торгах";

            var role = roleManager.FindByName(roleNameUser);
            if (role == null)
            {
                role = new ApplicationRole(roleNameUser) { Description = descriptionUser };
                var roleresult = roleManager.Create(role);
            }
            role = roleManager.FindByName(roleNameAdmin);
            if (role == null)
            {
                role = new ApplicationRole(roleNameAdmin) { Description = descriptionAdmin };
                var roleresult = roleManager.Create(role);
            }


            const string userName = "FlPhoenix";
            var admin = userManager.FindByName(userName);
            if (admin == null)
            {
                admin = new ApplicationUser
                {
                    Name = "Алексей",
                    Sname = "Курасов",
                    UserName = userName,
                    Email = "justaleks97@gmail.com",
                    PhoneNumber = "0938093535"
                };
                const string password = "Pass7610";
                var result = userManager.Create(admin, password);
                result = userManager.SetLockoutEnabled(admin.Id, false);

                var roleResult = userManager.AddToRole(admin.Id, "User");
                roleResult = userManager.AddToRole(admin.Id, "Admin");
            }

            var userList = new[] {
                new
                {
                    User = new ApplicationUser
                    {
                        Name = "Павел",
                        Sname = "Тропов",
                        UserName = "PashaT",
                        Email = "pasha.trop@gmail.com",
                        PhoneNumber = "0938095566"
                    },
                    Password = "Password1"
                },
                new
                {
                    User = new ApplicationUser
                    {
                        Name = "Юра",
                        Sname = "Павлов",
                        UserName = "Yurka97",
                        Email = "Pavlov.Yura@mail.ru",
                        PhoneNumber = "0972907656"
                    },
                    Password = "Password1"
                },
                new
                {
                    User = new ApplicationUser
                    {
                        Name = "Лев",
                        Sname = "Лесовой",
                        UserName = "Primarch40k",
                        Email = "lionAlJonhson@gmail.com",
                        PhoneNumber = "0637897645"
                    },
                    Password = "Password1"
                },
                new
                {
                    User = new ApplicationUser
                    {
                        Name = "Михаил",
                        Sname = "Вершинин",
                        UserName = "FleaSeller",
                        Email = "mishaVersh@mail.ru",
                        PhoneNumber = "0578903443"
                    },
                    Password = "Password1"
                },
                new
                {
                    User = new ApplicationUser
                    {
                        Name = "Александр",
                        Sname = "Рязанов",
                        UserName = "AlexKopach",
                        Email = "Ryazanov@rambler.ru",
                        PhoneNumber = "0674903212"
                    },
                    Password = "Password1"
                },
                new
                {
                    User = new ApplicationUser
                    {
                        Name = "Андрей",
                        Sname = "Маковецкий",
                        UserName = "Faust41",
                        Email = "andrew.macov@gmail.com",
                        PhoneNumber = "0667895412"
                    },
                    Password = "Password1"
                },



                new
                {
                    User = new ApplicationUser
                    {
                        Name = "Иван",
                        Sname = "Орлов",
                        UserName = "BonoboJohn",
                        Email = "ivan.orlov@gmail.com",
                        PhoneNumber = "0667895412"
                    },
                    Password = "Password1"
                },

                new
                {
                    User = new ApplicationUser
                    {
                        Name = "Сергей",
                        Sname = "Рак",
                        UserName = "RustStalker",
                        Email = "ivan.orlov@gmail.com",
                        PhoneNumber = "0667895410"
                    },
                    Password = "Password1"
                },

                new
                {
                    User = new ApplicationUser
                    {
                        Name = "Юлия",
                        Sname = "Подгородная",
                        UserName = "julka23",
                        Email = "Julia.Podgorod@gmail.com",
                        PhoneNumber = "0667895411"
                    },
                    Password = "Password1"
                },

                new
                {
                    User = new ApplicationUser
                    {
                        Name = "Аркадий",
                        Sname = "Пушкин",
                        UserName = "julka23",
                        Email = "arkashaPush@gmail.com",
                        PhoneNumber = "0667895411"
                    },
                    Password = "Password1"
                },

                //new
                //{
                //    User = new ApplicationUser
                //    {
                //        Name = "",
                //        Sname = "Подгородная",
                //        UserName = "julka23",
                //        Email = "Julia.Podgorod@gmail.com",
                //        PhoneNumber = "0667895411"
                //    },
                //    Password = "Password1"
                //},

                //new
                //{
                //    User = new ApplicationUser
                //    {
                //        Name = "Юлия",
                //        Sname = "Подгородная",
                //        UserName = "julka23",
                //        Email = "Julia.Podgorod@gmail.com",
                //        PhoneNumber = "0667895411"
                //    },
                //    Password = "Password1"
                //},

                //new
                //{
                //    User = new ApplicationUser
                //    {
                //        Name = "Юлия",
                //        Sname = "Подгородная",
                //        UserName = "julka23",
                //        Email = "Julia.Podgorod@gmail.com",
                //        PhoneNumber = "0667895411"
                //    },
                //    Password = "Password1"
                //},

                //new
                //{
                //    User = new ApplicationUser
                //    {
                //        Name = "Юлия",
                //        Sname = "Подгородная",
                //        UserName = "julka23",
                //        Email = "Julia.Podgorod@gmail.com",
                //        PhoneNumber = "0667895411"
                //    },
                //    Password = "Password1"
                //},

                //new
                //{
                //    User = new ApplicationUser
                //    {
                //        Name = "Юлия",
                //        Sname = "Подгородная",
                //        UserName = "julka23",
                //        Email = "Julia.Podgorod@gmail.com",
                //        PhoneNumber = "0667895411"
                //    },
                //    Password = "Password1"
                //},

                //new
                //{
                //    User = new ApplicationUser
                //    {
                //        Name = "Юлия",
                //        Sname = "Подгородная",
                //        UserName = "julka23",
                //        Email = "Julia.Podgorod@gmail.com",
                //        PhoneNumber = "0667895411"
                //    },
                //    Password = "Password1"
                //},

                //new
                //{
                //    User = new ApplicationUser
                //    {
                //        Name = "Юлия",
                //        Sname = "Подгородная",
                //        UserName = "julka23",
                //        Email = "Julia.Podgorod@gmail.com",
                //        PhoneNumber = "0667895411"
                //    },
                //    Password = "Password1"
                //},

                //new
                //{
                //    User = new ApplicationUser
                //    {
                //        Name = "Юлия",
                //        Sname = "Подгородная",
                //        UserName = "julka23",
                //        Email = "Julia.Podgorod@gmail.com",
                //        PhoneNumber = "0667895411"
                //    },
                //    Password = "Password1"
                //},

                //new
                //{
                //    User = new ApplicationUser
                //    {
                //        Name = "Юлия",
                //        Sname = "Подгородная",
                //        UserName = "julka23",
                //        Email = "Julia.Podgorod@gmail.com",
                //        PhoneNumber = "0667895411"
                //    },
                //    Password = "Password1"
                //},

                //new
                //{
                //    User = new ApplicationUser
                //    {
                //        Name = "Юлия",
                //        Sname = "Подгородная",
                //        UserName = "julka23",
                //        Email = "Julia.Podgorod@gmail.com",
                //        PhoneNumber = "0667895411"
                //    },
                //    Password = "Password1"
                //},

                //new
                //{
                //    User = new ApplicationUser
                //    {
                //        Name = "Юлия",
                //        Sname = "Подгородная",
                //        UserName = "julka23",
                //        Email = "Julia.Podgorod@gmail.com",
                //        PhoneNumber = "0667895411"
                //    },
                //    Password = "Password1"
                //},

                //new
                //{
                //    User = new ApplicationUser
                //    {
                //        Name = "Юлия",
                //        Sname = "Подгородная",
                //        UserName = "julka23",
                //        Email = "Julia.Podgorod@gmail.com",
                //        PhoneNumber = "0667895411"
                //    },
                //    Password = "Password1"
                //},

                //new
                //{
                //    User = new ApplicationUser
                //    {
                //        Name = "Юлия",
                //        Sname = "Подгородная",
                //        UserName = "julka23",
                //        Email = "Julia.Podgorod@gmail.com",
                //        PhoneNumber = "0667895411"
                //    },
                //    Password = "Password1"
                //},

                //new
                //{
                //    User = new ApplicationUser
                //    {
                //        Name = "Юлия",
                //        Sname = "Подгородная",
                //        UserName = "julka23",
                //        Email = "Julia.Podgorod@gmail.com",
                //        PhoneNumber = "0667895411"
                //    },
                //    Password = "Password1"
                //},

                //new
                //{
                //    User = new ApplicationUser
                //    {
                //        Name = "Юлия",
                //        Sname = "Подгородная",
                //        UserName = "julka23",
                //        Email = "Julia.Podgorod@gmail.com",
                //        PhoneNumber = "0667895411"
                //    },
                //    Password = "Password1"
                //},

                //new
                //{
                //    User = new ApplicationUser
                //    {
                //        Name = "Юлия",
                //        Sname = "Подгородная",
                //        UserName = "julka23",
                //        Email = "Julia.Podgorod@gmail.com",
                //        PhoneNumber = "0667895411"
                //    },
                //    Password = "Password1"
                //},

                //new
                //{
                //    User = new ApplicationUser
                //    {
                //        Name = "Юлия",
                //        Sname = "Подгородная",
                //        UserName = "julka23",
                //        Email = "Julia.Podgorod@gmail.com",
                //        PhoneNumber = "0667895411"
                //    },
                //    Password = "Password1"
                //},

                //new
                //{
                //    User = new ApplicationUser
                //    {
                //        Name = "Юлия",
                //        Sname = "Подгородная",
                //        UserName = "julka23",
                //        Email = "Julia.Podgorod@gmail.com",
                //        PhoneNumber = "0667895411"
                //    },
                //    Password = "Password1"
                //},

                //new
                //{
                //    User = new ApplicationUser
                //    {
                //        Name = "Юлия",
                //        Sname = "Подгородная",
                //        UserName = "julka23",
                //        Email = "Julia.Podgorod@gmail.com",
                //        PhoneNumber = "0667895411"
                //    },
                //    Password = "Password1"
                //},

                //new
                //{
                //    User = new ApplicationUser
                //    {
                //        Name = "Юлия",
                //        Sname = "Подгородная",
                //        UserName = "julka23",
                //        Email = "Julia.Podgorod@gmail.com",
                //        PhoneNumber = "0667895411"
                //    },
                //    Password = "Password1"
                //},

                //new
                //{
                //    User = new ApplicationUser
                //    {
                //        Name = "Юлия",
                //        Sname = "Подгородная",
                //        UserName = "julka23",
                //        Email = "Julia.Podgorod@gmail.com",
                //        PhoneNumber = "0667895411"
                //    },
                //    Password = "Password1"
                //},

                //new
                //{
                //    User = new ApplicationUser
                //    {
                //        Name = "Юлия",
                //        Sname = "Подгородная",
                //        UserName = "julka23",
                //        Email = "Julia.Podgorod@gmail.com",
                //        PhoneNumber = "0667895411"
                //    },
                //    Password = "Password1"
                //},

                //new
                //{
                //    User = new ApplicationUser
                //    {
                //        Name = "Юлия",
                //        Sname = "Подгородная",
                //        UserName = "julka23",
                //        Email = "Julia.Podgorod@gmail.com",
                //        PhoneNumber = "0667895411"
                //    },
                //    Password = "Password1"
                //},

                //new
                //{
                //    User = new ApplicationUser
                //    {
                //        Name = "Юлия",
                //        Sname = "Подгородная",
                //        UserName = "julka23",
                //        Email = "Julia.Podgorod@gmail.com",
                //        PhoneNumber = "0667895411"
                //    },
                //    Password = "Password1"
                //},

                //new
                //{
                //    User = new ApplicationUser
                //    {
                //        Name = "Юлия",
                //        Sname = "Подгородная",
                //        UserName = "julka23",
                //        Email = "Julia.Podgorod@gmail.com",
                //        PhoneNumber = "0667895411"
                //    },
                //    Password = "Password1"
                //},

                //new
                //{
                //    User = new ApplicationUser
                //    {
                //        Name = "Юлия",
                //        Sname = "Подгородная",
                //        UserName = "julka23",
                //        Email = "Julia.Podgorod@gmail.com",
                //        PhoneNumber = "0667895411"
                //    },
                //    Password = "Password1"
                //},

                //new
                //{
                //    User = new ApplicationUser
                //    {
                //        Name = "Юлия",
                //        Sname = "Подгородная",
                //        UserName = "julka23",
                //        Email = "Julia.Podgorod@gmail.com",
                //        PhoneNumber = "0667895411"
                //    },
                //    Password = "Password1"
                //},

                //new
                //{
                //    User = new ApplicationUser
                //    {
                //        Name = "Юлия",
                //        Sname = "Подгородная",
                //        UserName = "julka23",
                //        Email = "Julia.Podgorod@gmail.com",
                //        PhoneNumber = "0667895411"
                //    },
                //    Password = "Password1"
                //},

                //new
                //{
                //    User = new ApplicationUser
                //    {
                //        Name = "Юлия",
                //        Sname = "Подгородная",
                //        UserName = "julka23",
                //        Email = "Julia.Podgorod@gmail.com",
                //        PhoneNumber = "0667895411"
                //    },
                //    Password = "Password1"
                //},

                //new
                //{
                //    User = new ApplicationUser
                //    {
                //        Name = "Юлия",
                //        Sname = "Подгородная",
                //        UserName = "julka23",
                //        Email = "Julia.Podgorod@gmail.com",
                //        PhoneNumber = "0667895411"
                //    },
                //    Password = "Password1"
                //},

                //new
                //{
                //    User = new ApplicationUser
                //    {
                //        Name = "Юлия",
                //        Sname = "Подгородная",
                //        UserName = "julka23",
                //        Email = "Julia.Podgorod@gmail.com",
                //        PhoneNumber = "0667895411"
                //    },
                //    Password = "Password1"
                //},

                //new
                //{
                //    User = new ApplicationUser
                //    {
                //        Name = "Юлия",
                //        Sname = "Подгородная",
                //        UserName = "julka23",
                //        Email = "Julia.Podgorod@gmail.com",
                //        PhoneNumber = "0667895411"
                //    },
                //    Password = "Password1"
                //},

                //new
                //{
                //    User = new ApplicationUser
                //    {
                //        Name = "Юлия",
                //        Sname = "Подгородная",
                //        UserName = "julka23",
                //        Email = "Julia.Podgorod@gmail.com",
                //        PhoneNumber = "0667895411"
                //    },
                //    Password = "Password1"
                //},

                //new
                //{
                //    User = new ApplicationUser
                //    {
                //        Name = "Юлия",
                //        Sname = "Подгородная",
                //        UserName = "julka23",
                //        Email = "Julia.Podgorod@gmail.com",
                //        PhoneNumber = "0667895411"
                //    },
                //    Password = "Password1"
                //},

                //new
                //{
                //    User = new ApplicationUser
                //    {
                //        Name = "Юлия",
                //        Sname = "Подгородная",
                //        UserName = "julka23",
                //        Email = "Julia.Podgorod@gmail.com",
                //        PhoneNumber = "0667895411"
                //    },
                //    Password = "Password1"
                //},

            };

            foreach (var user in userList)
            {
                var temp = userManager.FindByName(user.User.UserName);
                if (temp == null)
                {
                    temp = user.User;
                    var result = userManager.Create(temp, user.Password);
                    result = userManager.SetLockoutEnabled(temp.Id, false);
                    var roleResult = userManager.AddToRole(temp.Id, "User");
                }
            }



            var categoryList = new List<Category>
            {
                new Category { CategoryId = 1, Title = "Электроника", Description = "Электронные устройства различного назначения"  },
                new Category { CategoryId = 2, Title = "Мода", Description = "Одежда, обувь и аксессуары"  },
                new Category { CategoryId = 3, Title = "Дом и приусадебное хозяйство", Description = "Все для домашнего хозяйства"  },
                new Category { CategoryId = 4, Title = "Другое", Description = "Товары невошедших категорий"  },

                new Category { CategoryId = 5 ,Title = "Развлечения", Description = "Игровые приставки и компьютеры, аксессуары к ним", ParentId = 1  },
                new Category { CategoryId = 6 ,Title = "Компьютеры и планшеты", Description = "ПК, лэптопы и тд.", ParentId = 1  },
                new Category { CategoryId = 7 ,Title = "Смартфоны и КПК", Description = "Портативные средства для связи и вычислений", ParentId = 1  },

                new Category { CategoryId = 8 ,Title = "Мужская одежда", Description = "Одежда мужских фасонов", ParentId = 2  },
                new Category { CategoryId = 9 ,Title = "Женская одежда", Description = "Одежда женских фасонов", ParentId = 2  },
                new Category { CategoryId = 10 ,Title = "Обувь", Description = "Обувь различных видов", ParentId = 2  },
                new Category { CategoryId = 11 ,Title = "Аксессуары", Description = "Часы, браслеты, украшения", ParentId = 2  },

                new Category { CategoryId = 12 ,Title = "Бытовая техника", Description = "Техника для обустройства дома", ParentId = 3  },
                new Category { CategoryId = 13 ,Title = "Инструменты", Description = "Инструменты для различной деятельности", ParentId = 3  },
                new Category { CategoryId = 14 ,Title = "Кухня", Description = "Аксессуары для кухонь и столовых", ParentId = 3  },
                new Category { CategoryId = 15 ,Title = "Мебель", Description = "Мебель различных видов", ParentId = 3  },
            };
            db.Categories.AddRange(categoryList);

            db.SaveChanges();


        }
    }
}