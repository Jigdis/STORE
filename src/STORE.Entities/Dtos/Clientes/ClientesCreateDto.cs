﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STORE.Entities.Dtos.Clientes
{
    public class ClientesCreateDto
    {
        public int? ClienteID { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }

    }
}