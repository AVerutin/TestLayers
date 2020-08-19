using System;
using System.Collections.Generic;
using System.Text;

namespace TestLayers
{
    class Material
    {
        public string Name { get; }
        public double Weight { get; set; }

        public Material()
        {
            Name = default;
            Weight = default;
        }

        public Material(string name)
        {
            if (name != "")
            {
                Name = name;
                Weight = default;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public Material(string name, double weight)
        {
            if (name == "" || weight == 0)
            {
                throw new ArgumentNullException();
            }

            Name = name;
            Weight = weight;
        }
    }
}
