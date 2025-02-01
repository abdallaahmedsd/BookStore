using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BusinessLogic
{
    public class TestService
    {
        public List<Test> GetAll()
        {
            return new List<Test>
        {
            new Test { Id = 1, Name = "Test Case 1" },
            new Test { Id = 2, Name = "Test Case 2" },
            new Test { Id = 3, Name = "Test Case 3" }
        };
        }

    }
}
