using Blog.Application.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Dtos.Settings
{
    public class SettingDto:EntityDto
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
