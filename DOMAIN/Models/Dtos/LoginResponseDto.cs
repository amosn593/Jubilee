﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Models.Dtos;
public class LoginResponseDto
{
    public string Token { get; set; }
    public User user { get; set; }
}
