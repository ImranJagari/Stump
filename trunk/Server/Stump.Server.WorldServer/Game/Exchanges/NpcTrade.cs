﻿#region License GNU GPL
// NpcTrade.cs
// 
// Copyright (C) 2013 - BehaviorIsManaged
// 
// This program is free software; you can redistribute it and/or modify it 
// under the terms of the GNU General Public License as published by the Free Software Foundation;
// either version 2 of the License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; 
// without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. 
// See the GNU General Public License for more details. 
// You should have received a copy of the GNU General Public License along with this program; 
// if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
#endregion

using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Exchanges.Items;
using Stump.Server.WorldServer.Handlers.Inventory;

namespace Stump.Server.WorldServer.Game.Exchanges
{
    public class NpcTrade : Trade<PlayerTrader, NpcTrader>
    {
        public NpcTrade() 
            : base(0)
        {
        }

        public override ExchangeTypeEnum ExchangeType
        {
            get { return ExchangeTypeEnum.NPC_TRADE; }
        }

        public override void Open(PlayerTrader firstTrader, NpcTrader secondTrader)
        {
            base.Open(firstTrader, secondTrader);

            InventoryHandler.SendExchangeStartOkNpcTradeMessage(FirstTrader.Character.Client, this);
        }

        public override void Close()
        {
            base.Close();

            InventoryHandler.SendExchangeLeaveMessage(FirstTrader.Character.Client, DialogTypeEnum.DIALOG_EXCHANGE,
                                                      FirstTrader.ReadyToApply);

            FirstTrader.Character.CloseDialog(this);
        }

        protected override void Apply()
        {                            
            // check all items are still there

            if (!FirstTrader.Items.All(x =>
                {
                    var item = FirstTrader.Character.Inventory.TryGetItem(x.Guid);
                    
                    if (item == null || item.Stack < x.Stack)
                        return false;

                    return true;
                }))
            {
                return;
            }

            FirstTrader.Character.Inventory.SetKamas(
                (int)( FirstTrader.Character.Inventory.Kamas + ( SecondTrader.Kamas - FirstTrader.Kamas ) ));


            // trade items
            foreach (var tradeItem in FirstTrader.Items)
            {
                var item = FirstTrader.Character.Inventory.TryGetItem(tradeItem.Guid);
                    FirstTrader.Character.Inventory.RemoveItem(item, tradeItem.Stack);
            }

            foreach (var tradeItem in SecondTrader.Items)
            {
                FirstTrader.Character.Inventory.AddItem(tradeItem.Template, tradeItem.Stack);
            }

            InventoryHandler.SendInventoryWeightMessage(FirstTrader.Character.Client);

        }

        protected override void OnTraderReadyStatusChanged(Trader trader, bool status)
        {
            base.OnTraderReadyStatusChanged(trader, status);

            InventoryHandler.SendExchangeIsReadyMessage(FirstTrader.Character.Client,
                                                        trader, status);

            if (trader is PlayerTrader && status)
            {
                SecondTrader.ToggleReady(true);
            }
        }

        protected override void OnTraderItemMoved(Trader trader, TradeItem item, bool modified, int difference)
        {
            base.OnTraderItemMoved(trader, item, modified, difference);

            if (item.Stack == 0)
            {
                InventoryHandler.SendExchangeObjectRemovedMessage(FirstTrader.Character.Client, trader != FirstTrader, item.Guid);
            }
            else if (modified)
            {
                InventoryHandler.SendExchangeObjectModifiedMessage(FirstTrader.Character.Client, trader != FirstTrader, item);
            }
            else
            {
                InventoryHandler.SendExchangeObjectAddedMessage(FirstTrader.Character.Client, trader != FirstTrader, item);
            }
        }

        protected override void OnTraderKamasChanged(Trader trader, uint amount)
        {
            base.OnTraderKamasChanged(trader, amount);

            InventoryHandler.SendExchangeKamaModifiedMessage(FirstTrader.Character.Client, trader != FirstTrader,
                                                             (int)amount);
        }
    }
}