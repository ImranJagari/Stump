﻿using System.Collections.Generic;
using Stump.Server.WorldServer.Database.Items;
using Stump.Server.WorldServer.Database.Items.Templates;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Effects.Instances;

namespace Stump.Server.WorldServer.Game.Items
{
    public class MerchantItem : Item<PlayerMerchantItemRecord>
    {
        #region Fields

        public Character Owner
        {
            get;
            private set;
        }

        public int Price
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        public MerchantItem(Character owner, PlayerMerchantItemRecord record)
            : base(record)
        {
            //m_objectItemValidator = new ObjectValidator<ObjectItem>(BuildObjectItem);

            Owner = owner;
            Price = Record.Price;
        }

        public MerchantItem(Character owner, int guid, ItemTemplate template, List<EffectBase> effects, int stack, int price)
        {
            //m_objectItemValidator = new ObjectValidator<ObjectItem>(BuildObjectItem);
            Owner = owner;
            Price = price;

            Record = new PlayerMerchantItemRecord // create the associated record
                         {
                             Id = guid,
                             OwnerId = owner.Id,
                             Template = template,
                             Stack = stack,
                             Price = price,
                             Effects = effects,
                         };
        }

        #endregion

        #region Functions

        #endregion
    }
}
