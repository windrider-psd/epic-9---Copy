using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.source.DataManager
{
    [Serializable]
    public class PlayerData
    {
        [SerializeField]
        private string id;

        [SerializeField]
        private string name;

        public PlayerData()
        {
        }

        public PlayerData(string id)
        {
            Id = id;
        }

        public PlayerData(string id, string name) : this(id)
        {
            Name = name;
        }

        public string Id { get => id; private set => id = value; }
        public string Name { get => name; set => name = value; }

        public override bool Equals(object obj)
        {
            return obj is PlayerData data &&
                   Id == data.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
        public override string ToString()
        {
            return String.Format("Name: {0} | Id: {1}", this.id, this.name);
        }
    }

}
