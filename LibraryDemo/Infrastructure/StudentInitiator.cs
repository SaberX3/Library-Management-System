﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using LibraryDemo.Data;
using LibraryDemo.Models.DomainModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryDemo.Infrastructure
{
    public class StudentInitiator
    {
        public static async Task InitialStudents(IServiceProvider serviceProvider)
        {
            UserManager<Student> userManager = serviceProvider.GetRequiredService<UserManager<Student>>();
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            if (userManager.Users.Any())
            {
                return;
            }

            if (await roleManager.FindByNameAsync("Admin")==null)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (await roleManager.FindByNameAsync("Student")==null)
            {
                await roleManager.CreateAsync(new IdentityRole("Student"));
            }

            IEnumerable<Student> initialStudents = new[]
            {
                new Student()
                {
                    UserName = "U20161120130",
                    Name = "mty",
                    Email = "tianyi_ma@foxmail.com",
                    PhoneNumber = "13064219231",
                    Degree = Degrees.CollegeStudent,
                    MaxBooksNumber = 10,
                },
                new Student()
                {
                    UserName = "U20161120233",
                    Name = "ww",
                    Email = "Nana@foxmail.com",
                    PhoneNumber = "12345678911",
                    Degree = Degrees.DoctorateDegree,
                    MaxBooksNumber = 15
                }
            };

            IEnumerable<Student> initialAdmins = new[]
            {
                new Student()
                {
                    UserName = "A000000000",
                    Name="Admin0000",
                    Email = "Admin@cnblog.com",
                    PhoneNumber = "12345678912",
                    Degree = Degrees.CollegeStudent,
                    MaxBooksNumber = 20
                },
                new Student()
                {
                    UserName = "A000000001",
                    Name = "Admin0001",
                    Email = "123456789@qq.com",
                    PhoneNumber = "12345678910",
                    Degree = Degrees.CollegeStudent,
                    MaxBooksNumber = 20
                },
            };
            foreach (var student in initialStudents)
            {
                await userManager.CreateAsync(student, student.UserName.Substring(student.UserName.Length - 6, 6));
            }
            foreach (var admin in initialAdmins)
            {
                await userManager.CreateAsync(admin, "mtymty123");
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}
