﻿namespace TokenBaseAuth.Entites
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
