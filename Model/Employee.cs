﻿using System;

namespace Model
{
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime HireDate { get; set; }

    }
    public class Result<T>
    {
        public T Value { get; set; }
        public string otherValue { get; set; }
    }
}
