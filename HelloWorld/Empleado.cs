using System;
using System.Collections.Generic;
using System.Linq;

namespace HelloWorld
{
    public class Employee
    {
        public Store TiendaAsociada;
        private string _NIF;

        public string NIF
        {
            get => _NIF;
            set
            {
                if (Employee.IsNIFValid(value))
                {
                    _NIF = value;
                }
                else
                {
                    throw new Exception("El nif introducido no es válido");
                }
            }
        }
        public static bool IsNIFValid(string nif)
        {
            var result = Idgen.Net.IdGen.ValidateNif(nif);
            if (result != Idgen.Net.ValidationResult.Valid)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    public class EmployeeCollection : List<Employee>
    {
        public static List<Employee> GetByStoreIdStatic(int Id)
        {
            //Imaginemos que atacamos a una BBDD ficticia
            var ddbbEmployeeCol = new EmployeeCollection()
            {
                new Employee() { NIF = "B213123123", TiendaAsociada = new Store(1) },
                new Employee() { NIF = "B213428223", TiendaAsociada = new Store(1) },
                new Employee() { NIF = "B213465123", TiendaAsociada = new Store(2) },
                new Employee() { NIF = "B213856123", TiendaAsociada = new Store(2) },
                new Employee() { NIF = "B342344223", TiendaAsociada = new Store(4) },
                new Employee() { NIF = "B213451523", TiendaAsociada = new Store(1) },
            };

            //Devolvemos todos los empleados que tienen cierta tienda asignada

            return ddbbEmployeeCol.Where(empleado => empleado.TiendaAsociada.ID == Id).ToList();
        }

        public List<Employee> GetByStoreId(int Id)
        {
            return this.Where(empleado => empleado.TiendaAsociada.ID == Id).ToList();
        }
    }
}