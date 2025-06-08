using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class EmployeeTests
    {
        [TestMethod]
        public void Employee_Constructor_SetsProperties()
        {
            var emp = new Developer("Иван", 30, 100000);

            Assert.AreEqual("Иван", emp.Name);
            Assert.AreEqual(30, emp.Age);
            Assert.AreEqual(100000, emp.GetSalary());
            Assert.IsTrue(emp.Id > 0);
        }

        [TestMethod]
        public void Employee_SetSalary_UpdatesValue()
        {
            var emp = new Developer("Тест", 25, 50000);
            emp.SetSalary(60000);

            Assert.AreEqual(60000, emp.GetSalary());
        }

        [TestMethod]
        public void Employee_SetNegativeSalary_DoesNotUpdate()
        {
            var emp = new Developer("Тест", 25, 50000);
            emp.SetSalary(-1000);

            Assert.AreEqual(50000, emp.GetSalary());
        }

        [TestMethod]
        public void Developer_GetPosition_ReturnsCorrectValue()
        {
            var dev = new Developer("Тест", 25, 50000);
            Assert.AreEqual("Разработчик программного обеспечения", dev.Position);
        }

        [TestMethod]
        public void ProjectManager_GetPosition_ReturnsCorrectValue()
        {
            var pm = new ProjectManager("Тест", 30, 80000);
            Assert.AreEqual("Менеджер проекта", pm.Position);
        }

        [TestMethod]
        public void CEO_GetPosition_ReturnsCorrectValue()
        {
            var ceo = new CEO("Тест", 40, 150000);
            Assert.AreEqual("Генеральный директор", ceo.Position);
        }
    }
    [TestClass]
    public class CompanyTests
    {
        private Company _company;
        private Developer _developer;
        private ProjectManager _manager;

        [TestInitialize]
        public void Setup()
        {
            _company = new Company();
            _developer = new Developer("Иван", 30, 100000);
            _manager = new ProjectManager("Алексей", 35, 150000);
        }

        [TestMethod]
        public void HireEmployee_AddsEmployeeToList()
        {
            _company.HireEmployee(_developer);

            Assert.AreEqual(1, _company.Employees.Count);
            Assert.AreSame(_developer, _company.Employees.First());
        }

        [TestMethod]
        public void FireEmployee_RemovesEmployeeFromList()
        {
            _company.HireEmployee(_developer);
            _company.FireEmployee(_developer.Id);

            Assert.AreEqual(0, _company.Employees.Count);
        }

        [TestMethod]
        public void FireEmployee_InvalidId_DoesNothing()
        {
            _company.HireEmployee(_developer);
            _company.FireEmployee(999);

            Assert.AreEqual(1, _company.Employees.Count);
        }

        [TestMethod]
        public void ChangeSalary_UpdatesEmployeeSalary()
        {
            _company.HireEmployee(_developer);
            _company.ChangeSalary(_developer.Id, 120000);

            Assert.AreEqual(120000, _developer.GetSalary());
        }

        [TestMethod]
        public void ChangeSalary_InvalidId_DoesNothing()
        {
            _company.HireEmployee(_developer);
            double originalSalary = _developer.GetSalary();
            _company.ChangeSalary(999, 120000);

            Assert.AreEqual(originalSalary, _developer.GetSalary());
        }

        [TestMethod]
        public void ShowAllEmployees_EmptyList_PrintsNoEmployees()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                _company.ShowAllEmployees();

                Assert.IsTrue(sw.ToString().Contains("Сотрудников нет"));
            }
        }

        [TestMethod]
        public void ShowAllEmployees_WithEmployees_PrintsAll()
        {
            _company.HireEmployee(_developer);
            _company.HireEmployee(_manager);

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                _company.ShowAllEmployees();
                var output = sw.ToString();

                Assert.IsTrue(output.Contains(_developer.Name));
                Assert.IsTrue(output.Contains(_manager.Name));
            }
        }
    }

}