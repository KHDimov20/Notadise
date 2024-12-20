﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Notadise.Areas.Identity.Data;

// Добавяне на данни за профила на потребителите на приложения чрез добавяне на свойства към класа ApplicationUser 
public class ApplicationUser : IdentityUser
{
    [PersonalData]
    [Column (TypeName = "nvarchar(100)")]
    public string FirstName { get; set; }

    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string LastName { get; set; }

}

