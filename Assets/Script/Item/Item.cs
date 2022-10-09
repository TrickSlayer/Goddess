using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Goddess.Item
{
    public abstract class Item
    {
        protected CharacterStats player;
        public Item()
        {
            GameObject playerTag = GameObject.FindGameObjectWithTag("Player");
            player = playerTag.GetComponent<CharacterStats>();
        }
    }
}
